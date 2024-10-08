using System.ComponentModel.DataAnnotations;

namespace Web_FIA44_DataAccessLayer.Models
{
    public class Article
    {
        [Display(Name ="Artikel-ID")]
        public int Aid { get; set; }
        [Required]
        [Display(Name = "Artikelbeschreibung")]
        public string Description { get; set; }
        [Required]
        [Range(0,1000,ErrorMessage ="Menge darf nur zwischen 0 und 1000 Stück liegen")]
        [Display(Name = "Menge")]
        public int Quantity { get; set; }
        [Range(0,1000,ErrorMessage ="Preis nur zwischen 0 und 1000 € wählbar")]
        [Display(Name = "Preis")]
        public decimal Price { get; set; }
        [Display(Name = "Gefahrstoff")]
        public bool IsHarzard { get; set; }

      
    }
}
