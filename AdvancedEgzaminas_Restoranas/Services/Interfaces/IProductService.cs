using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts(string filename);
        // Product GetProduct(string name)
        // Product GetFood(string name) ?
        // Product GetDrink(string name) ?
    }
}
