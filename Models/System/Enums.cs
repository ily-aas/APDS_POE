using System.ComponentModel;
using System.Reflection;

namespace APDS_POE.Models.System
{
    public class Enums
    {

        public enum UserRole
        {
            [Description("Farmer")]
            Farmer = 0,

            [Description("Employee")]
            Employee = 1,
        }

        public enum Category
        {

            [Description("All")]
            All = 0,

            [Description("Vegetables")]
            Vegatables = 1,

            [Description("Fruits")]
            Fruits = 2,

            [Description("Grains")]
            Grains = 3,

            [Description("Dairy Products")]
            DairyProducts = 4,

            [Description("Poultry")]
            Poultry = 5,

        }

        public enum DateFilter
        {
            All = 0,
            MoreThan = 1,
            LessThan = 2,
            EqualsTo = 3,
        }

    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}
