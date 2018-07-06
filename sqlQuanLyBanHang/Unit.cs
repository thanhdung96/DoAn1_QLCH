namespace sqlQuanLiBanHang
{
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Unit")]
    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
            Inventories = new HashSet<Inventory>();
            PurchaseOrdDetails = new HashSet<PurchaseOrdDetail>();
            SlsOrderDetails = new HashSet<SlsOrderDetail>();
            Stocks = new HashSet<Stock>();
        }

        [StringLength(20)]
		[Display(Name = "Mã ĐVT")]
        public string UnitID { get; set; }

        [Required]
        [StringLength(50)]
		[Display(Name = "ĐVT")]
        public string UnitName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrdDetail> PurchaseOrdDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlsOrderDetail> SlsOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
