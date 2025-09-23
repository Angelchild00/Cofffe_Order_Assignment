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
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
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
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsTrue(result.KidSafe);

    }
    [TestMethod]
    public void Classify_ShotsGreaterThanZeroAndNotDecaf_ReturnsNotKidSafe()
    {
        var bev = new Beverage(
            baseDrink: "Cappuccino",
            size: "Venti",
            temp: "Hot",
            milk: "2%",
            plantMilk: null,
            shots: 2,
            syrups: new[] { "Caramel" },
            toppings: new[] { "Cinnamon" },
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsFalse(result.KidSafe, "Beverage with shots greater than 0 and not decaf should not be classified as kid-safe.");
        Assert.IsTrue(result.Caffeinated, "Beverage with shots greater than 0 and not decaf should be classified as caffeinated.");

    }
    
}