using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Entities.Models
{
    [Table("Products")]
    public class Product

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Must Be Valid")]
        [MaxLength(70)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description Must Be Valid")]
        [MaxLength(150)]
        public string Description { get; set; }


        [Display(Name = "Image")]
        [ValidateNever]
        public string? Img { get; set; }


        [Required(ErrorMessage = "Price Must Be Valid")]
        public decimal Price { get; set; }


        [Required]
        [ForeignKey(nameof(Category))]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }


        [ValidateNever]
        public virtual Category Category { get; set; }

    }
}
