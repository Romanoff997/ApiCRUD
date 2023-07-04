using System.ComponentModel.DataAnnotations;

namespace CRUD_Сlients_API.Models
{
    public class ClientRequestModel
    {

        public string sortBy { get; set; }
        public string sortDir { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string search { get; set; }

    }
}
