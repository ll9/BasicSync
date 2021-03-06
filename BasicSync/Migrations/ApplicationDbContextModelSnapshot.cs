﻿// <auto-generated />
using System;
using BasicSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BasicSync.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("BasicSync.Models.BasicEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Hausnummer");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Ort");

                    b.Property<int>("RowVersion");

                    b.Property<string>("Straße");

                    b.Property<bool>("SyncStatus");

                    b.HasKey("Id");

                    b.ToTable("BasicEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
