using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagement.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Members",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Surnames = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                AddressStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AddressNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AddressAdditionalInformation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AddressZipCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AddressTown = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                AddressCountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DNI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Members", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MembershipPayment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Amount = table.Column<double>(type: "float", nullable: false),
                PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                Method = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MembershipPayment", x => x.Id);
                table.ForeignKey(
                    name: "FK_MembershipPayment_Members_MemberId",
                    column: x => x.MemberId,
                    principalTable: "Members",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MembershipPayment_MemberId",
            table: "MembershipPayment",
            column: "MemberId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MembershipPayment");

        migrationBuilder.DropTable(
            name: "Members");
    }
}
