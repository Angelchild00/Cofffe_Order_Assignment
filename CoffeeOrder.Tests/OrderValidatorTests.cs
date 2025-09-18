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
}