using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
namespace Run_Over_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Olaide Ogunbunmi\OneDrive - Fast Credit\Desktop\Olaide File Folder\path.txt";
            string result =Path.GetDirectoryName(filePath);
            Console.WriteLine(result);
            //InsertDepartment();
            //GetAllDepartment();
            //GetDepartmentByID();
            //DeleteDepartment();
            //CheckSerial checkSerial = new CheckSerial();
            //checkSerial.DeserializeFileToObject();
            //Serialization serialization = new Serialization();
            //serialization.DeserializeObject();
        }
        
        private static string connectionString = @"Server = OLAIDE-OGUNBUNM\SQLEXPRESS; Database = Company101; Integrated Security = True; TrustServerCertificate = True;";
        private static void InsertDepartment()
        {
            Console.Write("Enter your department name: ");
            string userDepartment = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Department(DepartmentName) VALUES(@DepartmentName)";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("@DepartmentName", System.Data.SqlDbType.VarChar, 255).Value = userDepartment;
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                if (result > 0)
                {
                    Console.WriteLine($"{result} Record Affected.......");
                }
            }
        }
        private static void GetAllDepartment()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string selectQuery = "SELECT DepartmentID, DepartmentName, CreationDate FROM Department";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"DepartmentID: {reader["DepartmentID"].ToString()}");
                        Console.WriteLine($"DepartmentName: {reader["DepartmentName"].ToString()}");
                        Console.WriteLine($"CreationDate: {reader["CreationDate"].ToString()}");
                        Console.WriteLine("-----------------------------------");
                    }
                }
                reader.Close();
                connection.Close();
            }
        }
        private static void GetDepartmentByID()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.Write("Enter the Department ID: ");
                string deptID = Console.ReadLine();
                string IdQuery = "SELECT DepartmentID, DepartmentName, CreationDate FROM Department WHERE DepartmentID = @DepartmentID";
                using (SqlCommand command = new SqlCommand(IdQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("@DepartmentID", System.Data.SqlDbType.Int).Value = Convert.ToInt32(deptID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Console.WriteLine($"DepartmentID: {reader["DepartmentID"]}");
                            Console.WriteLine($"DepartmentName: {reader["DepartmentName"]}");
                            Console.WriteLine($"CreationDate: {reader["CreationDate"]}");

                        }


                    }
                }


            }
        }
        private static void DeleteDepartment()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.Write("ID of Department to delete: ");
                string deleteID = Console.ReadLine();
                string query = "DELETE FROM Department WHERE DepartmentID = @DepartmentID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("@DepartmentID", System.Data.SqlDbType.Int).Value = Convert.ToInt32(deleteID);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine($"{result} rows affected");
                    }                   
                }
            }
        }

    }
}
