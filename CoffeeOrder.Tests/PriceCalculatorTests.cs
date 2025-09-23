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
}