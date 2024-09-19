﻿// <auto-generated />
using System;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INFRASTRUCTURE.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CORE.Entity.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("DateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VarChar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VarChar(200)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("VarChar(11)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Telefone")
                        .IsUnique();

                    b.ToTable("Contato", (string)null);
                });

            modelBuilder.Entity("CORE.Entity.ContatoRegiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContatoId")
                        .HasColumnType("Int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("DateTime");

                    b.Property<int>("RegiaoId")
                        .HasColumnType("Int");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.HasIndex("RegiaoId");

                    b.ToTable("ContatoRegiao", (string)null);
                });

            modelBuilder.Entity("CORE.Entity.Regiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("DateTime");

                    b.Property<string>("Ddd")
                        .IsRequired()
                        .HasColumnType("VarChar(2)");

                    b.HasKey("Id");

                    b.HasIndex("Ddd")
                        .IsUnique();

                    b.ToTable("Regiao", (string)null);
                });

            modelBuilder.Entity("CORE.Entity.ContatoRegiao", b =>
                {
                    b.HasOne("CORE.Entity.Contato", "Contato")
                        .WithMany("ContatosRegioes")
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE.Entity.Regiao", "Regiao")
                        .WithMany("ContatosRegioes")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contato");

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("CORE.Entity.Contato", b =>
                {
                    b.Navigation("ContatosRegioes");
                });

            modelBuilder.Entity("CORE.Entity.Regiao", b =>
                {
                    b.Navigation("ContatosRegioes");
                });
#pragma warning restore 612, 618
        }
    }
}
