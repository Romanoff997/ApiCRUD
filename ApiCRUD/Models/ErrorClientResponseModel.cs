﻿namespace ApiCRUD.Models
{
    public class ErrorClientResponseModel
    {
        public int status { get; set; }
        public string code { get; set; }
        public List<ValidationExceptions> exception { get; set; } = new List<ValidationExceptions>();


    }
}
