namespace sqlQuanLiBanHang
{
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("PurchaseOrdDetail")]
    public partial class PurchaseOrdDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
		[Display(Name = "Số hóa đơn")]
        public string OrderNO { get; set; }

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
        public int? Qty { get; set; }

		[Display(Name = "Giá bán")]
        public int? PurchasePrice { get; set; }

        //public int? DiscAmt { get; set; }

		[Display(Name = "Thành tiền")]
        public int? Amount { get; set; }

        public decimal? QtyProm { get; set; }

        public decimal? QtyPromAmt { get; set; }

        public decimal? AmtProm { get; set; }

        //public decimal? TaxAmt { get; set; }

        public virtual Inventory Inventory { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual Unit Unit { get; set; }
		
		[Display(Name = "Chiết khấu")]
        public decimal? DiscAmt1 { get; set; }
		
		[Display(Name = "Thuế")]
        public decimal? TaxAmt1 { get; set; }

        public int? DiscAmt { get; set; }
        public decimal? TaxAmt { get; set; }
    }
}
