﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20230421192054_ConfiguracionInicial")]
    partial class ConfiguracionInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("backend.Models.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Medico")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Paciente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("backend.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cita")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Paciente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("backend.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("backend.Models.Medico", b =>
                {
                    b.HasBaseType("backend.Models.Usuario");

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentoIdentificacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.ToTable("Usuarios", t =>
                        {
                            t.Property("DocumentoIdentificacion")
                                .HasColumnName("Medico_DocumentoIdentificacion");

                            t.Property("NombreCompleto")
                                .HasColumnName("Medico_NombreCompleto");

                            t.Property("Telefono")
                                .HasColumnName("Medico_Telefono");
                        });

                    b.HasDiscriminator().HasValue("Medico");
                });

            modelBuilder.Entity("backend.Models.Paciente", b =>
                {
                    b.HasBaseType("backend.Models.Usuario");

                    b.Property<string>("Beneficiarios")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentoIdentificacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Paciente");
                });
#pragma warning restore 612, 618
        }
    }
}