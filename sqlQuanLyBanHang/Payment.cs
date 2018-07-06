namespace sqlQuanLiBanHang
{
    using System;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Payment")]
    public partial class Payment
    {
        [Required]
        [StringLength(20)]
        public string CustID { get; set; }

        [Required]
        [StringLength(20)]
        public string SalesPersonID { get; set; }

        [Key]
        [StringLength(20)]
        public string PaymentNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }

        public decimal? PaymentAmt { get; set; }

        [StringLength(50)]
        public string Descript { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
