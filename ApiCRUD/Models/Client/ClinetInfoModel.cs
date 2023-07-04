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

        // Добавляем дополнительное свойство для хранения сериализованного массива в базе данных
        public string сhildren
        {
            get => _сhildren != null ? JsonSerializer.Serialize(_сhildren) : null;
            set => _сhildren = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<string[]>(value) : null;
        }
        //public List<string> documentIds { get; set; } = new List<string>();
        //public PassportModel passport { get; set; }
        //public LivingAddressModel livingAddress { get; set; }
        //public RegAddressModel regAddress { get; set; }
        //public List<string> jobs { get; set; } = new List<string>();
        //public int curWorkExp { get; set; }
        //public string? typeEducation { get; set; }
        //public float monIncome { get; set; }
        //public float monExpenses { get; set; }
        //public List<string> communications { get; set; } = new List<string>();
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; }

    }
}
