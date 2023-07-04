using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCRUD.Models.Client
{
    public class ClientInfoViewModel
    {
        [Required]
        public Guid id { get; set; }
        public string name { get; set; }
        public string? surname { get; set; }
        public string? patronymic { get; set; }
        public DateTime dob { get; set; }
        public string[] сhildren { get; set; }
    }
}
