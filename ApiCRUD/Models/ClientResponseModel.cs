using ApiCRUD.Models.Client;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Сlients_API.Models
{
    public class ClientResponseModel
    {

        public int limit { get; set; }
        public int page { get; set; }
        public int total { get; set; }
        public ClientInfoViewModel[] data { get; set; }
    }
}
