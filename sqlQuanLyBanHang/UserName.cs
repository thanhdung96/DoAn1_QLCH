namespace sqlQuanLiBanHang
{
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

	[Table("UserName")]
    public partial class UserName
    {
        [Key]
        [Column("Username")]
        [StringLength(100)]
		[Display(Name="Tên đăng nhập")]
        public string Username1 { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [StringLength(200)]
		[Display(Name = "Mật khẩu")]
		public string Password_ { get; set; }

        [Required(ErrorMessage = "Không để trống")]
        [StringLength(10)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Chỉ nhập chữ - Vui lòng không nhập số!")]
		[Display(Name = "Chức vụ")]
        public string Roles { get; set; }

        [StringLength(20)]
		[Display(Name = "Nhân viên")]
        public string MaNV { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
