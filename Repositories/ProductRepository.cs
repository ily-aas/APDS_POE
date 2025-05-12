using APDS_POE.Models;
using APDS_POE.Models.System;
using APDS_POE.Services;

namespace APDS_POE.Repositories
{

    public interface IProductRepository
    {
        public AppResponse AddProduct(Product Product);
        public List<Product> GetProducts(int ID);
    }
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext DB;
        private readonly IHelperService _helper;

        public ProductRepository(AppDbContext dbContext, IHelperService helperService)
        {
            DB = dbContext;
            _helper = helperService;
        }

        public AppResponse AddProduct(Product Product)
        {

            AppResponse response = new AppResponse();

            try
            {
                var User = _helper.GetSignedInUser();
                Product.UserId = User == null ? 0 : User.Id;
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

        public List<Product> GetProducts(int ID)
        {
            return DB.Products.Where(x => x.UserId == ID).ToList();
        }

        //TODO: RemoveProduct()

    }
}
