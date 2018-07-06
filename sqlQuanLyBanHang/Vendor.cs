namespace sqlQuanLiBanHang
{
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Vendor")]
    public partial class Vendor
    {
        [StringLength(20)]
        public string VendorID { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorName { get; set; }

        [StringLength(50)]
        public string AddressVendor { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public decimal DueAmt { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(2)]
        public string StatusVendor { get; set; }

        [StringLength(200)]
        public string DescriptionVendor { get; set; }

        [Required]
        [StringLength(20)]
        public string InvtID { get; set; }

        public virtual Inventory Inventory { get; set; }
    }
}
