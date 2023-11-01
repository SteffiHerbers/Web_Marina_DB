namespace Web_Marina_DB.Models
{
    public class Boot
    {
        public int BID { get; set; }
        public string Name { get; set; }
        public double Laenge { get; set; }
        public int? Motorleistung { get; set; }
        public bool IstSegler { get; set; }
        public double Tiefgang { get; set; }
        public string? Baujahr { get; set; }
        public string? Bildname { get; set; }
    }
}
