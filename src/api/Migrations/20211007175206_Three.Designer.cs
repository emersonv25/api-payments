﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211007175206_Three")]
    partial class Three
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.Models.EntityModel.Installment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("IdParcela")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("ValorBruto");

                    b.Property<decimal?>("AnticipatedAmount")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("ValorAntecipado");

                    b.Property<DateTime>("ForecastPaymentAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("PrevisaoRecebimentoData");

                    b.Property<int>("InstallmentNumber")
                        .HasColumnType("int")
                        .HasColumnName("NumeroParcela");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("ValorLiquido");

                    b.Property<DateTime?>("PaymentAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("RepasseData");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint")
                        .HasColumnName("IdTransacao");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Parcelas");
                });

            modelBuilder.Entity("api.Models.EntityModel.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("IdTransacao")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Acquirer")
                        .HasColumnType("bit")
                        .HasColumnName("Adquirente");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("ValorBruto");

                    b.Property<bool?>("Anticipated")
                        .HasColumnType("bit")
                        .HasColumnName("Antecipado");

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataAprovacao");

                    b.Property<DateTime?>("DisapprovedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataReprovacao");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("Taxa");

                    b.Property<int>("InstallmentsNumber")
                        .HasColumnType("int")
                        .HasColumnName("NumeroParcelas");

                    b.Property<string>("LastFourDigitsCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UltimosQuatroDigitos");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("ValorLiquido");

                    b.Property<DateTime>("TransactionAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataTransacao");

                    b.HasKey("Id");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("api.Models.EntityModel.Installment", b =>
                {
                    b.HasOne("api.Models.EntityModel.Transaction", "Transaction")
                        .WithMany("Installments")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("api.Models.EntityModel.Transaction", b =>
                {
                    b.Navigation("Installments");
                });
#pragma warning restore 612, 618
        }
    }
}
