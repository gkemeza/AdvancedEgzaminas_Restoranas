using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts(string drinksFilePath, string foodFilePath);
        void AddProduct();
        void SeedDrinks();
        void SeedFood();
        // Product GetProduct(string name)
        // Product GetFood(string name) ?
        // Product GetDrink(string name) ?
    }
}
