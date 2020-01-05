﻿// <auto-generated />
using System;
using MasterDataProduct.PersistenceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataProduct.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20191213184438_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasterDataProduct.Models.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MasterDataProduct.Models.Products.Product", b =>
                {
                    b.OwnsOne("MasterDataProduct.Models.Products.ManufacturingPlan", "Plan", b1 =>
                        {
                            b1.Property<Guid>("Id");

                            b1.HasKey("Id");

                            b1.ToTable("ManufacturingPlan");

                            b1.HasOne("MasterDataProduct.Models.Products.Product")
                                .WithOne("Plan")
                                .HasForeignKey("MasterDataProduct.Models.Products.ManufacturingPlan", "Id")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsMany("MasterDataProduct.Models.Products.OperationId", "Ids", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd();

                                    b2.Property<int>("Index");

                                    b2.Property<Guid>("ManufacturingPlanId");

                                    b2.Property<string>("Value");

                                    b2.HasKey("Id");

                                    b2.HasIndex("ManufacturingPlanId");

                                    b2.ToTable("OperationId");

                                    b2.HasOne("MasterDataProduct.Models.Products.ManufacturingPlan")
                                        .WithMany("Ids")
                                        .HasForeignKey("ManufacturingPlanId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("MasterDataProduct.Models.Products.Ref", "Reference", b1 =>
                        {
                            b1.Property<Guid>("ProductId");

                            b1.Property<string>("Value");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product");

                            b1.HasOne("MasterDataProduct.Models.Products.Product")
                                .WithOne("Reference")
                                .HasForeignKey("MasterDataProduct.Models.Products.Ref", "ProductId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}