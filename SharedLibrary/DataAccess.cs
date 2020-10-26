using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    
    public static class DataAccess
    {
        private static readonly string connectionString =
        @"Server=tcp:ec-server-aj.database.windows.net,1433;Initial Catalog=UPG2-Datalagringdb;Persist Security Info=False;User ID=sqlAdmin;Password=*****;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static async Task AddAsync(Customer customer, SupportCase supportCase)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //Kontrollera om kunden redan finns i databasen
                using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from customers where SSNumber = @SSN", conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@SSN", customer.SSN);
                    int userCount = (int)sqlCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        var query = @"
                        INSERT INTO SupportCases (CustomerNumber, Status, Description, Title, Category, Time)
                        VALUES(@CustomerNumber, @Status, @Description, @Title, @Category, @Time)
                        ";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@CustomerNumber", customer.SSN);
                        cmd.Parameters.AddWithValue("@Status", supportCase.Status);
                        cmd.Parameters.AddWithValue("@Description", supportCase.Description);
                        cmd.Parameters.AddWithValue("@Title", supportCase.Title);
                        cmd.Parameters.AddWithValue("@Category", supportCase.Category);
                        cmd.Parameters.AddWithValue("@Time", supportCase.Time);

                        await cmd.ExecuteReaderAsync();
                        conn.Close();
                    }
                    else
                    {
                        var query = @"
                        INSERT INTO Customers (SSNumber, Name, PhoneNumber, Email) 
                        VALUES(@SSNumber, @Name, @PhoneNumber, @Email)
                        INSERT INTO SupportCases (CustomerNumber, Status, Description, Title, Category, Time)
                        VALUES(@CustomerNumber, @Status, @Description, @Title, @Category, @Time)
                        ";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@CustomerNumber", customer.SSN);
                        cmd.Parameters.AddWithValue("@Status", supportCase.Status);
                        cmd.Parameters.AddWithValue("@Description", supportCase.Description);
                        cmd.Parameters.AddWithValue("@Title", supportCase.Title);
                        cmd.Parameters.AddWithValue("@Category", supportCase.Category);
                        cmd.Parameters.AddWithValue("@Time", supportCase.Time);

                        cmd.Parameters.AddWithValue("@SSNumber", customer.SSN);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);

                        await cmd.ExecuteReaderAsync();
                        conn.Close();
                    }
                 }  
            }

        }
        public static IEnumerable<SupportCase> GetAll(string status)
        {
           var customerList = new List<Customer>();
            var caseList = new List<SupportCase>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var query = "SELECT * FROM Customers, SupportCases WHERE SupportCases.Status = @Status";
                //var query = "SELECT * FROM Customers, SupportCases WHERE Customers.SSNumber = SupportCases.CustomerNumber";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);

                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    int SSN = result.GetInt32(0);
                    string Name = result.GetString(1);
                    int PhoneNumber = result.GetInt32(2);
                    string Email = result.GetString(3);

                    int CaseNumber = result.GetInt32(4);
                    int CustomerNumber = result.GetInt32(5);
                    string Status = result.GetString(6);
                    string Description = result.GetString(7);
                    string Title = result.GetString(8);
                    string Category = result.GetString(9);
                    DateTime Time = result.GetDateTime(10);

                    customerList.Add(new Customer(SSN, Name, PhoneNumber, Email));
                    caseList.Add(new SupportCase(CaseNumber, CustomerNumber, Status, Description, Title, Category, Time));

                   
                }

                conn.Close();
                return caseList;
            }  
        }
        public static async Task UpdateAsync(int caseNumber, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var query = "UPDATE SupportCases SET Status = @Status WHERE CaseNumber = @CaseNumber;";
               
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CaseNumber", caseNumber);
                cmd.Parameters.AddWithValue("@Status", status);
              
                await cmd.ExecuteReaderAsync();
                conn.Close();
            }
        }

    }
}
