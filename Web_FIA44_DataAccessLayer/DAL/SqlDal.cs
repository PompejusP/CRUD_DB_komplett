using Microsoft.Data.SqlClient;
using Web_FIA44_DataAccessLayer.Models;

namespace Web_FIA44_DataAccessLayer.DAL
{
    public class SqlDal : IAccessable
    {
        private readonly SqlConnection conn;
        public SqlDal(string connString)
        {
            conn = new SqlConnection(connString);
        }
        
        public List<Article> GetAllArticles()
        {
            //SQL-String festlegen um alle Artikel aus der Datenbank zu holen
            string SelectAll = "SELECT * FROM Article";
            //SqlCommand erstellen
            SqlCommand selectCmd = new SqlCommand(SelectAll, conn);
            //Verbindung zur Datenbank öffnen
            conn.Open();
            //SQLAnweisung gegen die Datenbank ausführen
            SqlDataReader reader = selectCmd.ExecuteReader();
            //Liste für die Artikel erstellen
            List<Article> AllArticlesList = new List<Article>();
            while (reader.Read())
            {
                //Artikel aus der Datenbank lesen
                Article article = new Article();
                article.Aid = (int)reader["Aid"];
                article.Description = reader["Description"].ToString();
                article.Quantity = (int)reader["Quantity"];
                article.Price = (decimal)reader["Price"];
                article.IsHarzard = (bool)reader["IsHarzard"];
                //Artikel in die Liste einfügen
                AllArticlesList.Add(article);
            }
            //Verbindung zur Datenbank schließen
            conn.Close();

            return AllArticlesList;
        }

        public Article GetArticleById(int Aid)
        {
            //SQL-String festlegen um einen Artikel aus der Datenbank zu holen
            string SelectById = "SELECT * FROM Article WHERE Aid = @Aid";
            //SqlCommand erstellen
            SqlCommand selectCmd = new SqlCommand(SelectById, conn);
            //Parameter für die Artikelnummer hinzufügen
            selectCmd.Parameters.AddWithValue("@Aid", Aid);
            //Verbindung zur Datenbank öffnen
            conn.Open();
            //SQLAnweisung gegen die Datenbank ausführen
            SqlDataReader reader = selectCmd.ExecuteReader();
            //Artikel aus der Datenbank lesen
            Article article = new Article();
            if (reader.Read())
            {
                article.Aid = (int)reader["Aid"];
                article.Description = reader["Description"].ToString();
                article.Quantity = (int)reader["Quantity"];
                article.Price = (decimal)reader["Price"];
                article.IsHarzard = (bool)reader["IsHarzard"];
            }
            //Verbindung zur Datenbank schließen
            conn.Close();
            return article;
            //throw new NotImplementedException();
		}

		

        public int InsertArticle(Article article)
        {
            //SQL-String festlegen um einen Artikel in die Datenbank einzufügen , output inserted.Aid VALUE gibt die Artikelnummer zurück
            string InsertQuery = "INSERT INTO Article (Description, Quantity, Price, IsHarzard) output inserted.Aid VALUES (@Description, @Quantity, @Price, @IsHarzard)";
            //SqlCommand erstellen
            SqlCommand insertCmd = new SqlCommand(InsertQuery, conn);
            //Parameter für die Beschreibung hinzufügen
            insertCmd.Parameters.AddWithValue("@Description", article.Description);
            //Parameter für die Menge hinzufügen
            insertCmd.Parameters.AddWithValue("@Quantity", article.Quantity);
            //Parameter für den Preis hinzufügen
            insertCmd.Parameters.AddWithValue("@Price", article.Price);
            //Parameter für die Gefährlichkeit hinzufügen
            insertCmd.Parameters.AddWithValue("@IsHarzard", article.IsHarzard);
            //Verbindung zur Datenbank öffnen
            conn.Open();
            //SQLAnweisung gegen die Datenbank ausführen
            int newAid = (int)insertCmd.ExecuteScalar();
            //Verbindung zur Datenbank schließen
            conn.Close();
            //Die Anzahl der eingefügten Zeilen zurückgeben
            return newAid;
           
        }

        public bool UpdateArticle(Article article)
        {
            //SQL-String festlegen um einen Artikel zu aktualisieren bzw. zu ändern
            string UpdateQuery = "UPDATE Article SET Description = @Description, Quantity = @Quantity, Price = @Price, IsHarzard = @IsHarzard WHERE Aid = @Aid";
            //SqlCommand erstellen
            SqlCommand updateCmd = new SqlCommand(UpdateQuery, conn);
            //Parameter für die Artikelnummer hinzufügen
            updateCmd.Parameters.AddWithValue("@Aid", article.Aid);
            //Parameter für die Beschreibung hinzufügen
            updateCmd.Parameters.AddWithValue("@Description", article.Description);
            //Parameter für die Menge hinzufügen
            updateCmd.Parameters.AddWithValue("@Quantity", article.Quantity);
            //Parameter für den Preis hinzufügen
            updateCmd.Parameters.AddWithValue("@Price", article.Price);
            //Parameter für die Gefährlichkeit hinzufügen
            updateCmd.Parameters.AddWithValue("@IsHarzard", article.IsHarzard);
            //Verbindung zur Datenbank öffnen
            conn.Open();
            //SQLAnweisung gegen die Datenbank ausführen
            int rows = updateCmd.ExecuteNonQuery();
            //Verbindung zur Datenbank schließen
            conn.Close();
            //Wenn genau eine Zeile aktualisiert wurde, dann gebe true zurück ansonsten false
            return rows == 1;
        }
        public bool DeleteArticleById(int Aid)
        {
            //SQL-String festlegen um einen Artikel aus der Datenbank zu löschen
            //Der Artikel wird anhand der Artikelnummer gelöscht
            string Deletestring = "DELETE FROM Article WHERE Aid = @Aid";
            //SqlCommand erstellen
            SqlCommand deleteCmd = new SqlCommand(Deletestring, conn);
            //Parameter für die Artikelnummer hinzufügen
            deleteCmd.Parameters.AddWithValue("@Aid", Aid);
            //Verbindung zur Datenbank öffnen
            conn.Open();
            //SQLAnweisung gegen die Datenbank ausführen
            int rows = deleteCmd.ExecuteNonQuery();
            //Verbindung zur Datenbank schließen
            conn.Close();
            //Wenn genau eine Zeile gelöscht wurde, dann gebe true zurück ansonsten false
            return rows == 1;
            
        }
    }
}
