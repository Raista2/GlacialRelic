using System;
using System.Collections.Generic;

// Main Game Logic
public class Game
{
    private static void Main(string[] args)
    {
        Player player = Player.Instance;

        Console.WriteLine("Welcome to the Dungeon!");
        player.DisplayStats();

        // Progress through levels
        for (int level = 1; level <= 5; level++)
        {
            Console.WriteLine($"\n=== Level {level} ===");

            // If it's level 5, the player encounters the boss
            if (level == 5)
            {
                Boss finalBoss = new Boss("Final Boss", 300, 30);
                finalBoss.Enter(player);
                break;
            }

            CombatRegion combatRegion = new CombatRegion();
            combatRegion.Enter(player);

            // Remove all debuffs after each level's encounter
            player.ClearDebuffs();

            // If player is dead, end the game
            if (player.HP <= 0)
            {
                break;
            }
        }

        Console.WriteLine("Game Over!");
    }
}
