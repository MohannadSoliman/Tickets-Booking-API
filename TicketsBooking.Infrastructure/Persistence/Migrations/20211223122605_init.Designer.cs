﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketsBooking.Infrastructure.Persistence;

namespace TicketsBooking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211223122605_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("TicketsBooking.Domain.Entities.Admin", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Email");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Place")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.EventProvider", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Bio")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<bool>("Verified")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("WebsiteLink")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Name");

                    b.HasIndex("WebsiteLink")
                        .IsUnique();

                    b.ToTable("EventProviders");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.Participant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("EventId")
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.SocialMedia", b =>
                {
                    b.Property<string>("Link")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("EventProviderName")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Link");

                    b.HasIndex("EventProviderName");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.Participant", b =>
                {
                    b.HasOne("TicketsBooking.Domain.Entities.Event", null)
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.SocialMedia", b =>
                {
                    b.HasOne("TicketsBooking.Domain.Entities.EventProvider", null)
                        .WithMany("SocialMedias")
                        .HasForeignKey("EventProviderName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.Event", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("TicketsBooking.Domain.Entities.EventProvider", b =>
                {
                    b.Navigation("SocialMedias");
                });
#pragma warning restore 612, 618
        }
    }
}
