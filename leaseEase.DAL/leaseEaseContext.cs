using System.IO;
using System.Data.Entity;
using System.Linq;
using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.User;
using System;
using System.Reflection.Emit;

namespace leaseEase.DAL
{
    public class leaseEaseContext : DbContext
    {
        public virtual DbSet<UDbSession> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TobeCreatorData> tobeCreatorDatas { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<TypesOfOffice> TypesOfOffice { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<OfficeImg> OfficeImages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public leaseEaseContext() : base("Host=localhost;Port=5432;Database=leaseEase;Username=postgres;Password=passForPGA")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<leaseEaseContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<leaseEaseContext>());
            setDB(this);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>()
                .HasMany(o => o.Reviews)
                .WithRequired(r => r.Office)
                .HasForeignKey(r => r.OfficeId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Office>()
                .HasMany(o => o.Bookings)
                .WithRequired(b => b.Office)
                .HasForeignKey(b => b.OfficeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Office>()
                .HasMany(o => o.Facilities)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("OfficeFacilities");
                    m.MapLeftKey("OfficeId");
                    m.MapRightKey("FacilityId");
                });

            modelBuilder.Entity<Office>()
             .HasMany(o => o.Images)
            .WithRequired(i => i.Office)
            .HasForeignKey(i => i.OfficeId)
            .WillCascadeOnDelete(true);


            modelBuilder.Entity<User>()
                .HasMany(u => u.MyBookings)
                .WithRequired(o => o.Creator)
                .HasForeignKey(o => o.CreatorId).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(u => u.MyReviews)
                .WithRequired(o => o.Creator)
                .HasForeignKey(o => o.CreatorId).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(o => o.WishList)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("UserWishList");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("OfficeId");
                });
            modelBuilder.Entity<User>()
                    .HasMany(o => o.Chats)
                    .WithMany(c=>c.Users)
                    .Map(m =>
                    {
                        m.ToTable("UserChats");
                        m.MapLeftKey("UserId");
                        m.MapRightKey("ChatId");
                    });
            modelBuilder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithRequired(o => o.Creator)
                .HasForeignKey(o => o.CreatorId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Chat    >()
                .HasMany(u => u.Messages)
                .WithRequired(o => o.Chat)
                .HasForeignKey(o => o.ChatId).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.creatorData)
                .WithRequired(cd => cd.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TobeCreatorData>()
                .HasMany(cd => cd.MyOffices)
                .WithRequired(o => o.Creator)
                .HasForeignKey(o => o.CreatorId)
                .WillCascadeOnDelete(true);
        }

        private void setDB(leaseEaseContext db)
        {
            //adding facilities by default
            addFacility(db, "High-speed internet access", "./leaseEase.Web/Content/images/High-speed internet access.png");
            addFacility(db, "Fully equipped kitchenette", "./leaseEase.Web/Content/images/Fully equipped kitchenette.png");
            addFacility(db, "Conference room facilities", "./leaseEase.Web/Content/images/Conference room facilities.png");
            addFacility(db, "On-site parking", "./leaseEase.Web/Content/images/On-site parking.png");
            addFacility(db, "On-site coffee bar", "./leaseEase.Web/Content/images/On-site coffee bar.png");
            addFacility(db, "Disabled-accessible amenities", "./leaseEase.Web/Content/images/Disabled-accessible amenities.png");
            addFacility(db, "Regular cleaning", "./leaseEase.Web/Content/images/Regular cleaning.png");
            addFacility(db, "24/7 security surveillance", "./leaseEase.Web/Content/images/247 security surveillance.png");
            addFacility(db, "Modern ergonomic furniture", "./leaseEase.Web/Content/images/Modern ergonomic furniture.png");
            addFacility(db, "Receptionist services", "./leaseEase.Web/Content/images/Receptionist services.png");
            addFacility(db, "Printing and scanning facilities", "./leaseEase.Web/Content/images/Printing and scanning facilities.png");
            addFacility(db, "Fitness center or gym access", "./leaseEase.Web/Content/images/Fitness center or gym access.png");
            addFacility(db, "Outdoor terrace", "./leaseEase.Web/Content/images/Outdoor terrace.png");
            addFacility(db, "Bicycle storage facilities", "./leaseEase.Web/Content/images/Bicycle storage facilities.png");
            addFacility(db, "Lounge areas", "./leaseEase.Web/Content/images/Lounge areas.png");

            //adding office types by default
            addTypeOfOffice(db, "Open Plan", "./leaseEase.Web/Content/images/Open Plan.png", "An office layout where there are no walls or partitions between workspaces. Open plan facilitates easy communication and collaboration among employees.");
            addTypeOfOffice(db, "Cubicle Style", "./leaseEase.Web/Content/images/Cubicle Style.png", "In this type of office, each worker is provided with their own cubicle or room for work, offering greater privacy and concentration.");
            addTypeOfOffice(db, "Coworking Space", "./leaseEase.Web/Content/images/Coworking Space.png", "Coworking spaces are open areas with shared desks that can be rented by individual entrepreneurs or small companies. They provide shared resources and networking opportunities.");
            addTypeOfOffice(db, "Virtual Office", "./leaseEase.Web/Content/images/Virtual Office.png", "A virtual office provides an address and secretarial services without a physical office presence. It's convenient for companies and freelancers working remotely but wanting a presence in a specific location.");
            addTypeOfOffice(db, "Shared Offices", "./leaseEase.Web/Content/images/Shared Offices.png", "These are offices leased by multiple companies or entrepreneurs and used jointly. They may include shared spaces as well as shared conference rooms and other amenities.");
            addTypeOfOffice(db, "Podcast Studios", "./leaseEase.Web/Content/images/Podcast Studios.png", "These are specialized office spaces equipped for podcast recording. They may include sound recording equipment, soundproofing, and other specific features for creating quality content.");
        }
        private void addFacility(leaseEaseContext db, string name, string imagePath)
        {
            if ((db.Facilities.FirstOrDefault(o => o.Name == name) == null)){
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imageFullPath = Path.Combine(baseDirectory, "..", imagePath);
                imageFullPath = Path.GetFullPath(imageFullPath);
                byte[] image = File.ReadAllBytes(imageFullPath);
                Facility facility = new Facility { Name = name, Image = image };
                db.Facilities.Add(facility);
                db.SaveChanges();
            }
        }

        private void addTypeOfOffice(leaseEaseContext db, string name, string imagePath, string description)
        {
            if ((db.TypesOfOffice.FirstOrDefault(o => o.Name == name) == null))
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imageFullPath = Path.Combine(baseDirectory, "..", imagePath);
                imageFullPath = Path.GetFullPath(imageFullPath);
                byte[] image = File.ReadAllBytes(imageFullPath);
                TypesOfOffice type = new TypesOfOffice { Name = name, Image = image, Description = description };
                db.TypesOfOffice.Add(type);
                db.SaveChanges();
            }
        }

    }
}
