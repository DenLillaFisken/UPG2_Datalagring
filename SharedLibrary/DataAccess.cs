using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedLibrary
{
    
    public static class DataAccess
    {
        private static readonly string ConnectionString = @"Server=tcp:ec-server-aj.database.windows.net,1433;Initial Catalog=UPG2-Datalagringdb;Persist Security Info=False;User ID=sqlAdmin;Password=MyLife2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static async Task<Configuration> ReadJson()
        {
            var jsonFilePath = @"C:\Users\alexa\OneDrive\Dokument\testarjson.json";
            StorageFile file = await StorageFile.GetFileFromPathAsync(jsonFilePath);

            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            var obj = JsonConvert.DeserializeObject<dynamic>(text);

            Configuration config = new Configuration(Convert.ToInt32(obj.NumberOfItems), Convert.ToString(obj.Status1), Convert.ToString(obj.Status2), Convert.ToString(obj.Status3));

            return config;
        }

        public static async Task AddAsync(Customer customer, SupportCase supportCase)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            { 

                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from customers where SSNumber = @SSN", conn);
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
                    }

                    conn.Close();
                }
                catch {}
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<CustomerCaseList> GetAll(string status1, string status2)
        {
            Customer customer = new Customer();
            SupportCase supportCase = new SupportCase();
            var joinedList = new List<CustomerCaseList>();

            var task = Task.Run(async () => await ReadJson());
            var number = task.Result.NumberOfItems;




            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var query = "SELECT TOP (@Limit) * FROM SupportCases INNER JOIN Customers ON Customers.SSNumber = SupportCases.CustomerNumber WHERE SupportCases.Status IN (@Status1, @Status2) ORDER BY SupportCases.CaseNumber DESC;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Limit", number);
                cmd.Parameters.AddWithValue("@Status1", status1);
                cmd.Parameters.AddWithValue("@Status2", status2);

                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    int CaseNumber = result.GetInt32(0);
                    int CustomerNumber = result.GetInt32(1);
                    string Status = result.GetString(2);
                    string Description = result.GetString(3);
                    string Title = result.GetString(4);
                    string Category = result.GetString(5);
                    DateTime Time = result.GetDateTime(6);

                    int SSN = result.GetInt32(7);
                    string Name = result.GetString(8);
                    int PhoneNumber = result.GetInt32(9);
                    string Email = result.GetString(10);

                    customer = new Customer(SSN, Name, PhoneNumber, Email);
                    supportCase = new SupportCase(CaseNumber, CustomerNumber, Status, Description, Title, Category, Time);
                    joinedList.Add(new CustomerCaseList(customer, supportCase));
                }

                return joinedList;
            }
        }
        public static async Task UpdateAsync(int caseNumber, string status)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
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