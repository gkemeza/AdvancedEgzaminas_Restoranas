﻿using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.Services;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas
{
    // - use Singleton pattern

    public class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dataAccess = new DataAccess();
            UserInterface userInterface = new UserInterface();
            IProductService productService = new ProductService(
                dataAccess, @"..\..\..\Data\drinks.csv", @"..\..\..\Data\food.csv");
            ITableService tableService = new TableService(
                dataAccess, @"..\..\..\Data\tables.csv");
            IOrderService orderService = new OrderService(
                dataAccess, tableService, productService, userInterface, @"..\..\..\Data\orders.csv");
            IRestaurantService restaurant = new RestaurantService(
                dataAccess, userInterface, tableService, productService, orderService);

            restaurant.Run();
        }
    }
}