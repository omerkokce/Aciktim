using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aciktim.Models
{
    public partial class AciktimContext : DbContext
    {
        public AciktimContext()
        {
        }

        public AciktimContext(DbContextOptions<AciktimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Apartment> Apartments { get; set; } = null!;
        public virtual DbSet<ApartmentNumber> ApartmentNumbers { get; set; } = null!;
        public virtual DbSet<BasketProduct> BasketProducts { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Carrier> Carriers { get; set; } = null!;
        public virtual DbSet<CarrierRestaurant> CarrierRestaurants { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientAddress> ClientAddresses { get; set; } = null!;
        public virtual DbSet<ClientFavorite> ClientFavorites { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductMenu> ProductMenus { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Street> Streets { get; set; } = null!;
        public virtual DbSet<GetAddress> FullAddress { get; set; }
        public virtual DbSet<OrderPrice> OrderPrices { get; set; }

        public IQueryable<GetAddress> GetRestaurantFullAddress(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            return FullAddress.FromSqlRaw("EXECUTE GetRestaurantAddress @id", pId);
        }
        public IQueryable<GetAddress> GetClientFullAddress(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            return FullAddress.FromSqlRaw("EXECUTE GetClientAddress @id", pId);
        }
        public IQueryable<GetAddress> GetOrderFullAddress(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            return FullAddress.FromSqlRaw("EXECUTE GetOrderAddress @id", pId);
        }
        public IQueryable<OrderPrice> GetPriceOrder(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            return OrderPrices.FromSqlRaw("EXECUTE GetPriceOrder @id", pId);
        }
        public IQueryable<Product> GetBasketProduct(int id)
        {
            SqlParameter pId = new SqlParameter("@id", id);
            return Products.FromSqlRaw("EXECUTE GetBasketProduct @id", pId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=Aciktim;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressName).HasMaxLength(50);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__Apartme__41EDCAC5");

                entity.HasOne(d => d.ApartmentNumber)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.ApartmentNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__Apartme__42E1EEFE");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__CityId__3F115E1A");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__Country__3E1D39E1");

                entity.HasOne(d => d.Neighbourhood)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.NeighbourhoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__Neighbo__40F9A68C");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__StateId__498EEC8D");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__StreetI__40058253");
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartment");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Stree__5EBF139D");
            });

            modelBuilder.Entity<ApartmentNumber>(entity =>
            {
                entity.ToTable("ApartmentNumber");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.ApartmentNumbers)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Apart__619B8048");
            });

            modelBuilder.Entity<BasketProduct>(entity =>
            {
                entity.HasKey(e => e.Bpid)
                    .HasName("PK__tmp_ms_x__3876B6ACED5DA249");

                entity.ToTable("BasketProduct");

                entity.Property(e => e.Bpid).HasColumnName("BPId");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BasketPro__Baske__17036CC0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BasketProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BasketPro__Produ__17F790F9");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.HasIndex(e => e.CardNumber, "UQ__Card__A4E9FFE9C7A77318")
                    .IsUnique();

                entity.Property(e => e.CardNumber).HasMaxLength(20);

                entity.Property(e => e.Cvv)
                    .HasMaxLength(3)
                    .HasColumnName("CVV");

                entity.Property(e => e.ExprationDate).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Card__ClientId__6477ECF3");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("Carrier");

                entity.HasIndex(e => e.Phone, "UQ__Carrier__5C7E359EBFDB7F0F")
                    .IsUnique();

                entity.HasIndex(e => e.CarrierName, "UQ__Carrier__B14565B072E6DB3C")
                    .IsUnique();

                entity.Property(e => e.CarrierName).HasMaxLength(50);

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .HasColumnName("E-mail");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Carriers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carrier__Address__45BE5BA9");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Carriers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carrier__RoleId__14270015");
            });

            modelBuilder.Entity<CarrierRestaurant>(entity =>
            {
                entity.HasKey(e => e.Crid)
                    .HasName("PK__tmp_ms_x__F2363F32A8221D6E");

                entity.ToTable("CarrierRestaurant");

                entity.Property(e => e.Crid).HasColumnName("CRId");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.CarrierRestaurants)
                    .HasForeignKey(d => d.CarrierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarrierRe__Carri__1AD3FDA4");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.CarrierRestaurants)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarrierRe__Resta__1BC821DD");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__City__CountryId__6E01572D");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasIndex(e => e.Phone, "UQ__Client__5C7E359E02B08A7B")
                    .IsUnique();

                entity.HasIndex(e => e.ClientName, "UQ__Client__65800DA0E53930E6")
                    .IsUnique();

                entity.HasIndex(e => e.EMail, "UQ__Client__E720A1C3E0A01326")
                    .IsUnique();

                entity.HasIndex(e => e.EMail, "UQ__Table__E720A1C35FD1595E")
                    .IsUnique();

                entity.Property(e => e.ClientName).HasMaxLength(20);

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .HasColumnName("E-mail");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Client__RoleID__71D1E811");
            });

            modelBuilder.Entity<ClientAddress>(entity =>
            {
                entity.HasKey(e => e.Caid)
                    .HasName("PK__ClientAd__A8D21E5638D82C81");

                entity.ToTable("ClientAddress");

                entity.Property(e => e.Caid).HasColumnName("CAId");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientAdd__Addre__46B27FE2");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientAdd__Clien__73BA3083");
            });

            modelBuilder.Entity<ClientFavorite>(entity =>
            {
                entity.HasKey(e => e.FavoriteId)
                    .HasName("PK__ClientFa__CE74FAD5AE5101DD");

                entity.ToTable("ClientFavorite");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientFavorites)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientFav__Clien__74AE54BC");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.ClientFavorites)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientFav__Resta__75A278F5");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__ClientI__76969D2E");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Restaur__778AC167");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.Expiration).HasColumnType("date");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.FileName).HasMaxLength(100);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.Property(e => e.ManagerName).HasMaxLength(20);

                entity.Property(e => e.Surname).HasMaxLength(20);

                entity.Property(e => e.Tcno)
                    .HasMaxLength(11)
                    .HasColumnName("TCNo");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Menu__ImageId__2BFE89A6");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__Restaurant__787EE5A0");
            });

            modelBuilder.Entity<Neighbourhood>(entity =>
            {
                entity.ToTable("Neighbourhood");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Neighbourhoods)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Neighbour__State__797309D9");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__AddressId__43D61337");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__ClientId__7A672E12");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.Opid)
                    .HasName("PK__OrderPro__AE2CBEFEB17174C9");

                entity.ToTable("OrderProduct");

                entity.Property(e => e.Opid).HasColumnName("OPId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Order__7C4F7684");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__7D439ABD");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Product__Discoun__2B0A656D");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Product__ImageId__2A164134");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Restaur__7E37BEF6");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Pcid)
                    .HasName("PK__tmp_ms_x__580221DF8627D9FD");

                entity.ToTable("ProductCategory");

                entity.Property(e => e.Pcid).HasColumnName("PCId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCa__Categ__1EA48E88");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCa__Produ__1F98B2C1");
            });

            modelBuilder.Entity<ProductMenu>(entity =>
            {
                entity.HasKey(e => e.Pmid)
                    .HasName("PK__ProductM__5C86FF463A7FE2FB");

                entity.ToTable("ProductMenu");

                entity.Property(e => e.Pmid).HasColumnName("PMId");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.ProductMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductMe__MenuI__02084FDA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductMenus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductMe__Produ__01142BA1");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .HasColumnName("E-mail");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(13);

                entity.Property(e => e.RestaurantName).HasMaxLength(30);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__Addre__44CA3770");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Restauran__Image__29221CFB");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__Manag__03F0984C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__State__CityId__04E4BC85");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("Street");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Neighbourhood)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.NeighbourhoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Street__Neighbou__05D8E0BE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
