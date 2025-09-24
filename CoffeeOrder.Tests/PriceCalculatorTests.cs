using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;

namespace CoffeeOrder.Tests;

[TestClass]
public class PriceCalculatorTests
{
    [TestMethod]
    public void CalculatePrice_Tall_NoAddons_ReturnsBasePrice()
    {
        var beverage = new Beverage(
            baseDrink: "Latte",     // Base price: 3.00
            size: "Tall",       // Size adjustment: 0.00
            temp: "Hot",    
            milk: "Whole",  
            plantMilk: null,
            shots: 0,
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(3.00m, price);
    }
    [TestMethod]
    public void CalculatePrice_AddShots()
    {
        var beverage = new Beverage(
            baseDrink: "Espresso",  // Base price: 2.50
            size: "Tall",   
            temp: "Hot",
            milk: null,
            plantMilk: null,
            shots: 2,   // 2 shots at 0.50 each
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(3.50m, price); // 2.50 + (2 * 0.50)
    }
    [TestMethod]
    public void CalculatePrice_AddSyrups()
    {
        var beverage = new Beverage(
            baseDrink: "Cappuccino",    // Base price: 3.50
            size: "Grande", // Size adjustment: 0.50
            temp: "Hot",
            milk: "2%",
            plantMilk: null,
            shots: 1,   // 1 shot at 0.50
            syrups: new[] { "Vanilla", "Caramel" }, // 2 syrups at 0.25 each
            toppings: Array.Empty<string>(),
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(5.00m, price); // 3.50 + 0.50 + 0.50 + (2 * 0.25)
    }
    [TestMethod]
    public void CalculatePrice_AddToppings()
    {
        var beverage = new Beverage(
            baseDrink: "Mocha", // Base price: 3.75
            size: "Venti",  // Size adjustment: 1.00
            temp: "Hot",
            milk: "Whole",
            plantMilk: null,
            shots: 1,   // 1 shot at 0.50
            syrups: Array.Empty<string>(),
            toppings: new[] { "Whipped Cream", "Chocolate Shavings" },  // 2 toppings at 0.30 each
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(5.85m, price); // 3.75 + 1.00 + 0.50 + (2 * 0.30)
    }
    [TestMethod]
    public void CalculatePrice_PlantMilkSurcharge()
    {
        var beverage = new Beverage(
            baseDrink: "Latte", // Base price: 3.00
            size: "Grande", // Size adjustment: 0.50
            temp: "Hot",
            milk: null,
            plantMilk: "Oat",   // Plant milk surcharge: 0.50
            shots: 1,   // 1 shot at 0.50
            syrups: new[] { "Vanilla" },    // 1 syrup at 0.25
            toppings: new[] { "Cinnamon" }, //  1 topping at 0.30
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(5.05m, price); // 3.00 + 0.50 + 0.50 + 0.25 + 0.30 + 0.50
    }
    [TestMethod]
    public void CalculatePrice_SumItems()
    {
        var beverage = new Beverage(
            baseDrink: "Americano", // Base price: 2.00
            size: "Venti",  // Size adjustment: 1.00
            temp: "Iced",
            milk: null,
            plantMilk: "Almond",    // Plant milk surcharge: 0.50
            shots: 3,   // 3 shots at 0.50 each
            syrups: new[] { "Hazelnut", "Caramel", "Vanilla" }, // 3 syrups at 0.25 each
            toppings: new[] { "Whipped Cream" },    // 1 topping at 0.30
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(6.05m, PriceCalculator.CalculatePrice(beverage)); // 2.00 + 1.00 + (3 * 0.50) + (3 * 0.25) + (1 * 0.30) + 0.50
    }
}