using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class PageViewModel
    {
        [Required(ErrorMessage = "Page is required")]
        [Range(1, int.MaxValue, ErrorMessage ="Page must be a positive number")]      
        public int Page { get; set; }

        [Required(ErrorMessage = "Pagesize is required")]
        [Range(5,30, ErrorMessage = "Pagesize can not be less then 5 or greater then 30")]
        public int PageSize { get; set; }        
    }
}