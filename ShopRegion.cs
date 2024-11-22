using System;
using System.Collections.Generic;

// Shop for Healing and Buffs
public class Shop
{
    public void OpenShop(Player player)
    {
        string choice = string.Empty;

        while (choice != "4") // Keep shopping until player chooses to exit
        {
            Console.WriteLine("\nWelcome to the Shop!");
            Console.WriteLine("1. Buy Health Potion (Cost: 10 Gold)");
            Console.WriteLine("2. Buy Mana Potion (Cost: 10 Gold)");
            Console.WriteLine("3. Buy Buff (Cost: 20 Gold)");
            Console.WriteLine("4. Exit Shop");

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BuyHealthPotion(player);
                    break;

                case "2":
                    BuyManaPotion(player);
                    break;

                case "3":
                    BuyBuff(player);
                    break;

                case "4":
                    Console.WriteLine("You exit the shop.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void BuyHealthPotion(Player player)
    {
        if (player.Gold >= 10)
        {
            player.Gold -= 10;
            player.Buffs.Add("Health Potion");
            Console.WriteLine("You bought a Health Potion!");
        }
        else
        {
            Console.WriteLine("You don't have enough gold.");
        }
    }

    private void BuyManaPotion(Player player)
    {
        if (player.Gold >= 10)
        {
            player.Gold -= 10;
            player.Buffs.Add("Mana Potion");
            Console.WriteLine("You bought a Mana Potion!");
        }
        else
        {
            Console.WriteLine("You don't have enough gold.");
        }
    }

    private void BuyBuff(Player player)
    {
        if (player.Gold >= 20)
        {
            player.Gold -= 20;
            player.Buffs.Add("Buff");
            Console.WriteLine("You bought a Buff!");
        }
        else
        {
            Console.WriteLine("You don't have enough gold.");
        }
    }
}