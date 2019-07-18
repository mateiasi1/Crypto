using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication17.Migrations
{
public partial class InitialCreate : Migration
{
protected override void Up(MigrationBuilder migrationBuilder)
{
migrationBuilder.CreateTable(
name: "Bank",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Name = table.Column<string>(nullable: true),
CUI = table.Column<string>(nullable: true),
Adress = table.Column<string>(nullable: true),
Country = table.Column<string>(nullable: true)
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdClient = table.Column<int>(nullable: false),
IdCurrency = table.Column<int>(nullable: false),
IdBank = table.Column<int>(nullable: false),
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdBankAccount = table.Column<int>(nullable: false),
Ammount = table.Column<double>(nullable: false),
Status = table.Column<string>(nullable: true),
IdFlatRateFee = table.Column<int>(nullable: false)
},
constraints: table =>
{
table.PrimaryKey("PK_BankAccountTransaction", x => x.Id);
});

migrationBuilder.CreateTable(
name: "ClientCurrency",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdClient = table.Column<int>(nullable: false),
IdCurrency = table.Column<int>(nullable: false)
},
constraints: table =>
{
table.PrimaryKey("PK_ClientCurrency", x => x.Id);
});

migrationBuilder.CreateTable(
name: "ClientDetails",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdUser = table.Column<int>(nullable: false),
FirstName = table.Column<string>(nullable: true),
LastName = table.Column<string>(nullable: true),
PhoneNumber = table.Column<string>(nullable: true),
Adress = table.Column<string>(nullable: true),
Email = table.Column<string>(nullable: true)
},
constraints: table =>
{
table.PrimaryKey("PK_ClientDetails", x => x.Id);
});

migrationBuilder.CreateTable(
name: "Conversion",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
AmmountToExchange = table.Column<double>(nullable: false),
IdWalletFrom = table.Column<int>(nullable: false),
IdWalletTo = table.Column<int>(nullable: false),
IdCommission = table.Column<int>(nullable: false)
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdConversion = table.Column<int>(nullable: false),
Ammount = table.Column<double>(nullable: false),
IdBankAccount = table.Column<int>(nullable: false),
IdFee = table.Column<int>(nullable: false)
},
constraints: table =>
{
table.PrimaryKey("PK_ConversionTransaction", x => x.Id);
});

migrationBuilder.CreateTable(
name: "Currency",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Currency1 = table.Column<string>(nullable: true),
CurrencyName = table.Column<string>(nullable: true),
Status = table.Column<string>(nullable: true)
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Percentage = table.Column<string>(nullable: true),
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Ammount = table.Column<double>(nullable: false),
Obsolete = table.Column<bool>(nullable: false)
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Username = table.Column<string>(nullable: true),
Password = table.Column<string>(nullable: true),
PasswordHash = table.Column<string>(nullable: true),
PasswordSalt = table.Column<string>(nullable: true),
ReferralId = table.Column<string>(nullable: true),
IsOver18 = table.Column<bool>(nullable: false),
PhoneNumber = table.Column<string>(nullable: true)
},
constraints: table =>
{
table.PrimaryKey("PK_RegisterUser", x => x.Id);
});

migrationBuilder.CreateTable(
name: "User",
columns: table => new
{
Id = table.Column<int>(nullable: false)
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
Username = table.Column<string>(nullable: true),
Password = table.Column<string>(nullable: true),
ReferralId = table.Column<string>(nullable: true),
Role = table.Column<string>(nullable: true),
IsOver18 = table.Column<bool>(nullable: false),
Confirmed = table.Column<bool>(nullable: false)
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
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
IdClientCurrency = table.Column<int>(nullable: false),
Sold = table.Column<double>(nullable: false),
IdBank = table.Column<int>(nullable: false)
},
constraints: table =>
{
table.PrimaryKey("PK_Wallet", x => x.Id);
});
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
name: "ClientCurrency");

migrationBuilder.DropTable(
name: "ClientDetails");

migrationBuilder.DropTable(
name: "Conversion");

migrationBuilder.DropTable(
name: "ConversionTransaction");

migrationBuilder.DropTable(
name: "Currency");

migrationBuilder.DropTable(
name: "Fee");

migrationBuilder.DropTable(
name: "FlatRateFee");

migrationBuilder.DropTable(
name: "RegisterUser");

migrationBuilder.DropTable(
name: "User");

migrationBuilder.DropTable(
name: "Wallet");
}
}
}
