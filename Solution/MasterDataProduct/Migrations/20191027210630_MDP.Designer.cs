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
    [Migration("20191027210630_MDP")]
    partial class MDP
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
                            b1.Property<Guid>("ProductId");

                            b1.Property<string>("Name")
                                .IsRequired();

                            b1.HasKey("ProductId");

                            b1.ToTable("Product");

                            b1.HasOne("MasterDataProduct.Models.Products.Product")
                                .WithOne("Plan")
                                .HasForeignKey("MasterDataProduct.Models.Products.ManufacturingPlan", "ProductId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}