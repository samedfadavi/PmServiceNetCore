using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmServiceNetCode.Models
{
    [Table("Tbl_Farayand")]
    public class TblFarayand
    {
        [Key]
        public int ID { get; set; }

        public string? Onvan { get; set; }

        public int? Grouh { get; set; }

        public string? NameSabtKonande { get; set; }

        public string? TarikhSabt { get; set; }

        public string? FilePath { get; set; }
    }
}
