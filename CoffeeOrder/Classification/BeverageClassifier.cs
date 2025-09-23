using CoffeeOrder.Models;

namespace CoffeeOrder.Classification;

public static class BeverageClassifier
{
    public static ClassificationResult Classify(Beverage beverage)
    {
       bool caffeinated = beverage.Shots > 0 && !beverage.IsDecaf;
       bool kidSafe = !caffeinated;

        return new ClassificationResult
        {
            KidSafe = kidSafe,
            DairyFree = false,
            VeganFriendly = false,
            Caffeinated = caffeinated
        };
    }

}