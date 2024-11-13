using System;
using System.Collections.Generic;
using System.Threading;

// Factory for creating regions
public class RegionFactory
{
    public static Region CreateRegion(string type)
    {
        return type switch
        {
            "shop" => new ShopRegion(),
            "buff" => new BuffRegion(),
            "combat" => new CombatRegion(),
            "boss" => new BossRegion(),
            _ => throw new ArgumentException("Invalid region type"),
        };
    }
}