using static APDS_POE.Models.System.Enums;
using static APDS_POE.Models.System.EnumExtensions;

namespace APDS_POE.Models.System
{
    public class Dtos
    {
        public class ProductVM
        {
            public User user { get; set; }
            public List<Product> Products { get; set; }
        }

        public class ProductFilterDto
        {
            public Category? Category { get; set; }
            public DateTime? Date { get; set; } = DateTime.MinValue;
            public DateFilter? DateFilter { get; set; }
        }
    }
}
