using APDS_POE.Models;
using APDS_POE.Models.System;
using APDS_POE.Services;
using static APDS_POE.Models.System.Dtos;

namespace APDS_POE.Repositories
{

    public interface IProductRepository
    {
        public AppResponse AddProduct(Product Product);
        public List<Product> GetProducts(int ID);
        public List<Product> GetAllProducts();
        public List<Product> GetFilteredProducts(ProductFilterDto Dto);
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

        public List<Product> GetAllProducts()
        {
            return DB.Products.ToList();
        }

        public List<Product> GetFilteredProducts(ProductFilterDto Dto)
        {

            var productsQuery = DB.Products.AsQueryable();

            // Filter by category
            if (Dto.Category != null)
            {
                var categoryDesc = Dto.Category.GetEnumDescription();
                productsQuery = productsQuery.Where(x => x.Category == categoryDesc);
            }

            // Filter by exact date when DateFilter is null
            if (Dto.DateFilter == Enums.DateFilter.All && (Dto.Date != DateTime.MinValue && Dto.Date != null))
            {
                var targetDate = Dto.Date.Value.Date;
                productsQuery = productsQuery.Where(x => x.ProductionDate.HasValue && x.ProductionDate.Value.Date == targetDate);
            }

            // Filter based on DateFilter logic
            if (Dto.DateFilter != Enums.DateFilter.All && (Dto.Date != DateTime.MinValue && Dto.Date != null))
            {
                var targetDate = Dto.Date.Value.Date;

                productsQuery = Dto.DateFilter switch
                {
                    Enums.DateFilter.MoreThan => productsQuery.Where(x => x.ProductionDate.HasValue && x.ProductionDate.Value.Date > targetDate),
                    Enums.DateFilter.LessThan => productsQuery.Where(x => x.ProductionDate.HasValue && x.ProductionDate.Value.Date < targetDate),
                    Enums.DateFilter.EqualsTo => productsQuery.Where(x => x.ProductionDate.HasValue && x.ProductionDate.Value.Date == targetDate),
                    _ => productsQuery // Enums.DateFilter.All or default: no filter applied
                };
            }

            var Products = productsQuery.ToList();

            return Products;

        }

        //TODO: RemoveProduct()

    }
}
