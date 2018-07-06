namespace sqlQuanLiBanHang
{
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("SlsOrderDetail")]
    public partial class SlsOrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
		[Display(Name="Số hóa đơn")]
        public string OrderNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
		[Display(Name = "Sản phẩm")]
        public string InvtID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
		[Display(Name = "ĐVT")]
        public string UnitID { get; set; }

		[Display(Name = "Số lượng")]
		[Range(0,10000, ErrorMessage="Số lượng phải lớn hơn 0")]
        public int? Qty { get; set; }

		[Display(Name = "Giá bán")]
        public decimal? SalesPrice { get; set; }

        //public decimal? DiscAmt { get; set; }

        //public decimal? TaxAmt { get; set; }

		[Display(Name = "Thành tiền")]
		public decimal? Amount { get; set; }

		[Display(Name = "Chiết khấu")]
		public decimal? Discount { get; set; }

        public virtual Inventory Inventory { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual Unit Unit { get; set; }
        public decimal? DiscAmt1 { get; set; }

		[Display(Name = "Thuế 1")]
        public decimal? TaxAmt1 { get; set; }
        public decimal? DiscAmt { get; set; }
        public decimal? TaxAmt { get; set; }
    }
}
