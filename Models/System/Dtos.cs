namespace APDS_POE.Models.System
{
    public class Dtos
    {
        public class ProductVM
        {
            public User user { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}
