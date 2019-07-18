﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication17.Data;

namespace WebApplication17.Migrations
{
[DbContext(typeof(Contexts))]
[Migration("20190716155611_InitialCreate")]
partial class InitialCreate
{
protected override void BuildTargetModel(ModelBuilder modelBuilder)
{
#pragma warning disable 612, 618
modelBuilder
.HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
.HasAnnotation("Relational:MaxIdentifierLength", 128)
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

modelBuilder.Entity("WebApplication17.Data.Registration.RegisterUser", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<bool>("IsOver18");

b.Property<string>("Password");

b.Property<string>("PasswordHash");

b.Property<string>("PasswordSalt");

b.Property<string>("PhoneNumber");

b.Property<string>("ReferralId");

b.Property<string>("Username");

b.HasKey("Id");

b.ToTable("RegisterUser");
});

modelBuilder.Entity("WebApplication17.Data.User", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<bool>("Confirmed");

b.Property<bool>("IsOver18");

b.Property<string>("Password");

b.Property<string>("ReferralId");

b.Property<string>("Role");

b.Property<string>("Username");

b.HasKey("Id");

b.ToTable("User");
});

modelBuilder.Entity("WebApplication17.Models.Bank", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<string>("Adress");

b.Property<string>("CUI");

b.Property<string>("Country");

b.Property<string>("Name");

b.HasKey("Id");

b.ToTable("Bank");
});

modelBuilder.Entity("WebApplication17.Models.BankAccount", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<string>("IBAN");

b.Property<int>("IdBank");

b.Property<int>("IdClient");

b.Property<int>("IdCurrency");

b.Property<double>("Sold");

b.HasKey("Id");

b.ToTable("BankAccount");
});

modelBuilder.Entity("WebApplication17.Models.BankAccountTransaction", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<double>("Ammount");

b.Property<int>("IdBankAccount");

b.Property<int>("IdFlatRateFee");

b.Property<string>("Status");

b.HasKey("Id");

b.ToTable("BankAccountTransaction");
});

modelBuilder.Entity("WebApplication17.Models.ClientCurrency", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<int>("IdClient");

b.Property<int>("IdCurrency");

b.HasKey("Id");

b.ToTable("ClientCurrency");
});

modelBuilder.Entity("WebApplication17.Models.ClientDetails", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<string>("Adress");

b.Property<string>("Email");

b.Property<string>("FirstName");

b.Property<int>("IdUser");

b.Property<string>("LastName");

b.Property<string>("PhoneNumber");

b.HasKey("Id");

b.ToTable("ClientDetails");
});

modelBuilder.Entity("WebApplication17.Models.Conversion", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<double>("AmmountToExchange");

b.Property<int>("IdCommission");

b.Property<int>("IdWalletFrom");

b.Property<int>("IdWalletTo");

b.HasKey("Id");

b.ToTable("Conversion");
});

modelBuilder.Entity("WebApplication17.Models.ConversionTransaction", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<double>("Ammount");

b.Property<int>("IdBankAccount");

b.Property<int>("IdConversion");

b.Property<int>("IdFee");

b.HasKey("Id");

b.ToTable("ConversionTransaction");
});

modelBuilder.Entity("WebApplication17.Models.Currency", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<string>("Currency1");

b.Property<string>("CurrencyName");

b.Property<string>("Status");

b.HasKey("Id");

b.ToTable("Currency");
});

modelBuilder.Entity("WebApplication17.Models.Fee", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<bool>("Obsolete");

b.Property<string>("Percentage");

b.HasKey("Id");

b.ToTable("Fee");
});

modelBuilder.Entity("WebApplication17.Models.FlatRateFee", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<double>("Ammount");

b.Property<bool>("Obsolete");

b.HasKey("Id");

b.ToTable("FlatRateFee");
});

modelBuilder.Entity("WebApplication17.Wallet.Wallet", b =>
{
b.Property<int>("Id")
.ValueGeneratedOnAdd()
.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

b.Property<int>("IdBank");

b.Property<int>("IdClientCurrency");

b.Property<double>("Sold");

b.HasKey("Id");

b.ToTable("Wallet");
});
#pragma warning restore 612, 618
}
}
}
