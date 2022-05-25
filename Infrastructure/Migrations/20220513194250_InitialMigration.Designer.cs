﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MovieShopDbContext))]
    [Migration("20220513194250_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationCore.Entities.Movie", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("BackdropUrl")
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal?>("Budget")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("CreatedBy")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("CreatedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("ImdbUrl")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OriginalLanguage")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Overview")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PosterUrl")
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal?>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime?>("ReleaseDate")
                    .HasColumnType("datetime2");

                b.Property<decimal?>("Revenue")
                    .HasColumnType("decimal(18,2)");

                b.Property<int?>("RunTime")
                    .HasColumnType("int");

                b.Property<string>("Tagline")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("TmdbUrl")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UpdatedBy")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.ToTable("Movies");
            });
#pragma warning restore 612, 618
        }
    }
}