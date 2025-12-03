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

namespace Nafaa.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly NafaaDbContext _dbContext;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        NafaaDbContext dbContext,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
    }

    // =========================
    // 1) REGISTER DONOR
    // =========================
    [HttpPost("donor/register")]
    public async Task<IActionResult> RegisterDonor([FromBody] DonorRegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var identityUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                errors = result.Errors.Select(e => e.Description)
            });
        }

        var user = new User
        {
            UserId = identityUser.Id,
            UserName = request.Email,      
            Role = UserRole.Donor,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = DateTime.UtcNow,
            PhoneNumber = request.PhoneNumber,
            DateCreated = DateTime.UtcNow,
            IsEmailVerified = false,
            Password = "HASHED_BY_IDENTITY"
        };

        var donor = new Donor
        {
            DonorId = Guid.NewGuid(),
            UserId = user.UserId,
            User = user
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.Donors.AddAsync(donor);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCurrentUser), new { }, new
        {
            message = "Donor registered successfully. Please verify your email.",
            userId = user.UserId,
            donorId = donor.DonorId
        });
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

        var response = new AuthResponse
        {
            Token = tokenResult.Token,
            ExpiresAt = tokenResult.ExpiresAt,
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

}
