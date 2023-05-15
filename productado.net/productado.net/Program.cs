using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;

namespace productado.net
{
    class ProductManagement
    {
        
        public void addnewproduct(SqlConnection con)
        {
            try
            {
                SqlCommand cmd = new SqlCommand($"insert into products values(@name,@description,@quantity,@price)", con);
                Console.WriteLine("Enter the product name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the product description");
                string description = Console.ReadLine();
                Console.WriteLine("Enter the product quantity");
                int quantity = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Enter the product price");
                int price = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Added successfully");
            }
            catch 
            {
                Console.WriteLine("No record added");
            }

        }

        public void getproduct(SqlConnection con)
        {
            Console.WriteLine("Enter the id to get");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd = new SqlCommand($"select * from products where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]} | ");
                }
                Console.WriteLine();
            }
            reader.Close();
              
        }
        public void getallproduct(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand($"select * from products", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]} | ");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        public void updateproduct(SqlConnection con)
        {
           
            Console.WriteLine("Entet the id to update");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd = new SqlCommand("update products set pname=@name,pdescription=@description,quantity=@quantity,price=@price where id=@id", con);
            Console.WriteLine("Enter the  new product name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new product description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter the  new product quantity");
            int quantity = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the new product price");
            int price = Convert.ToInt32(Console.ReadLine());
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            int rowaffected=cmd.ExecuteNonQuery();
            if(rowaffected > 0)
            {
                Console.WriteLine("Record updated successfully");

            }
            else
            {
                Console.WriteLine(" No Record updated ");
            }
        }
        public void deleteproduct(SqlConnection con)
        {   
            Console.WriteLine("Entet the id to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd = new SqlCommand("delete from products where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int rowaffected=cmd.ExecuteNonQuery();
            if(rowaffected > 0)
            {
                Console.WriteLine("Record is deleted successfully");
            }
            else
            {
                Console.WriteLine(" No Record is deleted ");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-6H3K9S3; database=productmanagement; Integrated Security=true");
            con.Open();
            ProductManagement obj= new ProductManagement();
            string ans = "";
            do
            {
                Console.WriteLine("--- Welcome to Product Management App");
                Console.WriteLine("1.Add new product");
                Console.WriteLine("2.Get product");
                Console.WriteLine("3.Get all product");
                Console.WriteLine("4.update product");
                Console.WriteLine("5. delete product");
                Console.WriteLine("Enter your choice");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            obj.addnewproduct(con);
                            break;
                        }
                    case 2:
                        {
                            obj.getproduct(con);
                            break;
                        }
                    case 3:
                        {
                            obj.getallproduct(con);
                            break;
                        }
                    case 4:
                        {
                            obj.updateproduct(con);
                            break;
                        }
                    case 5:
                        {
                            obj.deleteproduct(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invlaid option please enter correct option");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue[y/n]");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y");
            con.Close();
            
        }
    }
}