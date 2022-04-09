using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    internal class DataAccessLayer
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=ROCHA\SQLEXPRESS");
        
        public DataAccessLayer()
        {

        }
        public void InsertContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = @"
                    INSERT INTO Contacts (FirstName, LastName, Phone, Address) 
                    values (@FirstName, @LastName, @Phone, @Address)";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@FirstName", contact.FirstName),
                    new SqlParameter("@LastName", contact.LastName),
                    new SqlParameter("@Phone", contact.Phone),
                    new SqlParameter("@Address", contact.Address),
                };
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void UpdateContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = @"
                    Update Contacts 
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Phone =  @Phone,
                            Address = Address
                        WHERE Id = @Id";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Id", contact.Id),
                    new SqlParameter("@FirstName", contact.FirstName),
                    new SqlParameter("@LastName", contact.LastName),
                    new SqlParameter("@Phone", contact.Phone),
                    new SqlParameter("@Address", contact.Address),
                };
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<Contact> GetContacts(string search = null)
        {
            var lista = new List<Contact>();
            try
            {
                sqlConnection.Open();
                string query = @"SELECT * FROM Contacts";

                SqlCommand cmd = new SqlCommand();
                if (!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName Like @Search OR LastName LIKE @Search OR Phone LIKE @Search OR Address LIKE @Search";
                    cmd.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }
                cmd.CommandText = query;
                cmd.Connection = sqlConnection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { sqlConnection.Close(); }
            return lista;
        }
        public void Delete(int id)
        {
            try
            {
                sqlConnection.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query,sqlConnection);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
