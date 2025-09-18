namespace CoffeeOrder.Models;

public class Beverage
{
    public string? BaseDrink { get; }
    public string? Size { get; }
    public string? Temp { get; } // "Hot" or "Iced"
    public string? Milk { get; } // dairy milk (e.g., "2%")
    public string? PlantMilk { get; } // plant-based milk (e.g., "Oat")
    public int Shots { get; } // 0..4
    public string[] Syrups { get; }
    public string[] Toppings { get; }
    public bool IsDecaf { get; }

    public Beverage(
        string? baseDrink,
        string? size,
        string? temp,
        string? milk,
        string? plantMilk,
        int shots,
        IEnumerable<string>? syrups,
        IEnumerable<string>? toppings,
        bool isDecaf
    )
    {
        BaseDrink = baseDrink;
        Size = size;
        Temp = temp;
        Milk = milk;
        PlantMilk = plantMilk;
        Shots = shots;
        Syrups = (syrups ?? Array.Empty<string>()).ToArray();
        Toppings = (toppings ?? Array.Empty<string>()).ToArray();
        IsDecaf = isDecaf;
    }
}