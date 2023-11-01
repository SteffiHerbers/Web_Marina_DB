using System.ComponentModel.DataAnnotations;

namespace Web_Marina_DB.ViewModels
{
    public class UpdateViewModel
    {
        public int BID { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie einen Namen ein.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie die Länge des Bootes ein.")]
        public double? Laenge { get; set; }

        public int? Motorleistung { get; set; }

        public string Segler { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie einen Wert ein.")]
        public double? Tiefgang { get; set; }

        public string? Baujahr { get; set; }

        public IFormFile? Bild { get; set; }

        public IFormFile? neuesBild { get; set; }

        public string? Bildname { get; set; }

        // Für die Checkbox: "Bild verwenden"
        public string bildVerwenden { get; set; }
    }
}
