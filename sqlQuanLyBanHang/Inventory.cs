namespace sqlQuanLiBanHang
{
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Inventory")]
    public partial class Inventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventory()
        {
            PurchaseOrdDetails = new HashSet<PurchaseOrdDetail>();
            SlsOrderDetails = new HashSet<SlsOrderDetail>();
            StkTransDetails = new HashSet<StkTransDetail>();
            Stocks = new HashSet<Stock>();
            Vendors = new HashSet<Vendor>();
        }

        [Key]
        [StringLength(20)]
		[Display(Name = "Sản phẩm")]
        public string InvtID { get; set; }

        [Required]
        [StringLength(50)]
		[Display(Name = "Tên sản phẩm")]
        public string InvtName { get; set; }

        [StringLength(50)]
		[Display(Name = "Loại")]
        public string ClassName { get; set; }

        [Required]
        [StringLength(20)]
		[Display(Name = "Mã ĐVT")]
        public string UnitID { get; set; }

		[Display(Name = "Giá bán thùng")]
        public decimal? SalesPriceT { get; set; }

		[Display(Name = "Giá bán lẻ")]
		public decimal? SalesPriceL { get; set; }

		[Display(Name = "Thuế")]
		public decimal? SlsTax { get; set; }

		[Display(Name = "Số tồn")]
		public int? QtyStock { get; set; }

        public int? UnitRate { get; set; }

        [StringLength(200)]
		[Display(Name = "Mô tả")]
		public string InvtDescription { get; set; }

        public virtual Unit Unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrdDetail> PurchaseOrdDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlsOrderDetail> SlsOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StkTransDetail> StkTransDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
