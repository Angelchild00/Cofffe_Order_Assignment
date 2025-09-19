using CoffeeOrder.Models;

namespace CoffeeOrder.Classification;

public static class BeverageClassifier
{
    public static ClassificationResult Classify(Beverage beverage)
    {
        var kidSafe = beverage.Shots == 0 || beverage.IsDecaf;
        return new ClassificationResult
        {
            KidSafe = kidSafe,
            DairyFree = false,
            VeganFriendly = false,
            Caffeinated = false
        };
    }

}