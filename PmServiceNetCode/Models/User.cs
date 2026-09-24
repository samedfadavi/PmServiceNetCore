namespace PmServiceNetCode.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace PmServiceNetCode.Models
    {
        [Table("tbl_User")]
        public class User
        {
            [Key]
            public int Code_User { get; set; }

            public string? Name_User { get; set; }

            public string? UserName_User { get; set; }

            public string? Password_User { get; set; }

            public int? AccessLevel { get; set; }

            public int? no_karbar { get; set; }

            public int? code_omor { get; set; }

            public int? code_bakhsh { get; set; }

            public int? Flag_N { get; set; }

            public int? code_Role { get; set; }
        }
    }
}
