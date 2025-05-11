using APDS_POE.Models;
using APDS_POE.Models.System;
using APDS_POE.Services;

namespace APDS_POE.Repositories
{

    public interface IProductRepository
    {
        public AppResponse AddProduct(Product Product);
    }
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext DB;

        public ProductRepository(AppDbContext dbContext)
        {
            DB = dbContext;
        }

        public AppResponse AddProduct(Product Product)
        {

            AppResponse response = new AppResponse();

            try
            {
                //TODO: Get Signed in user
                Product.UserId = 1;
                Product.DateCreated = DateTime.Now;

                DB.Products.Add(Product);
                DB.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Product added successfully";
                return response;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while trying to add a product: {ex.Message}";
                return response;
            }
        }

        //TODO: RemoveProduct()

    }
}
