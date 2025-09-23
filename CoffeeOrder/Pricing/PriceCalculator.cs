namespace CoffeeOrder.Pricing;

using CoffeeOrder.Models;
using System.Collections.Generic;
using System.Linq;
using System;

public static class PriceCalculator
{
    private static readonly Dictionary<string, decimal> BasePrices = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Latte", 3.00m },
        { "Cappuccino", 3.50m },
        { "Espresso", 2.50m },
        { "Americano", 2.00m },
        { "Mocha", 3.75m },
        { "Hot Chocolate", 2.50m }
    };

    private static readonly Dictionary<string, decimal> SizeModifiers = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Tall", 0.00m },
        { "Grande", 0.50m },
        { "Venti", 1.00m }
    };

    private const decimal ShotPrice = 0.50m;
    private const decimal SyrupPrice = 0.25m;
    private const decimal ToppingPrice = 0.30m;
    private const decimal PlantMilkSurcharge = 0.50m;

    public static decimal CalculatePrice(Beverage beverage)
    {
        if (beverage == null)
            throw new ArgumentNullException(nameof(beverage));
            
        if (string.IsNullOrWhiteSpace(beverage.BaseDrink) || string.IsNullOrWhiteSpace(beverage.Size))
            throw new ArgumentException("Base drink and size must be specified.");

        if (!BasePrices.TryGetValue(beverage.BaseDrink, out var basePrice))
            throw new ArgumentException($"Unknown base drink: {beverage.BaseDrink}");

        if (!SizeModifiers.TryGetValue(beverage.Size, out var sizeModifier))
            throw new ArgumentException($"Unknown size: {beverage.Size}");

        var price = basePrice + sizeModifier;

        // Add shot costs
        price += beverage.Shots * ShotPrice;

        // Add syrup costs
        if (beverage.Syrups != null)
            price += beverage.Syrups.Length * SyrupPrice;

        // Add topping costs
        if (beverage.Toppings != null)
            price += beverage.Toppings.Length * ToppingPrice;

        // Add plant milk surcharge if applicable
        if (!string.IsNullOrWhiteSpace(beverage.PlantMilk))
            price += PlantMilkSurcharge;

        return price;
    }
    public static decimal CalculateOrderPrice(IEnumerable<Beverage> items) => items.Sum(CalculatePrice);
}