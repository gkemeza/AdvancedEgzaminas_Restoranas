using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product AddProduct();
        void SeedDrinks();
        void SeedFood();
    }
}
