using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication1.Controllers
{
    public class Productcontroller : Controller
    {
        string connectionString = "Server=localhost;Database=stockproduct;Trusted_Connection=True;";
        public List<ProductModel> Index(string sql)
        {
            //sql = "select * from dbo.Product";
            List<ProductModel> ProductList = new List<ProductModel>();
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                //string queryStatement = "SELECT TOP 5 * FROM dbo.Customers ORDER BY CustomerID";

                using (SqlCommand _cmd = new SqlCommand(sql, _con))
                {
                    DataTable customerTable = new DataTable("Product");

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(customerTable);
                    _con.Close();

                    ProductList = setModel(customerTable);
                }
            }
            return ProductList;
        }

        public List<ProductModel> setModel(DataTable dt)
        {
            List<ProductModel> ProductList = new List<ProductModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductModel product = new ProductModel();
                product.Id = Convert.ToInt32(dt.Rows[i]["Product_ID"]);
                product.Name = Convert.ToString(dt.Rows[i]["Product_Name"]);
                product.Price = Convert.ToInt32(dt.Rows[i]["Product_Price"]);
                product.Amount = Convert.ToInt32(dt.Rows[i]["Stock_Amount"]);
                ProductList.Add(product);
            }
            return ProductList;
        }
        public HttpStatusCode update(string sql)
        {
            //var sql = "UPDATE Employees SET LastName = @LastName, FirstName = @FirstName, Title = @Title ... ";// repeat for all variables
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        //command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = Lnamestring;
                        //command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = Fnamestring;
                        //command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Titelstring;
                        // repeat for all variables....
                        connection.Open();
                        command.ExecuteNonQuery();
                        //int status = 0;
                        connection.Close();
                        //string message = "Success";
                        return HttpStatusCode.OK;
                        //return Request.CreateResponse(HttpStatusCode.OK, new { status, message });
                    }
                }

            }
            catch (Exception e)
            {
                return HttpStatusCode.NotFound;
                //MessageBox.Show($"Failed to update. Error message: {e.Message}");
            }
            //return ProductList;
        }
    }
}
