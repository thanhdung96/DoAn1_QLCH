namespace sqlQuanLiBanHang
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("SalesOrder")]
    public partial class SalesOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesOrder()
        {
            SlsOrderDetails = new HashSet<SlsOrderDetail>();
        }

        [Key]
        [Required]
        [StringLength(20)]
		[Display(Name = "Số hóa đơn")]
        public string OrderNo { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Ngày lập")]
        public DateTime? OrderDate { get; set; }

        [Required]
        [StringLength(20)]
		[Display(Name = "Nhân viên")]
        public string IDEmployee { get; set; }

        [Required]
        [StringLength(20)]
		[Display(Name = "Khách hàng")]
        public string CustID { get; set; }

		[Display(Name = "Tổng tiền")]
        public decimal? TotalAmt { get; set; }

        [StringLength(200)]
		[Display(Name = "Mô tả")]
        public string SlsDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OverDueDate { get; set; }

		[Display(Name = "Giảm giá")]
		public decimal? OrderDisc { get; set; }

		[Display(Name = "Thuế")]
		public decimal? TaxAmt { get; set; }

		[Display(Name = "Thanh toán")]
		public decimal? Payment { get; set; }

		[Display(Name = "Nợ")]
		public decimal? Debt { get; set; }

        public decimal? VAT { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlsOrderDetail> SlsOrderDetails { get; set; }
    }
}
