namespace sqlQuanLiBanHang
{
	using System.Data.Entity;

	public partial class BH : DbContext
	{
		public BH()
			: base("name=BH")
		{
		}

		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Inventory> Inventories { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<PurchaseOrdDetail> PurchaseOrdDetails { get; set; }
		public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
		public virtual DbSet<SalesOrder> SalesOrders { get; set; }
		public virtual DbSet<SlsOrderDetail> SlsOrderDetails { get; set; }
		public virtual DbSet<StkTransDetail> StkTransDetails { get; set; }
		public virtual DbSet<Stock> Stocks { get; set; }
		public virtual DbSet<StockTransfer> StockTransfers { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<Unit> Units { get; set; }
		public virtual DbSet<Vendor> Vendors { get; set; }
		//public virtual DbSet<UserName> UserNames { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
				.Property(e => e.Amount)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Customer>()
				.Property(e => e.OverDueAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Customer>()
				.Property(e => e.DueAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Customer>()
				.HasMany(e => e.Payments)
				.WithRequired(e => e.Customer)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Customer>()
				.HasMany(e => e.SalesOrders)
				.WithRequired(e => e.Customer)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Employee>()
				.HasMany(e => e.Customers)
				.WithRequired(e => e.Employee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Employee>()
				.HasMany(e => e.Payments)
				.WithRequired(e => e.Employee)
				.HasForeignKey(e => e.SalesPersonID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Employee>()
				.HasMany(e => e.SalesOrders)
				.WithRequired(e => e.Employee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Inventory>()
				.Property(e => e.SalesPriceT)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Inventory>()
				.Property(e => e.SalesPriceL)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Inventory>()
				.Property(e => e.SlsTax)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Inventory>()
				.HasMany(e => e.PurchaseOrdDetails)
				.WithRequired(e => e.Inventory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Inventory>()
				.HasMany(e => e.SlsOrderDetails)
				.WithRequired(e => e.Inventory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Inventory>()
				.HasMany(e => e.StkTransDetails)
				.WithRequired(e => e.Inventory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Inventory>()
				.HasMany(e => e.Stocks)
				.WithRequired(e => e.Inventory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Inventory>()
				.HasMany(e => e.Vendors)
				.WithRequired(e => e.Inventory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Payment>()
				.Property(e => e.PaymentAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrdDetail>()
				.Property(e => e.QtyProm)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrdDetail>()
				.Property(e => e.QtyPromAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrdDetail>()
				.Property(e => e.AmtProm)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrdDetail>()
				.Property(e => e.TaxAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.Property(e => e.DiscAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.Property(e => e.TaxAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.Property(e => e.TotalAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.Property(e => e.PromAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.Property(e => e.ComAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<PurchaseOrder>()
				.HasMany(e => e.PurchaseOrdDetails)
				.WithRequired(e => e.PurchaseOrder)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.TotalAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.OrderDisc)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.TaxAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.Payment)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.Debt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.Property(e => e.VAT)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SalesOrder>()
				.HasMany(e => e.SlsOrderDetails)
				.WithRequired(e => e.SalesOrder)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SlsOrderDetail>()
				.Property(e => e.SalesPrice)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SlsOrderDetail>()
				.Property(e => e.DiscAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SlsOrderDetail>()
				.Property(e => e.TaxAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SlsOrderDetail>()
				.Property(e => e.Amount)
				.HasPrecision(18, 0);

			modelBuilder.Entity<SlsOrderDetail>()
				.Property(e => e.Discount)
				.HasPrecision(18, 0);

			modelBuilder.Entity<StkTransDetail>()
				.Property(e => e.Amount)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Stock>()
				.HasMany(e => e.StockTransfers)
				.WithRequired(e => e.Stock)
				.HasForeignKey(e => e.FromStockID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Stock>()
				.HasMany(e => e.StockTransfers1)
				.WithRequired(e => e.Stock1)
				.HasForeignKey(e => e.ToStockID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<StockTransfer>()
				.Property(e => e.TotalAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<StockTransfer>()
				.HasMany(e => e.StkTransDetails)
				.WithRequired(e => e.StockTransfer)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Unit>()
				.HasMany(e => e.Inventories)
				.WithRequired(e => e.Unit)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Unit>()
				.HasMany(e => e.PurchaseOrdDetails)
				.WithRequired(e => e.Unit)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Unit>()
				.HasMany(e => e.SlsOrderDetails)
				.WithRequired(e => e.Unit)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Unit>()
				.HasMany(e => e.Stocks)
				.WithRequired(e => e.Unit)
				.HasForeignKey(e => e.UnitID_Stock)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Vendor>()
				.Property(e => e.DueAmt)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Vendor>()
				.Property(e => e.Amount)
				.HasPrecision(18, 0);
		}

		public System.Data.Entity.DbSet<sqlQuanLiBanHang.UserName> UserNames { get; set; }
	}
}
