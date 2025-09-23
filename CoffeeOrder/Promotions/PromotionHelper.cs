namespace CoffeeOrder.Promotions;

using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Validation;
using System.Linq;
using System;
using System.Collections.Generic;

public record Discount(string code, decimal Amount, string Description);

public static class PromotionHelper
{
    
}