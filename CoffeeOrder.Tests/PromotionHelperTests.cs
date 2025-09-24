using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Promotions;


namespace CoffeeOrder.Tests
{
    [TestClass]
    public class PromotionHelperTests
    {
        [TestMethod]
        public void Apply_HappyHour_DoesNotDiscountIced()
        {
            var hot = new Beverage(
                baseDrink: "Latte",
                size: "Tall",
                temp: "Hot",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );

            var iced = new Beverage(
                baseDrink: "Latte",
                size: "Tall",
                temp: "Iced",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );

            var items = new[] { hot, iced };
            var subtotal = PriceCalculator.CalculateOrderPrice(items);

            var (totalAfter, discounts) = PromotionHelper.Apply(items, new[] { "HAPPYHOUR" });

            var expectedDiscount = Math.Round(PriceCalculator.CalculatePrice(hot) * 0.20m, 2);
            var expectedTotal = Math.Round(subtotal - expectedDiscount, 2);

            Assert.AreEqual(1, discounts.Count);
            Assert.AreEqual("HAPPYHOUR", discounts[0].Code);
            Assert.AreEqual(expectedDiscount, discounts[0].Amount);
            Assert.AreEqual(expectedTotal, totalAfter);
        }
        [TestMethod]
        public void Apply_BOGO_TwoItems_DiscountCheapestOne()
        {
            var bev1 = new Beverage(
                    baseDrink: "Latte", // Base price: 3.00
                    size: "Tall",   // Size adjustment: 0.00
                    temp: "Hot",
                    milk: null,
                    plantMilk: null,
                    shots: 0,
                    syrups: Array.Empty<string>(),
                    toppings: Array.Empty<string>(),
                    isDecaf: true
                );

            var bev2 = new Beverage(
                baseDrink: "Espresso",  // Base price: 2.50
                size: "Venti", // Size adjustment: 1.00
                temp: "Hot",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );

            var items = new[] { bev1, bev2 };
            var subtotal = PriceCalculator.CalculateOrderPrice(items);

            var (totalAfter, discounts) = PromotionHelper.Apply(items, new[] { "BOGO" });

            var expectedDiscount = Math.Round(Math.Min(PriceCalculator.CalculatePrice(bev1), PriceCalculator.CalculatePrice(bev2)), 2);
            var expectedTotal = Math.Round(subtotal - expectedDiscount, 2);

            Assert.AreEqual(1, discounts.Count);
            Assert.AreEqual("BOGO", discounts[0].Code);
            Assert.AreEqual(3.00m, discounts[0].Amount);
            Assert.AreEqual(subtotal - 3.00m, totalAfter);

        }
    }
}
