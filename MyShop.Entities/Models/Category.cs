

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyShop.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Must Be Valid")]
        [MaxLength(70)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description Must Be Valid")]
        [MaxLength(150)]
        public string Description { get; set; }


        [Display(Name = "Created Time")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;



        [JsonIgnore]  // Prevents cycle
        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
