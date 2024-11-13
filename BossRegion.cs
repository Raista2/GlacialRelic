using System;
using System.Collections.Generic;
using System.Threading;

public class BossRegion : Region
{
    public override void Enter(Player player)
    {
        Console.WriteLine("\nBOSS BATTLE!");
        Console.WriteLine("The Dungeon Master appears before you!");
        
        Random rnd = new Random();
        int bossHP = 150;
        
        while (bossHP > 0 && player.HP > 0)
        {
            Console.WriteLine($"\nBoss HP: {bossHP}");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Magic (costs 20 MP)");
            
            string choice = Console.ReadLine();
            int damage;
            
            switch (choice)
            {
                case "1":
                    damage = rnd.Next(15, 25);
                    bossHP -= damage;
                    Console.WriteLine($"You dealt {damage} damage to the boss!");
                    break;
                case "2":
                    if (player.MP >= 20)
                    {
                        damage = rnd.Next(30, 45);
                        bossHP -= damage;
                        player.MP -= 20;
                        Console.WriteLine($"You cast a spell and dealt {damage} damage!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough MP!");
                    }
                    break;
            }
            
            if (bossHP > 0)
            {
                int bossDamage = rnd.Next(15, 30);
                player.HP -= bossDamage;
                Console.WriteLine($"Boss attacks you for {bossDamage} damage!");
            }
        }
    }
}