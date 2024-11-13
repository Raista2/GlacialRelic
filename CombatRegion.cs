using System;
using System.Collections.Generic;
using System.Threading;

public class CombatRegion : Region
{
    public override void Enter(Player player)
    {
        Console.WriteLine("\nYou've encountered an enemy!");
        Random rnd = new Random();
        int damage = rnd.Next(10, 20);
        player.HP -= damage;
        int goldReward = rnd.Next(10, 30);
        player.Gold += goldReward;
        Console.WriteLine($"You took {damage} damage!");
        Console.WriteLine($"You received {goldReward} gold!");
    }
}