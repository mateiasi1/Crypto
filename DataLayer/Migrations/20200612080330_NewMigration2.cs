using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class NewMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    CurrencyAbbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IdCurrency = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    IdBank = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    Sold = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBankAccount = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Ammount = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdFlatRateFee = table.Column<int>(nullable: false),
                    TransactionType = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmmountToExchange = table.Column<double>(nullable: false),
                    IdWalletFrom = table.Column<int>(nullable: false),
                    IdWalletTo = table.Column<int>(nullable: false),
                    Percentage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConversionTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBankAccount = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Ammount = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdFlatRateFee = table.Column<int>(nullable: false),
                    TransactionType = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crypto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoCurrencyName = table.Column<string>(nullable: true),
                    CryptoCurrencyAbbreviation = table.Column<string>(nullable: true),
                    Refference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crypto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IdCryptoCurrency = table.Column<int>(nullable: false),
                    CryptoCurrencyName = table.Column<string>(nullable: true),
                    IdCrypto = table.Column<int>(nullable: false),
                    CryptokName = table.Column<string>(nullable: true),
                    IdBankAccount = table.Column<int>(nullable: false),
                    Refference = table.Column<string>(nullable: true),
                    Sold = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAccountTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBankAccount = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Ammount = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IdFlatRateFee = table.Column<int>(nullable: false),
                    TransactionType = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAccountTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoCurrencyAbbreviation = table.Column<string>(nullable: true),
                    CryptoCurrencyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyAbbreviation = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percentage = table.Column<double>(nullable: false),
                    UserRole = table.Column<string>(nullable: true),
                    Obsolete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatRateFee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ammount = table.Column<double>(nullable: false),
                    Obsolete = table.Column<bool>(nullable: false),
                    UserRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRateFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    IsOver18 = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    TokenValue = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    ReferralId = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsOver18 = table.Column<bool>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClientCurrency = table.Column<int>(nullable: false),
                    Sold = table.Column<double>(nullable: false),
                    IdBank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    TokenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Login_Token_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Token",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Login_TokenId",
                table: "Login",
                column: "TokenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountTransaction");

            migrationBuilder.DropTable(
                name: "Conversion");

            migrationBuilder.DropTable(
                name: "ConversionTransaction");

            migrationBuilder.DropTable(
                name: "Crypto");

            migrationBuilder.DropTable(
                name: "CryptoAccount");

            migrationBuilder.DropTable(
                name: "CryptoAccountTransaction");

            migrationBuilder.DropTable(
                name: "CryptoCurrency");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Fee");

            migrationBuilder.DropTable(
                name: "FlatRateFee");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "RegisterUser");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
