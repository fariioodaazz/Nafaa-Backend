using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nafaa.Api.Models.Auth;
using Nafaa.Api.Services;
using Nafaa.Domain.Entities;
using Nafaa.Domain.Enums;
using Nafaa.Infrastructure.Data;
using Nafaa.Infrastructure.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Nafaa.Api.Services.Auth;



namespace Nafaa.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly NafaaDbContext _dbContext;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService; 
    private readonly IPasswordResetService _passwordResetService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        NafaaDbContext dbContext,
        IJwtTokenService jwtTokenService,
        IEmailService emailService,
        IPasswordResetService passwordResetService,
        IRefreshTokenService refreshTokenService,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
        _passwordResetService = passwordResetService;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    // =========================
    // 1) REGISTER DONOR (WITH EMAIL CONFIRMATION)
    // =========================
    [HttpPost("donor/register")]
    public async Task<IActionResult> RegisterDonor([FromBody] DonorRegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 1. Create Identity user (security account)
        var identityUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = false
        };

        // 2. Create security account with password
        var createResult = await _userManager.CreateAsync(identityUser, request.Password);

        if (!createResult.Succeeded)
        {
            return BadRequest(new
            {
                errors = createResult.Errors.Select(e => e.Description)
            });
        }

        // 3. Create domain user (business account)
        var user = new User
        {
            UserId = identityUser.Id,
            UserName = request.Email,
            Role = UserRole.Donor,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth ?? DateTime.UtcNow.AddYears(-18), // Added null check
            PhoneNumber = request.PhoneNumber,
            DateCreated = DateTime.UtcNow,
            IsEmailVerified = false,
            Password = "HASHED_BY_IDENTITY"
        };

        // 4. Create donor profile
        var donor = new Donor
        {
            DonorId = Guid.NewGuid(),
            UserId = user.UserId,
            User = user,
        };

        try
        {
            // Save to database
            await _dbContext.Users.AddAsync(user);
            await _dbContext.Donors.AddAsync(donor);
            await _dbContext.SaveChangesAsync();

            // 5. Generate email confirmation token
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            // URL encode the token for safe transmission in URL
            var encodedToken = WebUtility.UrlEncode(emailToken);

            // 6. Generate confirmation link
            var confirmationLink = $"http://localhost:5218/api/auth/confirm-email?userId={identityUser.Id}&token={encodedToken}";

            // 7. Send confirmation email
            await _emailService.SendEmailConfirmationAsync(
                request.Email,
                request.FirstName,
                confirmationLink);

            return Ok(new
            {
                success = true,
                message = "Donor registered successfully. Please check your email to verify your account.",
                userId = user.UserId,
                donorId = donor.DonorId,
                email = request.Email,
                confirmationLink = confirmationLink,
            });
        }
        catch (DbUpdateException ex)
        {
            // Rollback: Delete the identity user if domain user creation fails
            await _userManager.DeleteAsync(identityUser);

            // Log the error
            Console.WriteLine($"Database error during donor registration: {ex.Message}");

            return StatusCode(500, new
            {
                message = "An error occurred during registration. Please try again.",
                details = ex.InnerException?.Message
            });
        }
        catch (Exception ex)
        {
            // Clean up on any other error
            if (await _userManager.FindByIdAsync(identityUser.Id.ToString()) != null)
            {
                await _userManager.DeleteAsync(identityUser);
            }

            // Log the error
            Console.WriteLine($"Error during donor registration: {ex.Message}");

            return StatusCode(500, new
            {
                message = "An unexpected error occurred. Please try again."
            });
        }
    }

    // =========================
    // 2) LOGIN DONOR
    // =========================
    [HttpPost("donor/login")]
    public async Task<IActionResult> LoginDonor([FromBody] DonorLoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 1. Try to find by email first
        ApplicationUser? identityUser = await _userManager.FindByEmailAsync(request.Identifier);

        // 2. If not found, try by phone number
        if (identityUser == null)
        {
            identityUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.Identifier);
        }

        if (identityUser == null)
            return Unauthorized(new { message = "Wrong Email or Phone Number, please try again." });

        // 3. Check password 
        var signInResult = await _signInManager.PasswordSignInAsync(
            identityUser,
            request.Password,
            isPersistent: false,
            lockoutOnFailure: true);

        if (!signInResult.Succeeded)
        {
            return Unauthorized(new { message = "Wrong Password, please try again." });
        }

        // 4. Load domain user + donor
        var user = await _dbContext.Users
            .Include(u => u.Donor)
            .FirstOrDefaultAsync(u => u.UserId == identityUser.Id);

        if (user == null || user.Donor == null)
        {
            return BadRequest(new { message = "User is not registered as donor." });
        }

        // 5. Issue JWT
        var tokenResult = _jwtTokenService.GenerateToken(user, user.Donor);

        // 6. Create refresh token
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user, ipAddress);

        var response = new AuthResponse
        {
            Token = tokenResult.Token,
            ExpiresAt = tokenResult.ExpiresAt,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiresAt = refreshToken.ExpiresAt,
            UserId = user.UserId,
            DonorId = user.Donor.DonorId,
            Role = user.Role.ToString(),
            Email = identityUser.Email ?? string.Empty
        };

        return Ok(response);
    }

    // Helper endpoint (for CreatedAtAction in register)
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        // 1. Get userId from JWT claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                        ?? User.FindFirst(JwtRegisteredClaimNames.Sub);

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized(new { message = "Invalid token: no user id." });
        }

        // 2. Load domain User with related entities
        var user = await _dbContext.Users
            .Include(u => u.Donor)
            .Include(u => u.Recipient)
            .Include(u => u.CharityStaff)
            .Include(u => u.PartnerStaff)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // 3. Load Identity user to get email / email confirmation
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());

        var response = new MeResponse
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            Role = user.Role,
            IsEmailVerified = user.IsEmailVerified || (identityUser?.EmailConfirmed ?? false),

            DonorId = user.Donor?.DonorId,
            RecipientId = user.Recipient?.RecipientId,
            CharityStaffId = user.CharityStaff?.StaffId,
            PartnerStaffId = user.PartnerStaff?.StaffId,

            Email = identityUser?.Email
        };

        return Ok(response);
    }

    // =========================
    // 3) EMAIL CONFIRMATION
    // =========================

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string token)
    {
        Console.WriteLine($"=== EMAIL CONFIRMATION ===");
        Console.WriteLine($"Token: {token?.Substring(0, 50)}...");
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return BadRequest("Invalid user");
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new { message = "Token is required." });
        }

        if (user.EmailConfirmed) return Ok("Already confirmed");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            return Ok("Email confirmed!");
        }

        return BadRequest($"Invalid token: {result.Errors.FirstOrDefault()?.Description}");
    }

    // =========================
    // 4) TEST SMTP CONNECTION
    // =========================

    [HttpGet("test-smtp-connection")]
    [AllowAnonymous]
    public async Task<IActionResult> TestSmtpConnection()
    {
        try
        {
            using var client = new System.Net.Mail.SmtpClient("sandbox.smtp.mailtrap.io", 587);
            client.Credentials = new NetworkCredential("e849a4602de752", "3ba8ed64202de7");
            client.EnableSsl = false;
            client.Timeout = 5000;

            // Test connection
            await client.SendMailAsync(
                "test@nafaa.com",
                "test@example.com",
                "Test",
                "Test body");

            return Ok(new { success = true, message = "SMTP connection successful" });
        }
        catch (Exception ex)
        {
            return Ok(new
            {
                success = false,
                message = "SMTP connection failed",
                error = ex.Message,
                details = "Check firewall, credentials, or try different port"
            });
        }
    }

    // =========================
    // 5) FORGOT PASSWORD
    // =========================

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var debugLink = await _passwordResetService.SendResetPasswordAsync(request.Email);

        return Ok(new
        {
            message = "If this email exists, a reset link has been sent.",
            // DEV ONLY: handy for testing without frontend
            debugResetLink = debugLink
        });
    }

    // =========================
    // 6) RESET PASSWORD
    // =========================
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, errors) = await _passwordResetService.ResetPasswordAsync(
            request.Email,
            request.Token,
            request.NewPassword);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Could not reset password.",
                errors
            });
        }

        return Ok(new { message = "Password reset successfully." });
    }

    // =========================
    // 7) REFRESH TOKEN
    // =========================
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, user, refreshToken, error) =
            await _refreshTokenService.ValidateAsync(request.RefreshToken);

        if (!success || user == null || refreshToken == null)
        {
            return Unauthorized(new { message = error ?? "Invalid refresh token." });
        }

        // Load related donor/roles
        var fullUser = await _dbContext.Users
            .Include(u => u.Donor)
            .Include(u => u.Recipient)
            .Include(u => u.CharityStaff)
            .Include(u => u.PartnerStaff)
            .FirstOrDefaultAsync(u => u.UserId == user.UserId);

        if (fullUser == null)
            return Unauthorized(new { message = "User no longer exists." });

        var tokenResult = _jwtTokenService.GenerateToken(fullUser, fullUser.Donor);

        var response = new AuthResponse
        {
            Token = tokenResult.Token,
            ExpiresAt = tokenResult.ExpiresAt,
            RefreshToken = refreshToken.Token,           
            RefreshTokenExpiresAt = refreshToken.ExpiresAt,
            UserId = fullUser.UserId,
            DonorId = fullUser.Donor?.DonorId,
            Role = fullUser.Role.ToString(),
            Email = fullUser.UserName
        };

        return Ok(response);
    }

    // =========================
    // 8) LOGOUT (REVOKE REFRESH TOKEN)
    // =========================
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized(new { message = "Invalid token." });
        }

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (request.AllDevices)
        {
            await _refreshTokenService.RevokeAllForUserAsync(userId, ip);
        }
        else if (!string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            await _refreshTokenService.RevokeAsync(request.RefreshToken, ip);
        }

        await _signInManager.SignOutAsync();

        return Ok(new { message = "Logged out successfully." });
    }
    // =========================
    // CHANGE PASSWORD
    // =========================
    [HttpPost("change-password")]
    [Authorize] // Only logged-in users
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Get current user from JWT token
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { message = "User not found." });

            // Verify current password
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                return BadRequest(new { message = "Current password is incorrect." });
            }

            // Change the password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Failed to change password.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }

            // Update security stamp (invalidates existing tokens)
            await _userManager.UpdateSecurityStampAsync(user);

            return Ok(new
            {
                success = true,
                message = "Password changed successfully.",
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password");
            return StatusCode(500, new { message = "Error changing password.", error = ex.Message });
        }
    }
}