namespace sqlQuanLiBanHang
{
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Payments = new HashSet<Payment>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        [Key]
        [StringLength(20)]
		[Display(Name = "Khách hàng")]
        public string CustID { get; set; }

        [Required]
        [StringLength(50)]
		[Display(Name = "Tên khách hàng")]
        public string CustName { get; set; }

        [StringLength(50)]
		[Display(Name = "Địa chỉ")]
        public string AddressCust { get; set; }

        [Required]
        [StringLength(20)]
		[Display(Name = "Nhân viên ")]
        public string IDEmployee { get; set; }

		[Display(Name = "SĐT")]
		public int? Phone { get; set; }

		[Display(Name = "Nợ tối đa")]
		public int? MaxDebt { get; set; }

        [StringLength(50)]
		[Display(Name = "Thời gian nợ")]
        public string TimeDebt { get; set; }

        [Required]
        [StringLength(2)]
		[Display(Name = "Trạng thái")]
        public string StatusCust { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(50)]
		[Display(Name = "Email")]
        public string Email { get; set; }

        public int? Overdue { get; set; }

        public decimal? Amount { get; set; }

        public decimal? OverDueAmt { get; set; }

        public decimal? DueAmt { get; set; }

        [StringLength(200)]
		[Display(Name = "Mô tả")]
        public string DescriptionCust { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
