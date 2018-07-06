namespace sqlQuanLiBanHang
{
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("StkTransDetail")]
    public partial class StkTransDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string TransID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string InvtID { get; set; }

        public int Qty { get; set; }

        public decimal Amount { get; set; }

        public virtual Inventory Inventory { get; set; }

        public virtual StockTransfer StockTransfer { get; set; }
    }
}
