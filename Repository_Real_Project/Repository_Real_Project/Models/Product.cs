using System.ComponentModel.DataAnnotations;

namespace Repository_Real_Project.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
    }
}
