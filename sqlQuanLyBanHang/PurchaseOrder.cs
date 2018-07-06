namespace sqlQuanLiBanHang
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("PurchaseOrder")]
    public partial class PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            PurchaseOrdDetails = new HashSet<PurchaseOrdDetail>();
        }

        [Key]
        [StringLength(20)]
		[Display(Name = "Số hóa đơn")]
        public string OrderNO { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Ngày lập")]
        public DateTime? OrderDate { get; set; }

		[Display(Name = "Chiết khấu")]
		public decimal? DiscAmt { get; set; }

		[Display(Name = "Thuế")]
        public decimal? TaxAmt { get; set; }

		[Display(Name = "Tổng tiền")]
        public decimal? TotalAmt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrverdueDate { get; set; }

        public decimal? PromAmt { get; set; }

        public decimal? ComAmt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrdDetail> PurchaseOrdDetails { get; set; }
    }
}
