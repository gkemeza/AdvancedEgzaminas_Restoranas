﻿using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int tableNumber, List<Product> products);
    }
}