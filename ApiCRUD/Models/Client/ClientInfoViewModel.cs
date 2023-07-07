using CRUD_Сlients_API.Models;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCRUD.Models.Client
{
    public class ClientInfoViewModel
    {
        //[Required]
        public Guid id { get; set; }
       // [Required]
        public string name { get; set; }
        //[Required]
        public string? surname { get; set; }
        //[Required]
        public string? patronymic { get; set; }
        public DateTime dob { get; set; }
        public string[]? children { get; set; }
        //public string[]? documentIds { get; set; }
        //[Required]
        //public PassportModel? passport { get; set; }
        //////[Required]
        //public LivingAddressModel livingAddress { get; set; }
        ////[Required]
        //public RegAddressModel regAddress { get; set; }

        public string[]? jobs { get; set; }

        //public int curWorkExp { get; set; }
        //public string? typeEducation { get; set; }
        //public float monIncome { get; set; }
        //public float monExpenses { get; set; }
        //public string[] communications { get; set; }


        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; }
    }
}
