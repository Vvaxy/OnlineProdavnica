using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OnlineProdavniceEF.Data;

#nullable disable

namespace OnlineProdavniceEF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("OnlineProdavniceEF.Models.Category", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();
                b.Property<string>("Name").IsRequired().HasMaxLength(50);
                b.HasKey("Id");
                b.ToTable("Categories");
            });

            modelBuilder.Entity("OnlineProdavniceEF.Models.Product", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();
                b.Property<int>("CategoryId");
                b.Property<string>("Description").HasMaxLength(200);
                b.Property<string>("ImageUrl").HasMaxLength(300);
                b.Property<string>("Name").IsRequired().HasMaxLength(80);
                b.Property<decimal>("Price");
                b.Property<int>("Quantity");
                b.HasKey("Id");
                b.HasIndex("CategoryId");
                b.ToTable("Products");
            });

            modelBuilder.Entity("OnlineProdavniceEF.Models.User", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();
                b.Property<string>("Email").IsRequired();
                b.Property<string>("FullName").IsRequired().HasMaxLength(50);
                b.Property<bool>("IsAdmin");
                b.Property<string>("Password").IsRequired().HasMaxLength(30);
                b.HasKey("Id");
                b.ToTable("Users");
            });

            modelBuilder.Entity("OnlineProdavniceEF.Models.Product", b =>
            {
                b.HasOne("OnlineProdavniceEF.Models.Category", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}
