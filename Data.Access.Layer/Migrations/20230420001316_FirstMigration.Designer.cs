﻿// <auto-generated />
using System;
using Data.Access.Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Access.Layer.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230420001316_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Candidate.Models.Candidat", b =>
                {
                    b.Property<Guid>("IdCandidat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CvFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DernierEmployeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbAnneeExpr")
                        .HasColumnType("int");

                    b.Property<string>("NiveauEtude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCandidat");

                    b.ToTable("Candidats");
                });

            modelBuilder.Entity("Candidate.Models.Candidature", b =>
                {
                    b.Property<Guid>("IdCandidat")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdOffre")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCandidature")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCandidat", "IdOffre");

                    b.HasIndex("IdOffre");

                    b.ToTable("Candidatures");
                });

            modelBuilder.Entity("Candidate.Models.Offre", b =>
                {
                    b.Property<Guid>("IdOffres")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePublish")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MaxSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MinNbrExperience")
                        .HasColumnType("int");

                    b.Property<decimal>("MinSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOffres");

                    b.ToTable("Offres");
                });

            modelBuilder.Entity("Candidate.Models.Candidature", b =>
                {
                    b.HasOne("Candidate.Models.Candidat", "Candidat")
                        .WithMany("Candidatures")
                        .HasForeignKey("IdCandidat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Candidate.Models.Offre", "Offre")
                        .WithMany("Candidatures")
                        .HasForeignKey("IdOffre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");

                    b.Navigation("Offre");
                });

            modelBuilder.Entity("Candidate.Models.Candidat", b =>
                {
                    b.Navigation("Candidatures");
                });

            modelBuilder.Entity("Candidate.Models.Offre", b =>
                {
                    b.Navigation("Candidatures");
                });
#pragma warning restore 612, 618
        }
    }
}
