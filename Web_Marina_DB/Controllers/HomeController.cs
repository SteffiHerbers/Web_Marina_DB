using Microsoft.AspNetCore.Mvc;
using Web_Marina_DB.Models;
using Web_Marina_DB.ViewModels;

namespace Web_Marina_DB.Controllers
{
    public class HomeController : Controller
    {
        readonly IDataAccessable datenzugriff;

        readonly IWebHostEnvironment WebServerInfo;

        // Konstruktor

        public HomeController(IConfiguration configuration, IWebHostEnvironment host)
        {
            //---------------------------------------------------------------------------------
            // Konfiguration für SQL Server
            //string connectionString = configuration.GetConnectionString("DB-Verbindung");
            //datenzugriff = new SqlServerDAL(connectionString);
            //---------------------------------------------------------------------------------


            //---------------------------------------------------------------------------------
            // Konfiguration für SQLite
            string connectionString = configuration.GetConnectionString("SqliteVerbindung");
            datenzugriff = new SQLiteDAL(connectionString);
            //---------------------------------------------------------------------------------

            WebServerInfo = host;
        }

        // Index-Seite

        public IActionResult Index()
        {
            List<Boot> bootsListe = datenzugriff.GetAllBoats();

            if (bootsListe == null)
            {
                ViewBag.Fehler = "Es konnte keine Verbindung zur Datenbank hergestellt werden.";
                return View();
            }

            return View(bootsListe);
        }

        // Admin-Seite

        public IActionResult Admin()
        {
            List<Boot> bootsListe = datenzugriff.GetAllBoats();

            return View(bootsListe);
        }

        // Delete

        public IActionResult Delete(int id)
        {
            // Bild aus dem Ordner images löschen, falls Bild bzw. Bildnahme vorhanden:
            if (datenzugriff.GetBoatById(id).Bildname != null)
            {
                string bilderpfad = WebServerInfo.WebRootPath + "/images/";
                System.IO.File.Delete(bilderpfad + datenzugriff.GetBoatById(id).Bildname);
            }

            // Boot aus DB löschen:
            datenzugriff.DeleteBoatById(id);

            return RedirectToAction("Admin");
        }

        // Insert

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Insert(InsertViewModel viewModel)
        {
            // IFormFile "neuesBild" aus der Validierung nehmen, da es zu Fehlern kommen kann, wenn kein Bild ausgewählt + hochgeladen wird
            ModelState.Remove("neuesBild");

            // Validierung durchführen:
            // Wenn die Validierung fehlschlägt, wird die View mit den Validierungsfehlern erneut angezeigt.

            if (!ModelState.IsValid)
            {
                return View();
            }

            // Bild hochladen:

            string? neuerBildname = null;

            if (viewModel.neuesBild != null && viewModel.neuesBild.Length != 0 && viewModel.neuesBild.ContentType.Contains("image"))
            {
                string bilderpfad = WebServerInfo.WebRootPath + "/images/";

                neuerBildname = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.neuesBild.FileName);

                using (var stream = System.IO.File.Create(bilderpfad + neuerBildname))
                {
                    await viewModel.neuesBild.CopyToAsync(stream);
                }
            }

            // Boot in DB speichern:

            Boot boot = new Boot
            {
                Name = viewModel.Name,
                Laenge = viewModel.Laenge.Value,
                Motorleistung = viewModel.Motorleistung.HasValue ? viewModel.Motorleistung.Value : null,
                IstSegler = viewModel.Segler == "ja" ? true : false,
                Tiefgang = viewModel.Tiefgang.Value,
                Baujahr = string.IsNullOrEmpty(viewModel.Baujahr) ? null : viewModel.Baujahr,
                Bildname = string.IsNullOrEmpty(neuerBildname) ? null : neuerBildname
            };

            datenzugriff.CreateBoat(boot);

            return RedirectToAction("Admin");
        }

        // Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            // Vorbelegung der Eingabefelder mit den Daten des Bootes, das bearbeitet werden soll
            // 1. Boot aus der DB holen
            // 2. Boot an die View übergeben und
            // 3. View anzeigen

            Boot boot = datenzugriff.GetBoatById(id);

            UpdateViewModel viewModel = new UpdateViewModel
            {
                BID = boot.BID,
                Name = boot.Name,
                Laenge = boot.Laenge,
                Motorleistung = boot.Motorleistung,
                Segler = boot.IstSegler ? "ja" : "nein",
                Tiefgang = boot.Tiefgang,
                Baujahr = boot.Baujahr,
                Bildname = boot.Bildname
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel viewModel)
        {
            // IFormFiles "Bild" und "neuesBild" aus der Validierung nehmen
            ModelState.Remove("Bild");
            ModelState.Remove("neuesBild");

            // Validierung durchführen:

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Checkbox "Bild verwenden" auswerten:

            string bilderpfad = WebServerInfo.WebRootPath + "/images/";
            string? bildname = viewModel.Bildname;

            // Es gibt noch kein Bild für das Boot, der Bildname ist null

            if (bildname == null)
            {
                if (viewModel.bildVerwenden == "neuesBild")
                {
                    if (viewModel.neuesBild != null && viewModel.neuesBild.Length != 0 && viewModel.neuesBild.ContentType.Contains("image"))
                    {
                        bildname = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.neuesBild.FileName);

                        using (var stream = System.IO.File.Create(bilderpfad + bildname))
                        {
                            await viewModel.neuesBild.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        bildname = null;
                    }
                }
                else
                {
                    bildname = null;
                }
            }

            // Es gibt bereits ein Bild für das Boot, der Bildname ist nicht null
            else
            {
                switch (viewModel.bildVerwenden)
                {
                    case "keinBild":
                        // altes Bild aus dem Bilderordner löschen:
                        System.IO.File.Delete(bilderpfad + viewModel.Bildname);

                        bildname = null;
                        break;
                    case "neuesBild":
                        if (viewModel.neuesBild != null && viewModel.neuesBild.Length != 0 && viewModel.neuesBild.ContentType.Contains("image"))
                        {
                            System.IO.File.Delete(bilderpfad + viewModel.Bildname);

                            bildname = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.neuesBild.FileName);

                            using (var stream = System.IO.File.Create(bilderpfad + bildname))
                            {
                                await viewModel.neuesBild.CopyToAsync(stream);
                            }
                        }
                        else
                        {
                            bildname = viewModel.Bildname;
                        }
                        break;
                    default:
                        bildname = viewModel.Bildname;
                        break;
                }
            }

            // Boot in DB speichern:

            Boot boot = new Boot
            {
                BID = viewModel.BID,
                Name = viewModel.Name,
                Laenge = viewModel.Laenge.Value,
                Motorleistung = viewModel.Motorleistung.HasValue ? viewModel.Motorleistung.Value : null,
                // prüfen, welcher Radiobutton ausgewählt wurde, ja oder nein:
                IstSegler = viewModel.Segler == "ja" ? true : false,
                Tiefgang = viewModel.Tiefgang.Value,
                Baujahr = string.IsNullOrEmpty(viewModel.Baujahr) ? null : viewModel.Baujahr,
                Bildname = string.IsNullOrEmpty(bildname) ? null : bildname
            };

            datenzugriff.UpdateBoat(boot);

            return RedirectToAction("Admin");
        }

        // Details

        public IActionResult Details(int id)
        {
            Boot boot = datenzugriff.GetBoatById(id);
            return View(boot);
        }
    }
}
