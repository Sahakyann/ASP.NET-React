﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspnetserver.Data;

#nullable disable

namespace aspnetserver.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240414150131_addUsersData")]
    partial class addUsersData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("aspnetserver.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("PostId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Content = "This is post 1 and it has some very interesting content",
                            Title = "Post 1"
                        },
                        new
                        {
                            PostId = 2,
                            Content = "This is post 2 and it has some very interesting content",
                            Title = "Post 2"
                        },
                        new
                        {
                            PostId = 3,
                            Content = "This is post 3 and it has some very interesting content",
                            Title = "Post 3"
                        },
                        new
                        {
                            PostId = 4,
                            Content = "This is post 4 and it has some very interesting content",
                            Title = "Post 4"
                        },
                        new
                        {
                            PostId = 5,
                            Content = "This is post 5 and it has some very interesting content",
                            Title = "Post 5"
                        },
                        new
                        {
                            PostId = 6,
                            Content = "This is post 6 and it has some very interesting content",
                            Title = "Post 6"
                        });
                });

            modelBuilder.Entity("aspnetserver.Data.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("userID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            userID = 1,
                            password = "123",
                            username = "User1"
                        },
                        new
                        {
                            userID = 2,
                            password = "123",
                            username = "User2"
                        },
                        new
                        {
                            userID = 3,
                            password = "123",
                            username = "User3"
                        },
                        new
                        {
                            userID = 4,
                            password = "123",
                            username = "User4"
                        },
                        new
                        {
                            userID = 5,
                            password = "123",
                            username = "User5"
                        },
                        new
                        {
                            userID = 6,
                            password = "123",
                            username = "User6"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
