﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20210113030703_MigracionPostgresInicial")]
    partial class MigracionPostgresInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.AutorLibro", b =>
                {
                    b.Property<int>("AutorLibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("AutorLibroGuid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AutorLibroId");

                    b.ToTable("AutorLibro");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.GradoAcademico", b =>
                {
                    b.Property<int>("GradoAcademicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AutorLibroId")
                        .HasColumnType("integer");

                    b.Property<string>("CentroAcademico")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaGrado")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GradoAcademicoGuid")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("GradoAcademicoId");

                    b.HasIndex("AutorLibroId");

                    b.ToTable("GradoAcademico");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.GradoAcademico", b =>
                {
                    b.HasOne("TiendaServicios.Api.Autor.Modelo.AutorLibro", "AutorLibro")
                        .WithMany("GradosAcademicos")
                        .HasForeignKey("AutorLibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutorLibro");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.AutorLibro", b =>
                {
                    b.Navigation("GradosAcademicos");
                });
#pragma warning restore 612, 618
        }
    }
}
