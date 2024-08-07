using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts(string filename);
        void AddProduct();
        void SeedDrinks();
        void SeedMeals();
        // Product GetProduct(string name)
        // Product GetFood(string name) ?
        // Product GetDrink(string name) ?
    }
}
