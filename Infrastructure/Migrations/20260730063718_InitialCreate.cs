using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bars", x => x.Id);
                    table.CheckConstraint("CK_Bar_Capacity_Range", "\"Capacity\" >= 0 AND \"Capacity\"<=1000");
                });

            migrationBuilder.CreateTable(
                name: "djs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_djs", x => x.Id);
                    table.UniqueConstraint("AK_djs_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "photographers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photographers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PhotoUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VkUrl = table.Column<string>(type: "text", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsFirstEnterFinished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Quizman"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DaysOff = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.UniqueConstraint("AK_admins_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_admins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notifications_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "push_tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false, defaultValue: "Android"),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_push_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_push_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_push_tokens_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "quizmans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Points = table.Column<int>(type: "integer", precision: 10, nullable: false, defaultValue: 0),
                    Fines = table.Column<int>(type: "integer", precision: 10, nullable: false, defaultValue: 0),
                    WorkShift = table.Column<int>(type: "integer", precision: 10, nullable: false, defaultValue: 3),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizmans", x => x.Id);
                    table.UniqueConstraint("AK_quizmans_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_quizmans_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "text", nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "text", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "Draft"),
                    ResponsibleAdminId = table.Column<Guid>(type: "uuid", nullable: true),
                    BarId = table.Column<Guid>(type: "uuid", nullable: true),
                    Partner = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreateByAdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GameStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DjId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotographerId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdminCreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                    table.CheckConstraint("CK_StartGameTime_Later_Now", "\"GameStartTime\" >= CURRENT_TIMESTAMP");
                    table.CheckConstraint("CK_WorkGameTime_Later_Now", "\"WorkStartTime\" >= CURRENT_TIMESTAMP");
                    table.ForeignKey(
                        name: "FK_games_admins_AdminCreatorId",
                        column: x => x.AdminCreatorId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_games_admins_ResponsibleAdminId",
                        column: x => x.ResponsibleAdminId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_bars_BarId",
                        column: x => x.BarId,
                        principalTable: "bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_djs_DjId",
                        column: x => x.DjId,
                        principalTable: "djs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "replacements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    QuizemanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsFullShift = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Wait"),
                    TakenAdminId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: true),
                    QuizmanId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replacements", x => x.Id);
                    table.CheckConstraint("CK_Date_Later_Now", "\"Date\" >= CURRENT_DATE");
                    table.ForeignKey(
                        name: "FK_replacements_admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_replacements_admins_TakenAdminId",
                        column: x => x.TakenAdminId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replacements_quizmans_QuizemanId",
                        column: x => x.QuizemanId,
                        principalTable: "quizmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replacements_quizmans_QuizmanId",
                        column: x => x.QuizmanId,
                        principalTable: "quizmans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "smart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    QuizmanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_smart_quizmans_QuizmanId",
                        column: x => x.QuizmanId,
                        principalTable: "quizmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fines_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizmanId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fines_history", x => x.Id);
                    table.CheckConstraint("CK_Amout_Positive", "\"Amount\" > 0");
                    table.ForeignKey(
                        name: "FK_fines_history_admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fines_history_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fines_history_quizmans_QuizmanId",
                        column: x => x.QuizmanId,
                        principalTable: "quizmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "game_participants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizmanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false, defaultValue: "None"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FullShift = table.Column<bool>(type: "boolean", nullable: false),
                    QuizmanId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_game_participants_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_participants_quizmans_QuizmanId",
                        column: x => x.QuizmanId,
                        principalTable: "quizmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_game_participants_quizmans_QuizmanId1",
                        column: x => x.QuizmanId1,
                        principalTable: "quizmans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "points_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizmanId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_points_history", x => x.Id);
                    table.CheckConstraint("CK_Amout_Positive", "\"Amount\" > 0");
                    table.ForeignKey(
                        name: "FK_points_history_admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_points_history_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_points_history_quizmans_QuizmanId",
                        column: x => x.QuizmanId,
                        principalTable: "quizmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bars_Name_Address",
                table: "bars",
                columns: new[] { "Name", "Address" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fines_history_AdminId",
                table: "fines_history",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_fines_history_GameId",
                table: "fines_history",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_fines_history_QuizmanId",
                table: "fines_history",
                column: "QuizmanId");

            migrationBuilder.CreateIndex(
                name: "IX_game_participants_GameId",
                table: "game_participants",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_game_participants_GameId_QuizmanId",
                table: "game_participants",
                columns: new[] { "GameId", "QuizmanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_participants_QuizmanId",
                table: "game_participants",
                column: "QuizmanId");

            migrationBuilder.CreateIndex(
                name: "IX_game_participants_QuizmanId1",
                table: "game_participants",
                column: "QuizmanId1");

            migrationBuilder.CreateIndex(
                name: "IX_games_AdminCreatorId",
                table: "games",
                column: "AdminCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_games_BarId",
                table: "games",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_games_DjId",
                table: "games",
                column: "DjId");

            migrationBuilder.CreateIndex(
                name: "IX_games_GameStartTime",
                table: "games",
                column: "GameStartTime");

            migrationBuilder.CreateIndex(
                name: "IX_games_MasterId",
                table: "games",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_games_Name",
                table: "games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_games_PhotographerId",
                table: "games",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_games_ResponsibleAdminId",
                table: "games",
                column: "ResponsibleAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_games_Status",
                table: "games",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_CreatedAt",
                table: "notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_SenderId",
                table: "notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_Type",
                table: "notifications",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserId",
                table: "notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserId_IsRead",
                table: "notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_points_history_AdminId",
                table: "points_history",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_points_history_GameId",
                table: "points_history",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_points_history_QuizmanId",
                table: "points_history",
                column: "QuizmanId");

            migrationBuilder.CreateIndex(
                name: "IX_push_tokens_Token",
                table: "push_tokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_push_tokens_UserId_IsActive",
                table: "push_tokens",
                columns: new[] { "UserId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_push_tokens_UserId1",
                table: "push_tokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_ExpiresAt",
                table: "refresh_tokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_Token",
                table: "refresh_tokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId",
                table: "refresh_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId_RevokedAt",
                table: "refresh_tokens",
                columns: new[] { "UserId", "RevokedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_replacements_AdminId",
                table: "replacements",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_replacements_Date",
                table: "replacements",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_replacements_QuizemanId",
                table: "replacements",
                column: "QuizemanId");

            migrationBuilder.CreateIndex(
                name: "IX_replacements_QuizemanId_Date",
                table: "replacements",
                columns: new[] { "QuizemanId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_replacements_QuizmanId",
                table: "replacements",
                column: "QuizmanId");

            migrationBuilder.CreateIndex(
                name: "IX_replacements_TakenAdminId",
                table: "replacements",
                column: "TakenAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_smart_QuizmanId_Date",
                table: "smart",
                columns: new[] { "QuizmanId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Login",
                table: "users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_VkUrl",
                table: "users",
                column: "VkUrl",
                unique: true,
                filter: "\"VkUrl\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fines_history");

            migrationBuilder.DropTable(
                name: "game_participants");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "points_history");

            migrationBuilder.DropTable(
                name: "push_tokens");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "replacements");

            migrationBuilder.DropTable(
                name: "smart");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "quizmans");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "bars");

            migrationBuilder.DropTable(
                name: "djs");

            migrationBuilder.DropTable(
                name: "masters");

            migrationBuilder.DropTable(
                name: "photographers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
