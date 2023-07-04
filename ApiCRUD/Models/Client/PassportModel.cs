using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Сlients_API.Models
{
    public class PassportModel
    {
        public Guid id { get; set; }
        public int series { get; set; }
        public int number { get; set; }
        public string giver { get; set; }
        public DateTime dateIssued { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
