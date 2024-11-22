using System;
using System.Collections.Generic;

// Boss Character
public class Boss : Region
{
    private int bossHP;
    private int bossAttack;
    private string bossName;

    public Boss(string name, int hp, int attack)
    {
        bossName = name;
        bossHP = hp;
        bossAttack = attack;
    }

    public override void Enter(Player player)
    {
        Console.WriteLine($"You've reached the final level! {bossName} awaits!");

        while (bossHP > 0 && player.HP > 0)
        {
            Console.WriteLine($"\nBoss HP: {bossHP}");
            player.DisplayStats();

            // Player turn
            Console.WriteLine("\nYour Move:");
            Console.WriteLine("1. Normal Attack");
            Console.WriteLine("2. Elemental Attack (Cost: 10 MP)");
            Console.WriteLine("3. Use Potion");

            string choice = Console.ReadLine();
            int damage;

            switch (choice)
            {
                case "1":
                    damage = NormalAttack(player);
                    bossHP -= damage;
                    Console.WriteLine($"You dealt {damage} damage to {bossName}!");
                    break;

                case "2":
                    if (player.MP >= 10)
                    {
                        damage = ElementalAttack(player);
                        player.MP -= 10;
                        bossHP -= damage;
                        Console.WriteLine($"You dealt {damage} damage to {bossName} with an Elemental Attack!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough MP.");
                    }
                    break;

                case "3":
                    UsePotion(player);
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    continue;
            }

            // Boss action
            if (bossHP > 0)
            {
                ExecuteEnemyAction(player);
            }
        }

        if (player.HP > 0)
        {
            Console.WriteLine($"\nYou defeated {bossName}!");
        }
        else
        {
            Console.WriteLine("\nYou were defeated by the boss!");
        }
    }

    private void ExecuteEnemyAction(Player player)
    {
        Random rand = new Random();
        int damage = rand.Next(30, 50);
        player.HP -= damage;
        Console.WriteLine($"{bossName} attacks you for {damage} damage!");
    }

    private int NormalAttack(Player player)
    {
        int damage = 20;
        if (player.Debuffs.Contains("Weakened"))
        {
            damage /= 2;
            Console.WriteLine("Your weakened state reduced the damage.");
        }
        return damage;
    }

    private int ElementalAttack(Player player)
    {
        int damage = 0;
        Random rand = new Random();
        int attackChoice = rand.Next(1, 4); // Random elemental attack

        switch (attackChoice)
        {
            case 1:
                damage = 25;
                Console.WriteLine("The Boss casts Fire!");
                break;
            case 2:
                damage = 20;
                Console.WriteLine("The Boss casts Ice!");
                break;
            case 3:
                damage = 30;
                Console.WriteLine("The Boss casts Lightning!");
                break;
        }
        return damage;
    }

    // Add the UsePotion method here
    private void UsePotion(Player player)
    {
        if (player.Buffs.Contains("Health Potion"))
        {
            player.HP += 50; // Larger healing potion for increased difficulty
            player.Buffs.Remove("Health Potion");
            Console.WriteLine("You used a Health Potion and regained 50 HP!");
        }
        else
        {
            Console.WriteLine("You don't have a Health Potion.");
        }
    }
}