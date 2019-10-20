﻿// <auto-generated />
using System;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20191018120710_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasterDataFactory.Models.Domain.MachineTypes.MachineType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("MachineTypes");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Domain.Machines.Machine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MachineTypeId");

                    b.HasKey("Id");

                    b.HasIndex("MachineTypeId");

                    b.ToTable("machine");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Domain.Operations.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cod");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Domain.ProductionLines.ProductionLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("ProductionLines");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Domain.Machines.Machine", b =>
                {
                    b.HasOne("MasterDataFactory.Models.Domain.MachineTypes.MachineType", "MachineType")
                        .WithMany()
                        .HasForeignKey("MachineTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}