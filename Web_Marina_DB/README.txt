Bei vorliegendem Projekt handelt es sich um ein Übungsprojekt, das in Visual Studio erstellt wurde.

Um das Programm direkt ohne weitere Vorbereitungen starten zu können, ist ihm eine SQLite Datenbank 
beigefügt, die sich ausschließlich für den Single-User-Gebrauch eignet und somit im Arbeitsalltag 
natürlich nicht zum Einsatz käme.

Das Programm lässt sich jedoch genau so gut zum Beispiel mit einer lokalen SQL Server Datenbank über den 
in Visual Studio mitgelieferten SQL Server betreiben.

Hierzu sind einige kleine Vorbereitungen und Änderungen im Programmcode notwendig:

1. Neue Datenbank erstellen:
   Ansicht -> Server-Explorer -> Rechtsklick auf Datenverbindungen -> Neue SQL Server Datenbank erstellen
   -> Servername: (LocalDb)\MSSQLLocalDB -> Neuer Datenbankname: Marina2DB -> OK

2. Tabelle erstellen und Tabelleninhalt hinzufügen:
   Die Datei "SqlErstellungsSkript.sql" kann in Visual Studio geöffnet und direkt ausgeführt 
   werden (kleiner grüner Pfeil oben links :))

3. Im HomeController einkommentieren:

   readonly SqlServerDAL datenzugriff;
   string connectionString = configuration.GetConnectionString("DB-Verbindung"); 
   datenzugriff = new SqlServerDAL(connectionString); 

4. Im HomeController auskommentieren:

   readonly SQLiteDAL datenzugriff;
   string connectionString = configuration.GetConnectionString("SqliteVerbindung"); 
   datenzugriff = new SQLiteDAL(connectionString); 


Falls ein anderer SQL Server verwendet werden sollte, müsste der ConnectionString
für die "DB-Verbindung" in der appsettings.json noch angepasst werden.

