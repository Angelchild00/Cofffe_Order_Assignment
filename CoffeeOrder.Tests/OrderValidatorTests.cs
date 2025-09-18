using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Validation;

namespace CoffeeOrder.Tests;

[TestClass]
public class OrderValidatorTests
{
    [TestMethod]
    public void Validate_MissingBase_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: null,
            size: "Tall",
            temp: "Hot",
            milk: "2%",
            plantMilk: null,
            shots: 1,
            syrups: new[] { "Vanilla" },
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        var result = OrderValidator.Validate(bev);

        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "base drink");
    }

    [TestMethod]
    public void Validate_DairyAndPlantMilkTogether_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Grande",
            temp: "Hot",
            milk: "2%",
            plantMilk: "Oat",
            shots: 1,
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        var result = OrderValidator.Validate(bev);

        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Milk selection invalid");
    }

    [TestMethod]
    public void Validate_TypicalLatte_ReturnsValid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Hot",
            milk: null,
            plantMilk: "Oat",
            shots: 2,
            syrups: new[] { "Vanilla" },
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        var result = OrderValidator.Validate(bev);

        Assert.IsTrue(result.IsValid);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void Validate_InvalidTempValue_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Tall",
            temp: "Warm", // Invalid temperature
            milk: null,
            plantMilk: "Oat",
            shots: 1,
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        //act
        var result = OrderValidator.Validate(bev);

        //assert
        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Temperature");
    }
    [TestMethod]
    public void Validate_InvalidSizeValue_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Massive", // Invalid size
            temp: "Hot",
            milk: null,
            plantMilk: "Soy",
            shots: 1,
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        //act
        var result = OrderValidator.Validate(bev);

        //assert
        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Size");
    }
    [TestMethod]
    public void Validate_ShotsOutOfRange_ReturnsInvalid()
    {
        var tooLow = new Beverage(
             baseDrink: "Americano",
             size: "Tall",
             temp: "Hot",
             milk: null,
             plantMilk: null,
             shots: -1, // Invalid shots (too low)
             syrups: Array.Empty<string>(),
             toppings: Array.Empty<string>(),
             isDecaf: false
         );

        var tooHigh = new Beverage(
            baseDrink: "Americano",
            size: "Tall",
            temp: "Hot",
            milk: null,
            plantMilk: null,
            shots: 5, // Invalid shots (too high)
            syrups: Array.Empty<string>(),
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        //act
        var resultLow = OrderValidator.Validate(tooLow);
        var resultHigh = OrderValidator.Validate(tooHigh);

        //assert
        Assert.IsFalse(resultLow.IsValid);
        StringAssert.Contains(string.Join("|", resultLow.Errors), "Shots");

        Assert.IsFalse(resultHigh.IsValid);
        StringAssert.Contains(string.Join("|", resultHigh.Errors), "Shots");
    }
    [TestMethod]
    public void Validate_NullBeverage_ReturnsInvalid()
    {
        Beverage bev = null;

        //act
        var result = OrderValidator.Validate(bev);

        //assert
        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Beverage must not be null");
    }
    [TestMethod]
    public void Validate_TooManySyrups_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Grande",
            temp: "Iced",
            milk: null,
            plantMilk: "Soy",
            shots: 1,
            syrups: new[] { "Vanilla", "Caramel", "Hazelnut", "Mocha", "Pumpkin Spice", "Peppermint" }, // 6 syrups, exceeding limit
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        //act
        var result = OrderValidator.Validate(bev);

        //assert
        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Syrups");
    }
    [TestMethod]
    public void Validate_NullSyrupEntry_ReturnsInvalid()
    {
        var bev = new Beverage(
            baseDrink: "Latte",
            size: "Grande",
            temp: "Iced",
            milk: null,
            plantMilk: "Soy",
            shots: 1,
            syrups: new[] { "Vanilla", null, "Hazelnut" }, // One null entry
            toppings: Array.Empty<string>(),
            isDecaf: false
        );

        //act
        var result = OrderValidator.Validate(bev);

        //assert
        Assert.IsFalse(result.IsValid);
        StringAssert.Contains(string.Join("|", result.Errors), "Syrups");
    }
}