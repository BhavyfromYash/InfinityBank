﻿// <auto-generated />
using System;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingSystem.Migrations
{
    [DbContext(typeof(BankDbContext))]
    partial class BankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingSystem.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<DateTime>("AccCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchPhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IFSC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("CusId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankingSystem.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Landmark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pincode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("BankingSystem.Models.Beneficiary", b =>
                {
                    b.Property<int>("BenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenId"));

                    b.Property<string>("BenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransferType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BenId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("BankingSystem.Models.Customer", b =>
                {
                    b.Property<int>("CusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CusId"));

                    b.Property<string>("AadhaarNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DebitCard")
                        .HasColumnType("bit");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GrossAnnualIncome")
                        .HasColumnType("int");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NetBanking")
                        .HasColumnType("bit");

                    b.Property<string>("OccupationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PanCardNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceOfIncome")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CusId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankingSystem.Models.ForgotPassword", b =>
                {
                    b.Property<int>("FPassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FPassId"));

                    b.Property<int>("OTP")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FPassId");

                    b.ToTable("ForgotPassword");
                });

            modelBuilder.Entity("BankingSystem.Models.ForgotUserId", b =>
                {
                    b.Property<int>("FUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FUserId"));

                    b.Property<int>("AccountNo")
                        .HasColumnType("int");

                    b.Property<int>("OTP")
                        .HasColumnType("int");

                    b.HasKey("FUserId");

                    b.ToTable("ForgotUserId");
                });

            modelBuilder.Entity("BankingSystem.Models.FundTransfer", b =>
                {
                    b.Property<int>("TransId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransId"));

                    b.Property<DateTime>("TransDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransId");

                    b.ToTable("FundsTransfer");
                });

            modelBuilder.Entity("BankingSystem.Models.FundTransferBeneficiary", b =>
                {
                    b.Property<int>("FundsTransferBenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FundsTransferBenId"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BenId")
                        .HasColumnType("int");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IFSC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FundsTransferBenId");

                    b.HasIndex("BenId");

                    b.ToTable("FundTransferBeneficiaries");
                });

            modelBuilder.Entity("BankingSystem.Models.LogOut", b =>
                {
                    b.Property<int>("LogOutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogOutId"));

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Suggestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogOutId");

                    b.ToTable("LogOuts");
                });

            modelBuilder.Entity("BankingSystem.Models.ManagerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ManagerInfos");
                });

            modelBuilder.Entity("BankingSystem.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingSystem.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OTP")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankingSystem.Models.UserAccountStatus", b =>
                {
                    b.Property<int>("UserAccountStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAccountStatusId"));

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserAccountStatusId");

                    b.ToTable("UserAccountStatus");
                });

            modelBuilder.Entity("BankingSystem.Models.WithinBankBeneficiary", b =>
                {
                    b.Property<int>("WithinBankBenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WithinBankBenId"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BenId")
                        .HasColumnType("int");

                    b.HasKey("WithinBankBenId");

                    b.HasIndex("BenId");

                    b.ToTable("WithinBankBeneficiaries");
                });

            modelBuilder.Entity("BankingSystem.ViewModels.AccountDetailsViewModel", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AadhaarNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AccCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchPhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CusId")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IFSC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PanCardNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountNumber");

                    b.ToTable("ViewAccountDetails");
                });

            modelBuilder.Entity("BankingSystem.Models.Account", b =>
                {
                    b.HasOne("BankingSystem.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Models.FundTransferBeneficiary", b =>
                {
                    b.HasOne("BankingSystem.Models.Beneficiary", "Beneficiary")
                        .WithMany()
                        .HasForeignKey("BenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");
                });

            modelBuilder.Entity("BankingSystem.Models.ManagerInfo", b =>
                {
                    b.HasOne("BankingSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankingSystem.Models.Transaction", b =>
                {
                    b.HasOne("BankingSystem.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BankingSystem.Models.WithinBankBeneficiary", b =>
                {
                    b.HasOne("BankingSystem.Models.Beneficiary", "Beneficiary")
                        .WithMany()
                        .HasForeignKey("BenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");
                });

            modelBuilder.Entity("BankingSystem.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
