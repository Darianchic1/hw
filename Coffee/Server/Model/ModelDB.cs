namespace Coffee;

using System.Data.SQLite; 
using System.Collections.Generic; 

public class SQLLitePurchaseRepository : IPurchaseRepository
{
    private readonly string _connectionString;
    private List<Purchase> purchases = new List<Purchase>();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Purchases (
            Id INTEGER PRIMARY KEY,
            Price REAL NOT NULL,
            Date DATETIME
        )";

    public SQLLitePurchaseRepository (string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
        ReadDataFromDatabase();
    }

    private void ReadDataFromDatabase()
    {
        purchases = GetAllPurchases();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString); 
        Console.WriteLine("База данных :  " + _connectionString + " создана");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection);
        command.ExecuteNonQuery();
             
        
    }

    public List<Purchase> GetAllPurchases()
    {
        List<Purchase> purchases = new List<Purchase>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Purchases";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Purchase purchase = new Purchase(Convert.ToInt32(reader["Id"]),Convert.ToDouble(reader["Price"]),(reader["Date"]).ToString()); 
                        purchases.Add(purchase);
                    }
                }
            }
        }
        return purchases;
    }

    public Purchase GetPurchaseById(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Purchases WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Purchase purchase = new Purchase(Convert.ToInt32(reader["Id"]),Convert.ToDouble(reader["Price"]),(reader["Date"]).ToString());
                        return  purchase;
                    }
                    return null;
                }
            }
        }
    }

    public void AddPurchase(Purchase purchase)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Purchases (Id, Price, Date) VALUES (@Id, @Price, @Date)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", purchase.Id);
                command.Parameters.AddWithValue("@Price", purchase.Price);
                command.Parameters.AddWithValue("@Date", purchase.Date);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeletePurchase(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Purchases WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}

