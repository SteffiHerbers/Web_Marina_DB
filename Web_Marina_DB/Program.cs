namespace Web_Marina_DB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // die Web-App wird/ ist eine MVC-Anwendung:
            // hier wird die Methode not-valid-value-message überschrieben

            builder.Services.AddControllersWithViews().AddMvcOptions
            (
                options => options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Bitte geben Sie einen gültigen Zahlenwert ein.")
            );

            var app = builder.Build();

            // wwwroot-Ordner von außen zugänglich machen:
            app.UseStaticFiles(); // statische Dateien (css, js, Bilder, etc.) werden ausgeliefert

            // Routing aktivieren:
            app.UseRouting();

            // default-Route festlegen (implizit: Endpunkt (= Startpunkte) festlegen:
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            // Kestrel wird gestartet:
            app.Run();
        }
    }
}
