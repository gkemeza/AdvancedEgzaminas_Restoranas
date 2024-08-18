using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        void SeedDrinks();
        void SeedFood();
    }
}
