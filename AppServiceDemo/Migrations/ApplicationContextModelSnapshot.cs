﻿// <auto-generated />
using System;
using AppServiceDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppServiceDemo.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppServiceDemo.Data.Entities.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("AppServiceDemo.Data.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameSessionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("AppServiceDemo.Data.Entities.Player", b =>
                {
                    b.HasOne("AppServiceDemo.Data.Entities.GameSession", "GameSession")
                        .WithMany("Players")
                        .HasForeignKey("GameSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("AppServiceDemo.Data.Entities.GameSession", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
