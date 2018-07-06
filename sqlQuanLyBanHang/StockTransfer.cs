namespace sqlQuanLiBanHang
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("StockTransfer")]
    public partial class StockTransfer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockTransfer()
        {
            StkTransDetails = new HashSet<StkTransDetail>();
        }

        [Key]
        [StringLength(20)]
        public string TransID { get; set; }

        [Required]
        [StringLength(20)]
        public string FromStockID { get; set; }

        [Required]
        [StringLength(20)]
        public string ToStockID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransDate { get; set; }

        public decimal? TotalAmt { get; set; }

        [StringLength(50)]
        public string Descript { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StkTransDetail> StkTransDetails { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual Stock Stock1 { get; set; }
    }
}
