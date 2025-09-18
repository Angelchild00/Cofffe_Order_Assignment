using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Validation;

namespace CoffeeOrder.Tests;
[TestClass]
public class BeverageClassifierTests
{
    [TestMethod]
    public void Classify_NoShotsOrDecaf_ReturnsKidSafe()
    {
        var bev = new Beverage(
            baseDrink: "Hot Chocolate",
            size: "Small",
            temp: "Hot",
            milk: "Whole",
            plantMilk: null,
            shots: 0,
            syrups: new[] { "Vanilla" },
            toppings: new[] { "Whipped Cream" },
            isDecaf: false
        );

        var category = BeverageClassifier.Classify(bev);

        Assert.AreEqual("Kid-Safe", category);

    }
}