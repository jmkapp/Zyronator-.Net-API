using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web.Configuration;
using Zyronator.Models;

namespace Zyronator.Services
{
    public class UserListsService
    {
        public List<DatabaseUserList> GetUserLists()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            List<DatabaseUserList> userLists = new List<DatabaseUserList>();

            string query = "select ListId, DiscogsId, ListName, ResourceUrl, Uri, ListDescription from dbo.DiscogsUserList";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DatabaseUserList userList = new DatabaseUserList();
                            userList.Id = reader.GetInt32(reader.GetOrdinal("ListId"));
                            userList.DiscogsId = reader.GetInt32(reader.GetOrdinal("DiscogsId"));
                            userList.Name = reader.GetString(reader.GetOrdinal("ListName"));
                            userList.ResourceUrl = reader.GetString(reader.GetOrdinal("ResourceUrl"));
                            userList.Uri = reader.GetString(reader.GetOrdinal("Uri"));
                            userList.Description = reader.GetString(reader.GetOrdinal("ListDescription"));

                            userLists.Add(userList);
                        }
                    }
                }
            }

            return userLists;
        }

        public void Insert(IEnumerable<List> userLists)
        {
            if (!userLists.Any()) return;

            string connectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            string query = "insert into dbo.DiscogsUserList(DiscogsId, ListName, ResourceUrl, Uri, ListDescription) " +
                "values(@DiscogsId, @ListName, @ResourceUrl, @Uri, @ListDescription)";

            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        foreach(List list in userLists)
                        {
                            command.Parameters.AddWithValue("@DiscogsId", list.Id);
                            command.Parameters.AddWithValue("@ListName", list.Name);
                            command.Parameters.AddWithValue("@ResourceUrl", list.Resource_Url);
                            command.Parameters.AddWithValue("@Uri", list.Uri);
                            command.Parameters.AddWithValue("@ListDescription", list.Description);

                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                    }
                }

                scope.Complete();
            }
        }
    }
}