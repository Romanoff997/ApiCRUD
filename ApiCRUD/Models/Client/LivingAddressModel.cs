namespace ApiCRUD.Models.Client
{
    public class LivingAddressModel
    {
        public Guid? id { get; set; }
        public int? zipCode { get; set; }
        public string? country { get; set; }
        public string? region { get; set; }
        public string? city { get; set; }
        public string? street { get; set; }
        public string? house { get; set; }
        public string? apartment { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
