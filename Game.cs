using System;
using System.Collections.Generic;

// Main Game Logic
public class Game
{
    public static void Main(string[] args)
    {
        ShowMainMenu();
        ChoosePlayerRole();
        Player player = Player.Instance;

        Console.WriteLine("\nGame is starting...");
        Console.WriteLine("Welcome to the Glacial Relic!");
        Console.WriteLine("You are an adventurer seeking the Glacial Relic, a powerful artifact.");
        Console.WriteLine("You must traverse through various regions and defeat enemies to reach the final level.");
        Console.WriteLine("Good luck!");
        
        for (int level = 1; level <= 5; level++)
        {
            Console.WriteLine($"\n=== Level {level} ===");

            // If it's level 4, the player encounters a shop
            if (level == 4)
            {
                Shop shop = new Shop();
                shop.OpenShop(player);
                continue;
            }

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

    private static void ShowMainMenu()
    {
        Console.WriteLine("  ________.__                .__       .__    __________       .__  .__        ");
        Console.WriteLine(" /  _____/|  | _____    ____ |__|____  |  |   \\______   \\ ____ |  | |__| ____  ");
        Console.WriteLine("/   \\  ___|  | \\__  \\ _/ ___\\|  \\__  \\ |  |    |       _// __ \\|  | |  |/ ___\\ ");
        Console.WriteLine("\\    \\_\\  \\  |__/ __ \\\\  \\___|  |/ __ \\|  |__  |    |   \\  ___/|  |_|  \\  \\___ ");
        Console.WriteLine(" \\______  /____(____  /\\___  >__(____  /____/  |____|_  /\\___  >____/__|\\___  >");
        Console.WriteLine("        \\/          \\/     \\/        \\/               \\/     \\/            \\/ ");
        Console.WriteLine("======================================= Main Menu =======================================");
        Console.WriteLine("1. Start Game");
        Console.WriteLine("2. Options");
        Console.WriteLine("3. Exit");
        Console.WriteLine("==========================================================================================");

        Console.Write("Choose one of the Options: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Starting Game...");
                break;
            case "2":
                Console.WriteLine("Belum ada opsi tambahan.");
                // TODO: Tambahkan pengaturan jika diperlukan
                break;
            case "3":
                Console.WriteLine("Exit Game.");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Option Invalid.");
                ShowMainMenu();
                break;
        }
    }

    private static void ChoosePlayerRole()
    {
        Console.WriteLine("\n=== Choose Role ===");
        Console.WriteLine("1. Fighter");
        Console.WriteLine("   - More HP, Less MP, Higher Normal Damage.");
        Console.WriteLine("2. Magician");
        Console.WriteLine("   - Less HP, More MP, Higher Magic Damage.");
        Console.WriteLine("=================");
        Console.Write("Choose role (1/2): ");

        string roleChoice = Console.ReadLine();
        Player player = Player.Instance;

        switch (roleChoice)
        {
            case "1":
                Console.WriteLine("You choose Fighter!");
                player.HP = 300;
                player.MP = 50;
                // Damage normal lebih tinggi
                break;
            case "2":
                Console.WriteLine("You choose Magician!");
                player.HP = 150;
                player.MP = 200;
                // Damage magic lebih tinggi
                break;
            default:
                Console.WriteLine("Pilihan tidak valid. Pilih ulang.");
                ChoosePlayerRole();
                break;
        }
    }
}