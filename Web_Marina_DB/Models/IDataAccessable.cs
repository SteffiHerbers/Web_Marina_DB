namespace Web_Marina_DB.Models
{
    public interface IDataAccessable
    {
        // CRUD Methoden definieren

        // Create
        // Im int ist der Primärschlüssel gespeichert. Habe ich einen Wert/ Primärschlüssel,
        // weiß ich, dass es geklappt hat. Bekomme ich eine -1 zurück, weiß ich,
        // dass es nicht geklappt hat. Außerdem kann ich mit dem Primärschlüssel weiterarbeiten. 

        int CreateBoat(Boot boot);

        // Alle Boote auslesen

        List<Boot> GetAllBoats();

        // Einzelnes Boot auslesen

        Boot GetBoatById(int id);

        // Update Boot

        bool UpdateBoat(Boot boot);

        // Delete Boot by ID

        bool DeleteBoatById(int id);
        
    }
}
