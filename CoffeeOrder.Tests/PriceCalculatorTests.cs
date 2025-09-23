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
            baseDrink: "Latte",
            size: "Tall",
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
            baseDrink: "Espresso",
            size: "Tall",
            temp: "Hot",
            milk: null,
            plantMilk: null,
            shots: 2,
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
            baseDrink: "Cappuccino",
            size: "Grande",
            temp: "Hot",
            milk: "2%",
            plantMilk: null,
            shots: 1,
            syrups: new[] { "Vanilla", "Caramel" },
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
            baseDrink: "Mocha",
            size: "Venti",
            temp: "Hot",
            milk: "Whole",
            plantMilk: null,
            shots: 1,
            syrups: Array.Empty<string>(),
            toppings: new[] { "Whipped Cream", "Chocolate Shavings" },
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
            baseDrink: "Latte",
            size: "Grande",
            temp: "Hot",
            milk: null,
            plantMilk: "Oat",
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: new[] { "Cinnamon" },
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
            baseDrink: "Americano",
            size: "Venti",
            temp: "Iced",
            milk: null,
            plantMilk: "Almond",
            shots: 3,
            syrups: new[] { "Hazelnut", "Caramel", "Vanilla" },
            toppings: new[] { "Whipped Cream" },
            isDecaf: false
        );
        //act
        var price = PriceCalculator.CalculatePrice(beverage);
        //assert
        Assert.AreEqual(6.05m, PriceCalculator.CalculatePrice(beverage)); // 2.00 + 1.00 + (3 * 0.50) + (3 * 0.25) + (1 * 0.30) + 0.50
    }
}