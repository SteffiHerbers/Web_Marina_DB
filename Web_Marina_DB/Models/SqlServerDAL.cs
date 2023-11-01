using Microsoft.Data.SqlClient;

namespace Web_Marina_DB.Models
{
    public class SqlServerDAL: IDataAccessable
    {
        readonly SqlConnection connection;

        // Konstruktor

        public SqlServerDAL(string verbindung)
        {
            connection = new SqlConnection(verbindung);
        }

        // CRUD Methoden auscodieren

        // GetAllBoats

        public List<Boot> GetAllBoats()
        {
            SqlCommand selectCmd = new SqlCommand("SELECT * FROM Boot", connection);
            List<Boot> bootsListe = new List<Boot>();

            try
            {
                connection.Open();
                SqlDataReader reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    Boot boot = new Boot
                    {
                        BID = Convert.ToInt32(reader["BID"]),
                        Name = reader["Name"].ToString(),
                        Laenge = Convert.ToDouble(reader["Laenge"]),
                        Motorleistung = reader["Motorleistung"] == DBNull.Value ? null : Convert.ToInt32(reader["Motorleistung"]),
                        IstSegler = Convert.ToBoolean(reader["IstSegler"]),
                        Tiefgang = Convert.ToDouble(reader["Tiefgang"]),
                        Baujahr = reader[6] == DBNull.Value ? null : reader["Baujahr"].ToString(),
                        Bildname = reader[7] == DBNull.Value ? null : reader["Bildname"].ToString()
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

        // Get Boat by ID

        public Boot GetBoatById(int id)
        {
            SqlCommand SelectCmd = new SqlCommand("SELECT * FROM Boot WHERE BID = @BID", connection);
            SelectCmd.Parameters.AddWithValue("@BID", id);
            Boot boot = new Boot();

            try
            {
                connection.Open();
                SqlDataReader reader = SelectCmd.ExecuteReader();

                while (reader.Read())
                {
                    boot.BID = Convert.ToInt32(reader["BID"]);
                    boot.Name = reader["Name"].ToString();
                    boot.Laenge = Convert.ToDouble(reader["Laenge"]);
                    boot.Motorleistung = reader[3] == DBNull.Value ? null : Convert.ToInt32(reader["Motorleistung"]);
                    boot.IstSegler = Convert.ToBoolean(reader["IstSegler"]);
                    boot.Tiefgang = Convert.ToDouble(reader["Tiefgang"]);
                    boot.Baujahr = reader[6] == DBNull.Value ? null : reader["Baujahr"].ToString();
                    boot.Bildname = reader[7] == DBNull.Value ? null : reader["Bildname"].ToString();
                }
            }
            catch (Exception)
            {
                boot = null;
            }
            finally
            {
                connection.Close();
            }
            return boot;
        }

        // Create Boat

        public int CreateBoat(Boot boot)
        {
            string insertString = "INSERT INTO Boot(Name, Laenge, Motorleistung, IstSegler, Tiefgang, Baujahr, Bildname) OUTPUT inserted.BID VALUES(@Name, @Laenge, @Motorleistung, @IstSegler, @Tiefgang, @Baujahr, @Bildname)";
            SqlCommand insertCmd = new SqlCommand(insertString, connection);

            insertCmd.Parameters.AddWithValue("@Name", boot.Name);
            insertCmd.Parameters.AddWithValue("@Laenge", boot.Laenge);

            // Motorleistung kann null sein, deshalb mit null abgleichen:
            // ?? -> null-coalescing-operator/ null-koaleszierender Operator
            // Dieser Operator legt einen Standardwert fest, falls der Ausdruck null ist
            // Ausdruck auf der linken Seite des Operators wird ausgewertet
            // wenn der Ausdruck nicht null ist, wird er zurückgegeben
            // wenn der Ausdruck null ist, wird der Ausdruck auf der rechten Seite des Operators zurückgegeben

            insertCmd.Parameters.AddWithValue("@Motorleistung", boot.Motorleistung ?? (object)DBNull.Value);
            insertCmd.Parameters.AddWithValue("@IstSegler", boot.IstSegler);
            insertCmd.Parameters.AddWithValue("@Tiefgang", boot.Tiefgang);

            insertCmd.Parameters.AddWithValue("@Baujahr", string.IsNullOrEmpty(boot.Baujahr) ? (object)DBNull.Value : boot.Baujahr);
            insertCmd.Parameters.AddWithValue("@Bildname", string.IsNullOrEmpty(boot.Bildname) ? (object)DBNull.Value : boot.Bildname);

            connection.Open();
            int PK = Convert.ToInt32(insertCmd.ExecuteScalar());
            connection.Close();
            return PK;
        }

        // Update Boot

        public bool UpdateBoat(Boot boot)
        {
            string updateString = "UPDATE Boot SET Name = @Name, Laenge = @Laenge, Motorleistung = @Motorleistung, IstSegler = @IstSegler, Tiefgang = @Tiefgang, Baujahr = @Baujahr, Bildname = @Bildname WHERE BID = @BID";
            SqlCommand updateCmd = new SqlCommand(updateString, connection);

            updateCmd.Parameters.AddWithValue("@BID", boot.BID);
            updateCmd.Parameters.AddWithValue("@Name", boot.Name);
            updateCmd.Parameters.AddWithValue("@Laenge", boot.Laenge);
            updateCmd.Parameters.AddWithValue("@Motorleistung", boot.Motorleistung ?? (object)DBNull.Value);
            updateCmd.Parameters.AddWithValue("@IstSegler", boot.IstSegler);
            updateCmd.Parameters.AddWithValue("@Tiefgang", boot.Tiefgang);
            updateCmd.Parameters.AddWithValue("@Baujahr", string.IsNullOrEmpty(boot.Baujahr) ? (object)DBNull.Value : boot.Baujahr);
            updateCmd.Parameters.AddWithValue("@Bildname", string.IsNullOrEmpty(boot.Bildname) ? (object)DBNull.Value : boot.Bildname);

            connection.Open();
            int rowsAffected = updateCmd.ExecuteNonQuery();
            connection.Close();

            return rowsAffected > 0; // bool => true, wenn mehr als 0 Zeilen betroffen sind
        }

        // Delete Boat by ID

        public bool DeleteBoatById(int id)
        {
            string deleteString = "DELETE FROM Boot WHERE BID = @BID";
            SqlCommand deleteCmd = new SqlCommand(deleteString, connection);

            deleteCmd.Parameters.AddWithValue("@BID", id);

            connection.Open();
            int rowsAffected = deleteCmd.ExecuteNonQuery();
            connection.Close();

            return rowsAffected > 0;
        }
    }
}
