namespace Web_Marina_DB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // die Web-App wird/ ist eine MVC-Anwendung:
            // hier wird die Methode not-valid-value-message �berschrieben

            builder.Services.AddControllersWithViews().AddMvcOptions
            (
                options => options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Bitte geben Sie einen g�ltigen Zahlenwert ein.")
            );

            var app = builder.Build();

            // wwwroot-Ordner von au�en zug�nglich machen:
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
