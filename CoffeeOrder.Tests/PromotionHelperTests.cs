using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Validation;
using System.Linq;
using System;
using CoffeeOrder.Promotions;

namespace CoffeeOrder.Tests;

[TestClass]
public class PromotionHelperTests
{
    [TestMethod]
    public void Apply_HappyHour_SingleHotDrink_Takes20PercentOff()
    {
        // Arrange
        var bev = new Beverage(
         baseDrink: "Latte",
         size: "Tall",
         temp: "Hot",
         milk: "Whole",
         plantMilk: null,
         shots: 1,
         syrups: Array.Empty<string>(),
         toppings: Array.Empty<string>(),
         isDecaf: true
         );
        var items = new[] { bev };
        var subtotal = PricingHelper.CalculateSubtotal(items);
        var (totalAfter, discounts) = PromotionHelper.Apply(items new[] { "HappyHour" });
        var expectedDiscount = Math.Round(subtotal - expectedDiscount, 2);

        // Assert
        Assert.AreEqual(expectedTotsl, totalAfter);
        Assert.AreEqual("HAPPYHOUR", discounts[0].Code);
        Assert.AreEqual(expectedDiscount, discounts[0].Amount);
    }

    }