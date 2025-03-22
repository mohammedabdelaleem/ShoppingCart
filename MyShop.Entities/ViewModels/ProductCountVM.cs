using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.ViewModels
{
	public class ProductCountVM
	{
		public Product Product { get; set; }

		[Range(1,100,ErrorMessage ="Enter A Valid Value Between 1 And 100")]
        public int Count { get; set; }
    }
}
