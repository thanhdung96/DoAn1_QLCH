namespace sqlQuanLiBanHang
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Stock")]
    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            StockTransfers = new HashSet<StockTransfer>();
            StockTransfers1 = new HashSet<StockTransfer>();
        }

        [StringLength(20)]
        public string StockID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StockDate { get; set; }

        [StringLength(50)]
        public string NoteStock { get; set; }

        [Required]
        [StringLength(20)]
        public string InvtID { get; set; }

        [Required]
        [StringLength(20)]
        public string UnitID_Stock { get; set; }

        public int? Quantity { get; set; }

        public virtual Inventory Inventory { get; set; }

        public virtual Unit Unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockTransfer> StockTransfers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockTransfer> StockTransfers1 { get; set; }
    }
}
