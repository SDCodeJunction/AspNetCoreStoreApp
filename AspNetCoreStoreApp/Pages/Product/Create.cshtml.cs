using AspNetCoreStoreApp.Pages.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AspNetCoreStoreApp.Pages.Product
{
    public class CreateModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        } 

        public void OnPost()
        {
            productInfo.ProductName = Request.Form["productname"];
            productInfo.Price = Request.Form["price"];
            productInfo.LastModified = Request.Form["lastmodified"];

            if (productInfo.ProductName.Length == 0 ||
                productInfo.Price.Length == 0 ||
                productInfo.LastModified.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //Save the new product into the database
            //productInfo.ProductName = "";
            //productInfo.Price = "";
            //productInfo.LastModified = "";

            //successMessage = "New product added";

            try
            {
                string ConnectionString = @"data source=DESKTOP-4H46A0N\MSSQLSERVER01; database=Books; integrated security=SSPI";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Opening Connection  
                    connection.Open();

                    string sqlquery = "INSERT INTO Products " + "(ProductName,Price, LastModified) VALUES" +
                        "(@ProductName,@Price, @LastModified);";
                    using (SqlCommand command = new SqlCommand(sqlquery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productInfo.ProductName.ToString());
                        command.Parameters.AddWithValue("@Price", Convert.ToDecimal(productInfo.Price));
                        command.Parameters.AddWithValue("@LastModified", Convert.ToDateTime(productInfo.LastModified));

                        command.ExecuteNonQuery();
                    }
                    

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("Index");
        }
    }
}
