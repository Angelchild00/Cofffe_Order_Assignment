namespace CoffeeOrder.Models;

public sealed class ClassificationResult
{
    public bool DairyFree { get; init; }
    public bool VeganFriendly { get; init; }
    public bool KidSafe { get; init; }
    public bool Caffeinated { get; init; }
    public bool ContainsDairy { get; init; }
}