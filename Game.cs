using System;
using System.Collections.Generic;
using System.Threading;

// Main Game Class
public class Game
{
    private readonly Player player;
    private readonly Random random;
    private readonly string[] regionTypes = { "shop", "buff", "combat" };
    private int currentLevel;
    private const int MaxLevel = 5;

    public Game()
    {
        player = Player.Instance;
        random = new Random();
        currentLevel = 1;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Roguelike Dungeon!");
        
        while (currentLevel <= MaxLevel && player.HP > 0)
        {
            Console.WriteLine($"\n=== Level {currentLevel} ===");
            player.DisplayStats();
            
            if (currentLevel == MaxLevel)
            {
                // Boss battle on final level
                var bossRegion = RegionFactory.CreateRegion("boss");
                bossRegion.Enter(player);
                
                if (player.HP > 0)
                {
                    Console.WriteLine("\nCongratulations! You've defeated the Dungeon Master!");
                    return;
                }
            }
            else
            {
                // Random region for normal levels
                string regionType = regionTypes[random.Next(regionTypes.Length)];
                var region = RegionFactory.CreateRegion(regionType);
                region.Enter(player);
                
                Console.WriteLine("\nPress any key to continue to the next level...");
                Console.ReadKey();
                currentLevel++;
            }
        }

        if (player.HP <= 0)
        {
            Console.WriteLine("\nGame Over! You have been defeated!");
        }
    }
}