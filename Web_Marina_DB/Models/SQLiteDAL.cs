using Microsoft.Data.Sqlite;

namespace Web_Marina_DB.Models
{
    public class SQLiteDAL: IDataAccessable
    {
        readonly SqliteConnection connection;

        // Konstruktor

        public SQLiteDAL(string verbindung)
        {
            connection = new SqliteConnection(verbindung);
        }

        // CRUD Methoden auscodieren

        // GetAllBoats

        public List<Boot> GetAllBoats()
        {
            SqliteCommand selectCmd = new SqliteCommand("SELECT * FROM Boot", connection);
            List<Boot> bootsListe = new List<Boot>();

            try
            {
                connection.Open();
                SqliteDataReader reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    Boot boot = new Boot
                    {
                        BID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Laenge = reader.GetDouble(2),
                        Motorleistung = reader[3] == DBNull.Value ? null : reader.GetInt32(3),
                        IstSegler = reader.GetBoolean(4), // ggf. konvertieren?
                        Tiefgang = reader.GetDouble(5),
                        Baujahr = reader[6] == DBNull.Value ? null : reader[6].ToString(),
                        Bildname = reader[7] == DBNull.Value ? null : reader[7].ToString()
                    };

                    bootsListe.Add(boot);
                }
            }
            catch (Exception)
            {
                bootsListe = null;
            }

            finally
            {
                connection.Close();
            }
            return bootsListe;
        }

        public Boot GetBoatById(int id)
        {
            List<Boot> bootsListe = GetAllBoats();
            return bootsListe.FirstOrDefault(boot => boot.BID == id);
        }


        public int CreateBoat(Boot boot)
        {
            SqliteCommand insertCmd = new SqliteCommand("INSERT INTO Boot (Name, Laenge, Motorleistung, IstSegler, Tiefgang, Baujahr, Bildname) VALUES (@Name, @Laenge, @Motorleistung, @IstSegler, @Tiefgang, @Baujahr, @Bildname)", connection);

            insertCmd.Parameters.AddWithValue("@Name", boot.Name);
            insertCmd.Parameters.AddWithValue("@Laenge", boot.Laenge);
            insertCmd.Parameters.AddWithValue("@Motorleistung", boot.Motorleistung == null ? DBNull.Value : boot.Motorleistung);
            insertCmd.Parameters.AddWithValue("@IstSegler", boot.IstSegler);
            insertCmd.Parameters.AddWithValue("@Tiefgang", boot.Tiefgang);
            insertCmd.Parameters.AddWithValue("@Baujahr", boot.Baujahr == null ? DBNull.Value : boot.Baujahr);
            insertCmd.Parameters.AddWithValue("@Bildname", boot.Bildname == null ? DBNull.Value : boot.Bildname);

            SqliteCommand selectCmd = new SqliteCommand("SELECT last_insert_rowid()", connection);

            connection.Open();

            int Anzahl = insertCmd.ExecuteNonQuery();

            if (Anzahl == 1)
            {
                int PK = Convert.ToInt32(selectCmd.ExecuteScalar());
                connection.Close();
                return PK;
            }
            else
            {
                connection.Close();
                return -1;
            }
        }

        public bool UpdateBoat(Boot boot)
        {
            SqliteCommand updateCmd = new SqliteCommand("UPDATE Boot SET Name = @Name, Laenge = @Laenge, Motorleistung = @Motorleistung, IstSegler = @IstSegler, Tiefgang = @Tiefgang, Baujahr = @Baujahr, Bildname = @Bildname WHERE BID = @BID", connection);

            updateCmd.Parameters.AddWithValue("@BID", boot.BID);
            updateCmd.Parameters.AddWithValue("@Name", boot.Name);
            updateCmd.Parameters.AddWithValue("@Laenge", boot.Laenge);
            updateCmd.Parameters.AddWithValue("@Motorleistung", boot.Motorleistung == null ? DBNull.Value : boot.Motorleistung);
            updateCmd.Parameters.AddWithValue("@IstSegler", boot.IstSegler);
            updateCmd.Parameters.AddWithValue("@Tiefgang", boot.Tiefgang);
            updateCmd.Parameters.AddWithValue("@Baujahr", boot.Baujahr == null ? DBNull.Value : boot.Baujahr);
            updateCmd.Parameters.AddWithValue("@Bildname", boot.Bildname == null ? DBNull.Value : boot.Bildname);

            connection.Open();
            int rowsAffected = updateCmd.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }


        public bool DeleteBoatById(int id)
        {
            SqliteCommand deleteCmd = new SqliteCommand("DELETE FROM Boot WHERE BID = @BID", connection);
            deleteCmd.Parameters.AddWithValue("@BID", id);
            connection.Open();

            bool result = deleteCmd.ExecuteNonQuery() > 0;
            connection.Close();
            return result;
        }
    }
}
