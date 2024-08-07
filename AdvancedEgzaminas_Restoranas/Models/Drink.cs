﻿namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Drink : Product
    {
        public override string Type => "Drink";

        public Drink(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Drink() { }


    }
}
