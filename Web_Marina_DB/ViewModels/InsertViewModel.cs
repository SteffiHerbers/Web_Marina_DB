using System.ComponentModel.DataAnnotations;

namespace Web_Marina_DB.ViewModels
{
    public class InsertViewModel
    {
        public int BID { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie einen Namen ein.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie die Länge des Bootes ein.")]
        
        // Der double muss hier auf nullable gesetzt werden, obwohl er nicht null sein darf,
        // da wir sonst nicht die gewünschte Fehlermeldung bekommen,
        // wenn das Feld nicht ausgefüllt wird -> das gilt für Zahlenwerte,
        // mit Strings ist das kein Problem

        public double? Laenge { get; set; }

        public int? Motorleistung { get; set; }

        public string Segler { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie einen Wert ein.")]
        public double? Tiefgang { get; set; }

        public string? Baujahr { get; set; }

        public IFormFile? neuesBild { get; set; }
    }
}
