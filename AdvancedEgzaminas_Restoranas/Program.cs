﻿using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.Services;
using AdvancedEgzaminas_Restoranas.UI;
using AdvancedEgzaminas_Restoranas.DataAccess;

namespace AdvancedEgzaminas_Restoranas
{
    public class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dataAccess = new DataAccess.DataAccess();
            UserInterface userInterface = new UserInterface();
            IEmailService emailService = new EmailService(userInterface);
            IReceiptService receiptService = new ReceiptService(
                dataAccess, userInterface, @"..\..\..\Data\receipts.json");
            IProductService productService = new ProductService(
                dataAccess, @"..\..\..\Data\drinks.csv", @"..\..\..\Data\food.csv");
            ITableService tableService = new TableService(
                dataAccess, userInterface, @"..\..\..\Data\tables.csv");
            IOrderService orderService = new OrderService(
                dataAccess, tableService, productService, userInterface, @"..\..\..\Data\orders.json");
            IRestaurantService restaurant = new RestaurantService(
                dataAccess, userInterface, tableService, productService,
                orderService, receiptService, emailService);

            restaurant.Run();
        }
    }
}