﻿// <auto-generated />
using System;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasterDataFactory.Models.MachineTypes.MachineType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("MachineTypes");
                });

            modelBuilder.Entity("MasterDataFactory.Models.MachineTypesOperations.MachineTypeOperation", b =>
                {
                    b.Property<Guid>("MachineTypeId");

                    b.Property<Guid>("OperationId");

                    b.HasKey("MachineTypeId", "OperationId");

                    b.HasIndex("OperationId");

                    b.ToTable("MachineTypeOperations");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Machines.Machine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MachineTypeId");

                    b.Property<Guid?>("ProductionLineId");

                    b.HasKey("Id");

                    b.HasIndex("MachineTypeId");

                    b.HasIndex("ProductionLineId");

                    b.ToTable("machine");
                });

            modelBuilder.Entity("MasterDataFactory.Models.Operations.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("operations");
                });

            modelBuilder.Entity("MasterDataFactory.Models.ProductionLines.ProductionLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("ProductionLines");
                });

            modelBuilder.Entity("MasterDataFactory.Models.MachineTypes.MachineType", b =>
                {
                    b.OwnsOne("MasterDataFactory.Models.MachineTypes.MachineTypeDescription", "Type", b1 =>
                        {
                            b1.Property<Guid>("MachineTypeId");

                            b1.Property<string>("Type")
                                .HasColumnName("Tipo");

                            b1.HasKey("MachineTypeId");

                            b1.ToTable("MachineTypes");

                            b1.HasOne("MasterDataFactory.Models.MachineTypes.MachineType")
                                .WithOne("Type")
                                .HasForeignKey("MasterDataFactory.Models.MachineTypes.MachineTypeDescription", "MachineTypeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("MasterDataFactory.Models.MachineTypesOperations.MachineTypeOperation", b =>
                {
                    b.HasOne("MasterDataFactory.Models.MachineTypes.MachineType", "MachineType")
                        .WithMany("MachineTypeOperations")
                        .HasForeignKey("MachineTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MasterDataFactory.Models.Operations.Operation", "Operation")
                        .WithMany("MachineTypeOperations")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MasterDataFactory.Models.Machines.Machine", b =>
                {
                    b.HasOne("MasterDataFactory.Models.MachineTypes.MachineType", "MachineType")
                        .WithMany()
                        .HasForeignKey("MachineTypeId");

                    b.HasOne("MasterDataFactory.Models.ProductionLines.ProductionLine")
                        .WithMany("Machines")
                        .HasForeignKey("ProductionLineId");

                    b.OwnsOne("MasterDataFactory.Models.Machines.MachineBrand", "MachineBrand", b1 =>
                        {
                            b1.Property<Guid>("MachineId");

                            b1.Property<string>("Brand");

                            b1.HasKey("MachineId");

                            b1.ToTable("machine");

                            b1.HasOne("MasterDataFactory.Models.Machines.Machine")
                                .WithOne("MachineBrand")
                                .HasForeignKey("MasterDataFactory.Models.Machines.MachineBrand", "MachineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Machines.MachineLocation", "MachineLocation", b1 =>
                        {
                            b1.Property<Guid>("MachineId");

                            b1.Property<string>("Location");

                            b1.HasKey("MachineId");

                            b1.ToTable("machine");

                            b1.HasOne("MasterDataFactory.Models.Machines.Machine")
                                .WithOne("MachineLocation")
                                .HasForeignKey("MasterDataFactory.Models.Machines.MachineLocation", "MachineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Machines.MachineModel", "MachineModel", b1 =>
                        {
                            b1.Property<Guid>("MachineId");

                            b1.Property<string>("Model");

                            b1.HasKey("MachineId");

                            b1.ToTable("machine");

                            b1.HasOne("MasterDataFactory.Models.Machines.Machine")
                                .WithOne("MachineModel")
                                .HasForeignKey("MasterDataFactory.Models.Machines.MachineModel", "MachineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Machines.MachineState", "MachineState", b1 =>
                        {
                            b1.Property<Guid>("MachineId");

                            b1.Property<int>("State");

                            b1.HasKey("MachineId");

                            b1.ToTable("machine");

                            b1.HasOne("MasterDataFactory.Models.Machines.Machine")
                                .WithOne("MachineState")
                                .HasForeignKey("MasterDataFactory.Models.Machines.MachineState", "MachineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("MasterDataFactory.Models.Operations.Operation", b =>
                {
                    b.OwnsOne("MasterDataFactory.Models.Operations.OperationDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("OperationId");

                            b1.Property<string>("Value");

                            b1.HasKey("OperationId");

                            b1.ToTable("operations");

                            b1.HasOne("MasterDataFactory.Models.Operations.Operation")
                                .WithOne("Description")
                                .HasForeignKey("MasterDataFactory.Models.Operations.OperationDescription", "OperationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Operations.OperationDuration", "Duration", b1 =>
                        {
                            b1.Property<Guid>("OperationId");

                            b1.Property<TimeSpan>("Value");

                            b1.HasKey("OperationId");

                            b1.ToTable("operations");

                            b1.HasOne("MasterDataFactory.Models.Operations.Operation")
                                .WithOne("Duration")
                                .HasForeignKey("MasterDataFactory.Models.Operations.OperationDuration", "OperationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Operations.OperationTool", "Tool", b1 =>
                        {
                            b1.Property<Guid>("OperationId");

                            b1.Property<string>("Value");

                            b1.HasKey("OperationId");

                            b1.ToTable("operations");

                            b1.HasOne("MasterDataFactory.Models.Operations.Operation")
                                .WithOne("Tool")
                                .HasForeignKey("MasterDataFactory.Models.Operations.OperationTool", "OperationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("MasterDataFactory.Models.Operations.OperationToolSetupTime", "SetupTime", b1 =>
                        {
                            b1.Property<Guid>("OperationId");

                            b1.Property<TimeSpan>("Value");

                            b1.HasKey("OperationId");

                            b1.ToTable("operations");

                            b1.HasOne("MasterDataFactory.Models.Operations.Operation")
                                .WithOne("SetupTime")
                                .HasForeignKey("MasterDataFactory.Models.Operations.OperationToolSetupTime", "OperationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("MasterDataFactory.Models.ProductionLines.ProductionLine", b =>
                {
                    b.OwnsOne("MasterDataFactory.Models.ProductionLines.ProductionLineDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProductionLineId");

                            b1.Property<string>("Description");

                            b1.HasKey("ProductionLineId");

                            b1.ToTable("ProductionLines");

                            b1.HasOne("MasterDataFactory.Models.ProductionLines.ProductionLine")
                                .WithOne("Description")
                                .HasForeignKey("MasterDataFactory.Models.ProductionLines.ProductionLineDescription", "ProductionLineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
