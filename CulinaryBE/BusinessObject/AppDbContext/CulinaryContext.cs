using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.AppDbContext
{
    public class CulinaryContext : DbContext
    {
        public CulinaryContext(DbContextOptions<CulinaryContext> options) : base(options)
        {
        }
        public CulinaryContext() { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ShipperStatus> ShipperStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryMapping> BlogCategoryMappings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductEmbedding> ProductEmbeddings { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<OrderVoucher> OrderVouchers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogSave> BlogSaves { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary keys
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<BlogCategoryMapping>()
                .HasKey(bcm => new { bcm.BlogId, bcm.CategoryId });

            // Configure unique constraints
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique()
                .HasDatabaseName("idx_role_name");

            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.PermissionName)
                .IsUnique()
                .HasDatabaseName("idx_permission_name");

            modelBuilder.Entity<Manager>()
                .HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("idx_username");

            modelBuilder.Entity<Manager>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("idx_email");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("idx_email_customer");

            modelBuilder.Entity<Voucher>()
                .HasIndex(v => v.VoucherCode)
                .IsUnique()
                .HasDatabaseName("idx_voucher_code");

            // Configure other indexes
            ConfigureIndexes(modelBuilder);

            // Configure relationships and delete behaviors
            ConfigureRelationships(modelBuilder);

            // Configure enum conversions
            ConfigureEnums(modelBuilder);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // Manager indexes
            modelBuilder.Entity<Manager>()
                .HasIndex(u => u.RoleId)
                .HasDatabaseName("idx_role_id");

            // ShipperStatus indexes
            modelBuilder.Entity<ShipperStatus>()
                .HasIndex(ss => ss.Status)
                .HasDatabaseName("idx_ship_status");

            // Category indexes
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .HasDatabaseName("idx_category_name");

            modelBuilder.Entity<BlogCategory>()
                .HasIndex(bc => bc.CategoryName)
                .HasDatabaseName("idx_category_bloc_name");

            // BlogCategoryMapping indexes
            modelBuilder.Entity<BlogCategoryMapping>()
                .HasIndex(bcm => bcm.BlogId)
                .HasDatabaseName("idx_blog_id_category");

            modelBuilder.Entity<BlogCategoryMapping>()
                .HasIndex(bcm => bcm.CategoryId)
                .HasDatabaseName("idx_category_id_blog");

            // Warehouse indexes
            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.WarehouseName)
                .HasDatabaseName("idx_warehouse_name");

            // Product indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName)
                .HasDatabaseName("idx_product_name");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId)
                .HasDatabaseName("idx_category_id");

            // ProductImage indexes
            modelBuilder.Entity<ProductImage>()
                .HasIndex(pi => pi.ProductId)
                .HasDatabaseName("idx_product_id");

            // ProductHistory indexes
            modelBuilder.Entity<ProductHistory>()
                .HasIndex(ph => ph.ProductId)
                .HasDatabaseName("idx_product_id_history");

            modelBuilder.Entity<ProductHistory>()
                .HasIndex(ph => ph.ChangedBy)
                .HasDatabaseName("idx_changed_by");

            // Stock indexes
            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.ProductId)
                .HasDatabaseName("idx_product_id_stock");

            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.WarehouseId)
                .HasDatabaseName("idx_warehouse_id");

            // StockTransaction indexes
            modelBuilder.Entity<StockTransaction>()
                .HasIndex(st => st.ProductId)
                .HasDatabaseName("idx_product_id_transaction");

            modelBuilder.Entity<StockTransaction>()
                .HasIndex(st => st.WarehouseId)
                .HasDatabaseName("idx_warehouse_id_transaction");

            modelBuilder.Entity<StockTransaction>()
                .HasIndex(st => st.ManagerId)
                .HasDatabaseName("idx_manager_id_transaction");

            // Customer indexes
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.FullName)
                .HasDatabaseName("idx_full_name");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Status)
                .HasDatabaseName("idx_customer_status");

            // CustomerAddress indexes
            modelBuilder.Entity<CustomerAddress>()
                .HasIndex(ca => ca.CustomerId)
                .HasDatabaseName("idx_customer_id_address");

            modelBuilder.Entity<CustomerAddress>()
                .HasIndex(ca => ca.IsDefault)
                .HasDatabaseName("idx_is_default");

            // Voucher indexes
            modelBuilder.Entity<Voucher>()
                .HasIndex(v => v.Status)
                .HasDatabaseName("idx_status_voucher");

            modelBuilder.Entity<Voucher>()
                .HasIndex(v => new { v.StartDate, v.EndDate })
                .HasDatabaseName("idx_date_range");

            // OrderVoucher indexes
            modelBuilder.Entity<OrderVoucher>()
                .HasIndex(ov => ov.OrderId)
                .HasDatabaseName("idx_order_id_voucher");

            modelBuilder.Entity<OrderVoucher>()
                .HasIndex(ov => ov.VoucherId)
                .HasDatabaseName("idx_voucher_id_order");

            // Cart indexes
            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.CustomerId)
                .HasDatabaseName("idx_customer_id_cart");

            // CartItem indexes
            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.CartId)
                .HasDatabaseName("idx_cart_id");

            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.ProductId)
                .HasDatabaseName("idx_product_id_cart_item");

            // Order indexes
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerId)
                .HasDatabaseName("idx_customer_id_order");

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.ShipperId)
                .HasDatabaseName("idx_shipper_id");

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderStatus)
                .HasDatabaseName("idx_order_status");

            // OrderStatusHistory indexes
            modelBuilder.Entity<OrderStatusHistory>()
                .HasIndex(osh => osh.OrderId)
                .HasDatabaseName("idx_order_id_history");

            modelBuilder.Entity<OrderStatusHistory>()
                .HasIndex(osh => osh.Status)
                .HasDatabaseName("idx_status");

            modelBuilder.Entity<OrderStatusHistory>()
                .HasIndex(osh => osh.ChangedBy)
                .HasDatabaseName("idx_changed_by_history");

            // OrderDetail indexes
            modelBuilder.Entity<OrderDetail>()
                .HasIndex(od => od.OrderId)
                .HasDatabaseName("idx_order_id");

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(od => od.ProductId)
                .HasDatabaseName("idx_product_id_order_detail");

            // ProductReview indexes
            modelBuilder.Entity<ProductReview>()
                .HasIndex(pr => pr.ProductId)
                .HasDatabaseName("idx_product_id_review");

            modelBuilder.Entity<ProductReview>()
                .HasIndex(pr => pr.CustomerId)
                .HasDatabaseName("idx_customer_id_review");

            // Blog indexes
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.CustomerId)
                .HasDatabaseName("idx_customer_id_blog");

            // BlogImage indexes
            modelBuilder.Entity<BlogImage>()
                .HasIndex(bi => bi.BlogId)
                .HasDatabaseName("idx_blog_id");

            // BlogSave indexes
            modelBuilder.Entity<BlogSave>()
                .HasIndex(bs => bs.BlogId)
                .HasDatabaseName("idx_blog_id_save");

            modelBuilder.Entity<BlogSave>()
                .HasIndex(bs => bs.CustomerId)
                .HasDatabaseName("idx_customer_id_save");

            // BlogComment indexes
            modelBuilder.Entity<BlogComment>()
                .HasIndex(bc => bc.BlogId)
                .HasDatabaseName("idx_blog_id_comment");

            modelBuilder.Entity<BlogComment>()
                .HasIndex(bc => bc.CustomerId)
                .HasDatabaseName("idx_customer_id_comment");

            modelBuilder.Entity<BlogComment>()
                .HasIndex(bc => bc.ParentCommentId)
                .HasDatabaseName("idx_parent_comment_id");
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Role-Permission many-to-many
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Manager-Role relationship
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.Role)
                .WithMany(r => r.Managers)
                .HasForeignKey(m => m.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            // ShipperStatus-Manager relationship (1:1)
            modelBuilder.Entity<ShipperStatus>()
                .HasOne(ss => ss.Manager)
                .WithOne(m => m.ShipperStatus)
                .HasForeignKey<ShipperStatus>(ss => ss.ShipperId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog-BlogCategory many-to-many through BlogCategoryMapping
            modelBuilder.Entity<BlogCategoryMapping>()
                .HasOne(bcm => bcm.Blog)
                .WithMany(b => b.BlogCategoryMappings)
                .HasForeignKey(bcm => bcm.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BlogCategoryMapping>()
                .HasOne(bcm => bcm.BlogCategory)
                .WithMany(bc => bc.BlogCategoryMappings)
                .HasForeignKey(bcm => bcm.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product-Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // ProductEmbedding-Product relationship (1:1)
            modelBuilder.Entity<ProductEmbedding>()
                .HasOne(pe => pe.Product)
                .WithOne(p => p.ProductEmbedding)
                .HasForeignKey<ProductEmbedding>(pe => pe.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductImage-Product relationship
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // BlogImage-Blog relationship
            modelBuilder.Entity<BlogImage>()
                .HasOne(bi => bi.Blog)
                .WithMany(b => b.BlogImages)
                .HasForeignKey(bi => bi.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductHistory relationships
            modelBuilder.Entity<ProductHistory>()
                .HasOne(ph => ph.Product)
                .WithMany(p => p.ProductHistories)
                .HasForeignKey(ph => ph.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductHistory>()
                .HasOne(ph => ph.ChangedByManager)
                .WithMany(m => m.ProductHistories)
                .HasForeignKey(ph => ph.ChangedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // Stock relationships
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Stocks)
                .HasForeignKey(s => s.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            // StockTransaction relationships
            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Product)
                .WithMany(p => p.StockTransactions)
                .HasForeignKey(st => st.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Warehouse)
                .WithMany(w => w.StockTransactions)
                .HasForeignKey(st => st.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Manager)
                .WithMany(m => m.StockTransactions)
                .HasForeignKey(st => st.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // CustomerAddress-Customer relationship
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAddresses)
                .HasForeignKey(ca => ca.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderVoucher relationships
            modelBuilder.Entity<OrderVoucher>()
                .HasOne(ov => ov.Order)
                .WithMany(o => o.OrderVouchers)
                .HasForeignKey(ov => ov.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderVoucher>()
                .HasOne(ov => ov.Voucher)
                .WithMany(v => v.OrderVouchers)
                .HasForeignKey(ov => ov.VoucherId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cart-Customer relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Carts)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem relationships
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipper)
                .WithMany(m => m.ShippedOrders)
                .HasForeignKey(o => o.ShipperId)
                .OnDelete(DeleteBehavior.SetNull);

            // OrderStatusHistory relationships
            modelBuilder.Entity<OrderStatusHistory>()
                .HasOne(osh => osh.Order)
                .WithMany(o => o.OrderStatusHistories)
                .HasForeignKey(osh => osh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderStatusHistory>()
                .HasOne(osh => osh.ChangedByManager)
                .WithMany(m => m.OrderStatusHistories)
                .HasForeignKey(osh => osh.ChangedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // OrderDetail relationships
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

            // ProductReview relationships
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Customer)
                .WithMany(c => c.ProductReviews)
                .HasForeignKey(pr => pr.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Blog-Customer relationship
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // BlogSave relationships
            modelBuilder.Entity<BlogSave>()
                .HasOne(bs => bs.Blog)
                .WithMany(b => b.BlogSaves)
                .HasForeignKey(bs => bs.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BlogSave>()
                .HasOne(bs => bs.Customer)
                .WithMany(c => c.BlogSaves)
                .HasForeignKey(bs => bs.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // BlogComment relationships
            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.Blog)
                .WithMany(b => b.BlogComments)
                .HasForeignKey(bc => bc.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.Customer)
                .WithMany(c => c.BlogComments)
                .HasForeignKey(bc => bc.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Self-referencing relationship for BlogComment
            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.ParentComment)
                .WithMany(bc => bc.ChildComments)
                .HasForeignKey(bc => bc.ParentCommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureEnums(ModelBuilder modelBuilder)
        {
            // Configure enum to string conversions
            modelBuilder.Entity<ShipperStatus>()
                .Property(ss => ss.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Manager>()
                .Property(m => m.Status)
                .HasConversion<string>();

            modelBuilder.Entity<StockTransaction>()
                .Property(st => st.TransactionType)
                .HasConversion<string>();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Voucher>()
                .Property(v => v.DiscountType)
                .HasConversion<string>();

            modelBuilder.Entity<Voucher>()
                .Property(v => v.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasConversion<string>();

            modelBuilder.Entity<OrderStatusHistory>()
                .Property(osh => osh.Status)
                .HasConversion<string>();
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                 new Role { RoleId = 1, RoleName = "Admin" },
                 new Role { RoleId = 2, RoleName = "Staff" },
                 new Role { RoleId = 3, RoleName = "Shipper" }
             );                
    } }
}
