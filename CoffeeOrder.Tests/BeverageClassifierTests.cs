using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Validation;
using CoffeeOrder.Classification;

namespace CoffeeOrder.Tests;

[TestClass]
public class BeverageClassifierTests
{
    [TestMethod]
    public void Classify_NoShots_ReturnsKidSafe()
    {
        var bev = new Beverage(
            baseDrink: "Hot Chocolate",
            size: "Tall",
            temp: "Hot",
            milk: "Whole",
            plantMilk: null,
            shots: 0,
            syrups: new[] { "Vanilla" },
            toppings: new[] { "Whipped Cream" },
            isDecaf: false
        );

        var result = BeverageClassifier.Classify(bev);

        Assert.IsTrue(result.KidSafe);

    }
    [TestMethod]
    public void Classify_Decaf_ReturnsKidSafe()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Grande",
            temp: "Hot",
            milk: "2%",
            plantMilk: null,
            shots: 1,
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: true
        );

        var result = BeverageClassifier.Classify(bev);

        Assert.IsTrue(result.KidSafe);

    }
}