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
}