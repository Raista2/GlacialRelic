using System;
using System.Collections.Generic;
using System.Threading;

public class ShopRegion : Region
{
    public override void Enter(Player player)
    {
        Console.WriteLine("\nYou've entered the Shop!");
        Console.WriteLine("1. Buy Health Potion (20 Gold) - Heals 30 HP");
        Console.WriteLine("2. Buy Mana Potion (15 Gold) - Restores 20 MP");
        Console.WriteLine("3. Leave Shop");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                if (player.Gold >= 20)
                {
                    player.Gold -= 20;
                    player.HP += 30;
                    Console.WriteLine("You bought a Health Potion!");
                }
                else
                {
                    Console.WriteLine("Not enough gold!");
                }
                break;
            case "2":
                if (player.Gold >= 15)
                {
                    player.Gold -= 15;
                    player.MP += 20;
                    Console.WriteLine("You bought a Mana Potion!");
                }
                else
                {
                    Console.WriteLine("Not enough gold!");
                }
                break;
        }
    }
}