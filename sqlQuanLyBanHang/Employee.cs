namespace sqlQuanLiBanHang
{
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Customers = new HashSet<Customer>();
            Payments = new HashSet<Payment>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        [Key]
        [StringLength(20)]
		[Display(Name = "Nhân viên")]
        public string IDEmployee { get; set; }

        [Required]
        [StringLength(50)]
		[Display(Name = "Tên nhân viên")]
		public string EmployeeName { get; set; }

        [StringLength(50)]
		[Display(Name = "Địa chỉ")]
		public string AddressEmployee { get; set; }

		[Display(Name = "Nợ tối đa")]
        public int? MaxDebt { get; set; }

		[Display(Name = "Nhận tối đa")]
        public int? MaxReceive { get; set; }

        [StringLength(5)]
		[Display(Name = "Trạng thái")]
        public string StatusEmployee { get; set; }

        [StringLength(200)]
		[Display(Name = "Mô tả")]
        public string DescriptionEmployee { get; set; }

        [StringLength(30)]
		[Display(Name = "Email")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
