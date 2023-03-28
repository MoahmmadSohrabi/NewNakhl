﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NakhleNakhoda.Data;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230227144359_Update-UserBook2")]
    partial class UpdateUserBook2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.BookFacility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("RoomFacilityId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserBookId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoomFacilityId");

                    b.HasIndex("UserBookId");

                    b.ToTable("BookFacility", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.BookRoomCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("BookDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoomCategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint");

                    b.Property<int>("RoomPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserBookId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoomCategoryId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserBookId");

                    b.ToTable("BookRoomCategory", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoomCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomCategoryId");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.RoomCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RoomCategory", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.RoomFacility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoomFacility", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.UserBook", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("QtyAdult")
                        .HasColumnType("int");

                    b.Property<int>("QtyBaby")
                        .HasColumnType("int");

                    b.Property<int>("QtyChild")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("PaymentId");

                    b.ToTable("UserBook", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.Member", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CannotLoginUntilDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("IdentifierNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLoginDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OtpExpireAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<long>("PictureId")
                        .HasColumnType("bigint");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShebaNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberToken", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Media.Picture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AltAttribute")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("SeoFilename")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("TitleAttribute")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("VirtualPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Picture", (string)null);
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Media.PictureBinary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte[]>("BinaryData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("PictureId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PictureId")
                        .IsUnique();

                    b.ToTable("PictureBinary");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.BookFacility", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Catalog.RoomFacility", "RoomFacility")
                        .WithMany()
                        .HasForeignKey("RoomFacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NakhleNakhoda.Domain.Catalog.UserBook", "UserBook")
                        .WithMany("RoomFacilities")
                        .HasForeignKey("UserBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomFacility");

                    b.Navigation("UserBook");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.BookRoomCategory", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Catalog.RoomCategory", "RoomCategory")
                        .WithMany()
                        .HasForeignKey("RoomCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NakhleNakhoda.Domain.Catalog.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NakhleNakhoda.Domain.Catalog.UserBook", "UserBook")
                        .WithMany("BookRoomCategories")
                        .HasForeignKey("UserBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("RoomCategory");

                    b.Navigation("UserBook");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.Room", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Catalog.RoomCategory", "RoomCategory")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RoomCategory");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.UserBook", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Member", "Member")
                        .WithMany("Reserves")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NakhleNakhoda.Domain.Catalog.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberClaim", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Member", "User")
                        .WithMany("UserClaim")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberLogin", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Member", "User")
                        .WithMany("UserLogin")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberRole", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NakhleNakhoda.Domain.Identity.Member", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.MemberToken", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Member", "User")
                        .WithMany("UserToken")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.RoleClaim", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Identity.Role", "Role")
                        .WithMany("RoleClaim")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Media.PictureBinary", b =>
                {
                    b.HasOne("NakhleNakhoda.Domain.Media.Picture", "Picture")
                        .WithOne("PictureBinary")
                        .HasForeignKey("NakhleNakhoda.Domain.Media.PictureBinary", "PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.RoomCategory", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Catalog.UserBook", b =>
                {
                    b.Navigation("BookRoomCategories");

                    b.Navigation("RoomFacilities");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.Member", b =>
                {
                    b.Navigation("Reserves");

                    b.Navigation("UserClaim");

                    b.Navigation("UserLogin");

                    b.Navigation("UserRole");

                    b.Navigation("UserToken");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Identity.Role", b =>
                {
                    b.Navigation("RoleClaim");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("NakhleNakhoda.Domain.Media.Picture", b =>
                {
                    b.Navigation("PictureBinary")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}