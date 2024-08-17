using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        Product AddProduct();
        void SeedDrinks();
        void SeedFood();
    }
}
