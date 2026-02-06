namespace PmServiceNetCode.Models
{
    public class Form
    {
        public int IdForm { get; set; }          // ID_Form
        public string? Onvan { get; set; }       // nvarchar(500)
        public string? Description { get; set; } // nvarchar(500)

        public bool IsDeleted { get; set; }
    }
}
