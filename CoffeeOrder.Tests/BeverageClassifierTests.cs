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
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsFalse(result.KidSafe, "Beverage with shots greater than 0 and not decaf should not be classified as kid-safe.");
        Assert.IsTrue(result.Caffeinated, "Beverage with shots greater than 0 and not decaf should be classified as caffeinated.");

    }
    [TestMethod]
    public void Classify_NoDairyMilk_ReturnsDairyFree()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Hot",
            milk: null, //no dairy milk
            plantMilk: "Oat",
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: Array.Empty<string>(), //no toppings
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsTrue(result.DairyFree, "No dairy milk or toppings should be classified as dairy-free.");
    }
    [TestMethod]
    public void Classify_WhippedCreamTopping_ReturnsNotDairyFree()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Hot",
            milk: null, //no dairy milk
            plantMilk: "Oat",
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: new[] { "Whipped Cream" }, //has whipped cream topping
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsFalse(result.DairyFree, "Whipped cream topping should make the beverage not dairy-free.");
    }
    [TestMethod]
    public void Claassify_DairyMilk_ReturnsNotDairyFree()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Hot",
            milk: "Whole", //has dairy milk
            plantMilk: null,
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: Array.Empty<string>(), //no toppings
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsFalse(result.DairyFree, "Dairy milk should make the beverage not dairy-free.");
    }
    [TestMethod]
    public void Classify_DairyFree_NoAnimalProducts_ReturnsVeganFriendly()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Hot",
            milk: null, //no dairy milk
            plantMilk: "Oat",
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: Array.Empty<string>(), //no toppings
            isDecaf: false
        );
        //act
        var result = BeverageClassifier.Classify(bev);
        //assert
        Assert.IsTrue(result.DairyFree, "Beverage with no dairy milk or toppings should be classified as dairy-free.");
        Assert.IsTrue(result.VeganFriendly, "Beverage with no dairy milk or toppings should be classified as vegan-friendly.");
    }
}