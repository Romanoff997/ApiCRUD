using CRUD_Сlients_API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiCRUD.Models.Client
{
    public class ClientInfoModel
    {
        [Required]
        public Guid id { get; set; }
        public string name { get; set; }
        public string? surname { get; set; }
        public string? patronymic { get; set; } 
        public DateTime dob { get; set; }
        [NotMapped]
        private string[] _сhildren;

        public string сhildren
        {
            get => _сhildren != null ? JsonSerializer.Serialize(_сhildren) : null;
            set => _сhildren = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<string[]>(value) : null;
        }
        //[NotMapped]
        //private string[]? _documentIds { get; set; }
        //public string documentIds
        //{
        //    get => _documentIds != null ? JsonSerializer.Serialize(_documentIds) : null;
        //    set => _documentIds = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<string[]>(value) : null;
        //}
        public PassportModel? passport { get; set; }
        public LivingAddressModel? livingAddress { get; set; }
        public RegAddressModel? regAddress { get; set; }
        [NotMapped]
        private string[]? _jobs { get; set; }
        public string jobs
        {
            get => _jobs != null ? JsonSerializer.Serialize(_jobs) : null;
            set => _jobs = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<string[]>(value) : null;
        }
        //public int? curWorkExp { get; set; }
        //public string? typeEducation { get; set; }
        //public float? monIncome { get; set; }
        //public float? monExpenses { get; set; }
        //[NotMapped]
        //private string[]? _communications { get; set; }

        //public string communications
        //{
        //    get => _communications != null ? JsonSerializer.Serialize(_communications) : null;
        //    set => _communications = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<string[]>(value) : null;
        //}
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; }

    }
}
