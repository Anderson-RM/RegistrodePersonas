﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrodePersonas;

namespace RegistrodePersonas.Migrations
{
    [DbContext(typeof(contecto))]
    partial class contectoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("RegistrodePersonas.persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cedula")
                        .HasColumnType("TEXT");

                    b.Property<string>("direccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("nacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("nombre")
                        .HasColumnType("TEXT");

                    b.Property<string>("telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("personas");
                });
#pragma warning restore 612, 618
        }
    }
}