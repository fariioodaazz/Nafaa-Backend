using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nafaa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    BotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.BotId);
                });

            migrationBuilder.CreateTable(
                name: "Charities",
                columns: table => new
                {
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfRecipients = table.Column<int>(type: "int", nullable: false),
                    NumberOfStaff = table.Column<int>(type: "int", nullable: false),
                    NumberOfCampaigns = table.Column<int>(type: "int", nullable: false),
                    EndorsementScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charities", x => x.CharityId);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartnerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastMessageAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationId);
                    table.ForeignKey(
                        name: "FK_Conversations_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "BotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.DonorId);
                    table.ForeignKey(
                        name: "FK_Donors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    EducationLevel = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false),
                    MonthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyAssistance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_Recipients_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    Wage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeeklyWorkingHours = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staff_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFromUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardInformations",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardInformations", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CardInformations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharityDonor",
                columns: table => new
                {
                    FavoriteCharitiesCharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavoriteDonorsDonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityDonor", x => new { x.FavoriteCharitiesCharityId, x.FavoriteDonorsDonorId });
                    table.ForeignKey(
                        name: "FK_CharityDonor_Charities_FavoriteCharitiesCharityId",
                        column: x => x.FavoriteCharitiesCharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharityDonor_Donors_FavoriteDonorsDonorId",
                        column: x => x.FavoriteDonorsDonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharityDonor1",
                columns: table => new
                {
                    EndorsedCharitiesCharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndorsingDonorsDonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityDonor1", x => new { x.EndorsedCharitiesCharityId, x.EndorsingDonorsDonorId });
                    table.ForeignKey(
                        name: "FK_CharityDonor1_Charities_EndorsedCharitiesCharityId",
                        column: x => x.EndorsedCharitiesCharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharityDonor1_Donors_EndorsingDonorsDonorId",
                        column: x => x.EndorsingDonorsDonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Permissions = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    EducationLevel = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.NationalId);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Housings",
                columns: table => new
                {
                    HouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: false),
                    Ownership = table.Column<int>(type: "int", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Independence = table.Column<int>(type: "int", nullable: false),
                    WaterSource = table.Column<int>(type: "int", nullable: false),
                    HasElectricity = table.Column<bool>(type: "bit", nullable: false),
                    QualityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Housings", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_Housings_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    MedicalHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disability = table.Column<int>(type: "int", nullable: true),
                    ChronicCondition = table.Column<int>(type: "int", nullable: true),
                    Medications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilityStatus = table.Column<int>(type: "int", nullable: true),
                    RequiresMedicalCare = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.MedicalHistoryId);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentationPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    AmountRequested = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountApproved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReviewNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VirtualCards",
                columns: table => new
                {
                    VirtualCardCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pin = table.Column<int>(type: "int", nullable: false),
                    CardStatus = table.Column<int>(type: "int", nullable: false),
                    TotalPointsEarned = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualCards", x => x.VirtualCardCode);
                    table.ForeignKey(
                        name: "FK_VirtualCards_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedByStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Staff_CreatedByStaffId",
                        column: x => x.CreatedByStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharityStaffStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_StaffPermissions_Staff_CharityStaffStaffId",
                        column: x => x.CharityStaffStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "RequestReviewer",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharityStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestReviewer", x => new { x.RequestId, x.CharityStaffId });
                    table.ForeignKey(
                        name: "FK_RequestReviewer_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId");
                    table.ForeignKey(
                        name: "FK_RequestReviewer_Staff_CharityStaffId",
                        column: x => x.CharityStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    PointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointType = table.Column<int>(type: "int", nullable: false),
                    PointsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VirtualCardCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.PointId);
                    table.ForeignKey(
                        name: "FK_Points_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId");
                    table.ForeignKey(
                        name: "FK_Points_VirtualCards_VirtualCardCode",
                        column: x => x.VirtualCardCode,
                        principalTable: "VirtualCards",
                        principalColumn: "VirtualCardCode");
                });

            migrationBuilder.CreateTable(
                name: "CharityStaffProject",
                columns: table => new
                {
                    ProjectsProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMembersStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityStaffProject", x => new { x.ProjectsProjectId, x.StaffMembersStaffId });
                    table.ForeignKey(
                        name: "FK_CharityStaffProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_CharityStaffProject_Staff_StaffMembersStaffId",
                        column: x => x.StaffMembersStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationType = table.Column<int>(type: "int", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_Donations_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId");
                    table.ForeignKey(
                        name: "FK_Donations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectRecipient",
                columns: table => new
                {
                    ProjectsProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientsRecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRecipient", x => new { x.ProjectsProjectId, x.RecipientsRecipientId });
                    table.ForeignKey(
                        name: "FK_ProjectRecipient_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_ProjectRecipient_Recipients_RecipientsRecipientId",
                        column: x => x.RecipientsRecipientId,
                        principalTable: "Recipients",
                        principalColumn: "RecipientId");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledDonations",
                columns: table => new
                {
                    ScheduledDonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledDonations", x => x.ScheduledDonationId);
                    table.ForeignKey(
                        name: "FK_ScheduledDonations_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "CharityId");
                    table.ForeignKey(
                        name: "FK_ScheduledDonations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledDonations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "StaffStaffPermission",
                columns: table => new
                {
                    StaffMembersStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffPermissionPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffStaffPermission", x => new { x.StaffMembersStaffId, x.StaffPermissionPermissionId });
                    table.ForeignKey(
                        name: "FK_StaffStaffPermission_StaffPermissions_StaffPermissionPermissionId",
                        column: x => x.StaffPermissionPermissionId,
                        principalTable: "StaffPermissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffStaffPermission_Staff_StaffMembersStaffId",
                        column: x => x.StaffMembersStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentGatewayReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Donations_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donations",
                        principalColumn: "DonationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CardInformations_DonorId",
                table: "CardInformations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityDonor_FavoriteDonorsDonorId",
                table: "CharityDonor",
                column: "FavoriteDonorsDonorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityDonor1_EndorsingDonorsDonorId",
                table: "CharityDonor1",
                column: "EndorsingDonorsDonorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityStaffProject_StaffMembersStaffId",
                table: "CharityStaffProject",
                column: "StaffMembersStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_BotId",
                table: "Conversations",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CharityId",
                table: "Donations",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ProjectId",
                table: "Donations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_UserId",
                table: "Donors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_RecipientId",
                table: "FamilyMembers",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_RecipientId",
                table: "Housings",
                column: "RecipientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_RecipientId",
                table: "MedicalHistories",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DonationId",
                table: "Payments",
                column: "DonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Points_CharityId",
                table: "Points",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_VirtualCardCode",
                table: "Points",
                column: "VirtualCardCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecipient_RecipientsRecipientId",
                table: "ProjectRecipient",
                column: "RecipientsRecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CharityId",
                table: "Projects",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedByStaffId",
                table: "Projects",
                column: "CreatedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_CharityId",
                table: "Recipients",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestReviewer_CharityStaffId",
                table: "RequestReviewer",
                column: "CharityStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RecipientId",
                table: "Requests",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDonations_CharityId",
                table: "ScheduledDonations",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDonations_DonorId",
                table: "ScheduledDonations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDonations_ProjectId",
                table: "ScheduledDonations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CharityId",
                table: "Staff",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PartnerId",
                table: "Staff",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_CharityStaffStaffId",
                table: "StaffPermissions",
                column: "CharityStaffStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffStaffPermission_StaffPermissionPermissionId",
                table: "StaffStaffPermission",
                column: "StaffPermissionPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualCards_RecipientId",
                table: "VirtualCards",
                column: "RecipientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CardInformations");

            migrationBuilder.DropTable(
                name: "CharityDonor");

            migrationBuilder.DropTable(
                name: "CharityDonor1");

            migrationBuilder.DropTable(
                name: "CharityStaffProject");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "Housings");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "ProjectRecipient");

            migrationBuilder.DropTable(
                name: "RequestReviewer");

            migrationBuilder.DropTable(
                name: "ScheduledDonations");

            migrationBuilder.DropTable(
                name: "StaffStaffPermission");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "VirtualCards");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Charities");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
