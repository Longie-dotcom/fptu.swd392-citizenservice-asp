using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "CitizenAreas",
                columns: table => new
                {
                    CitizenAreaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLat = table.Column<double>(type: "float", nullable: false),
                    MaxLat = table.Column<double>(type: "float", nullable: false),
                    MinLng = table.Column<double>(type: "float", nullable: false),
                    MaxLng = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenAreas", x => x.CitizenAreaID);
                });

            migrationBuilder.CreateTable(
                name: "CitizenProfiles",
                columns: table => new
                {
                    CitizenProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointBalance = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenProfiles", x => x.CitizenProfileID);
                });

            migrationBuilder.CreateTable(
                name: "CollectionReports",
                columns: table => new
                {
                    CollectionReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WasteType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPS_Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GPS_Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReportAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CitizenProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionReports", x => x.CollectionReportID);
                    table.ForeignKey(
                        name: "FK_CollectionReports_CitizenProfiles_CitizenProfileID",
                        column: x => x.CitizenProfileID,
                        principalTable: "CitizenProfiles",
                        principalColumn: "CitizenProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintReports",
                columns: table => new
                {
                    ComplaintReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReportAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CitizenProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenAreaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintReports", x => x.ComplaintReportID);
                    table.ForeignKey(
                        name: "FK_ComplaintReports_CitizenAreas_CitizenAreaID",
                        column: x => x.CitizenAreaID,
                        principalTable: "CitizenAreas",
                        principalColumn: "CitizenAreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplaintReports_CitizenProfiles_CitizenProfileID",
                        column: x => x.CitizenProfileID,
                        principalTable: "CitizenProfiles",
                        principalColumn: "CitizenProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RewardHistories",
                columns: table => new
                {
                    RewardHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CitizenProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenAreaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardHistories", x => x.RewardHistoryID);
                    table.ForeignKey(
                        name: "FK_RewardHistories_CitizenAreas_CitizenAreaID",
                        column: x => x.CitizenAreaID,
                        principalTable: "CitizenAreas",
                        principalColumn: "CitizenAreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RewardHistories_CitizenProfiles_CitizenProfileID",
                        column: x => x.CitizenProfileID,
                        principalTable: "CitizenProfiles",
                        principalColumn: "CitizenProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionReports_CitizenProfileID",
                table: "CollectionReports",
                column: "CitizenProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReports_CitizenAreaID",
                table: "ComplaintReports",
                column: "CitizenAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReports_CitizenProfileID",
                table: "ComplaintReports",
                column: "CitizenProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_RewardHistories_CitizenAreaID",
                table: "RewardHistories",
                column: "CitizenAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_RewardHistories_CitizenProfileID",
                table: "RewardHistories",
                column: "CitizenProfileID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "CollectionReports");

            migrationBuilder.DropTable(
                name: "ComplaintReports");

            migrationBuilder.DropTable(
                name: "RewardHistories");

            migrationBuilder.DropTable(
                name: "CitizenAreas");

            migrationBuilder.DropTable(
                name: "CitizenProfiles");
        }
    }
}
