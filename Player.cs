using System;
using System.Collections.Generic;

// Singleton Pattern for Player
public class Player
{
    private static Player instance;
    private static readonly object padlock = new object();

    public int HP { get; set; }
    public int MP { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public List<string> Buffs { get; set; }
    public List<string> Debuffs { get; set; }

    private Player()
    {
        // Increased Player Stats
        HP = 200; // Starting HP increased
        MP = 100; // Starting MP increased
        Level = 1;
        Gold = 0;
        Buffs = new List<string>();
        Debuffs = new List<string>();  // List to track debuffs
        // Give the player a health potion at the start of the game
        Buffs.Add("Health Potion");
    }

    public static Player Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine("\n=== Player Stats ===");
        Console.WriteLine($"HP: {HP}");
        Console.WriteLine($"MP: {MP}");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Gold: {Gold}");
        Console.WriteLine("Active Buffs:");
        foreach (var buff in Buffs)
        {
            Console.WriteLine($"- {buff}");
        }
        Console.WriteLine("Active Debuffs:");
        foreach (var debuff in Debuffs)
        {
            Console.WriteLine($"- {debuff}");
        }
    }

    // Apply the debuff effects
    public void ApplyDebuffEffects()
    {
        if (Debuffs.Contains("Poisoned"))
        {
            // Poison deals 5 damage each turn
            HP -= 5;
            Console.WriteLine("You are poisoned! You lose 5 HP due to poison.");
        }

        if (Debuffs.Contains("Weakened"))
        {
            // Weakened reduces the player's damage output by half
            Console.WriteLine("You are weakened! Your attacks deal half the damage.");
        }
    }

    public void RemoveDebuff(string debuff)
    {
        Debuffs.Remove(debuff);
        Console.WriteLine($"{debuff} has worn off.");
    }

    // Clear all debuffs after combat
    public void ClearDebuffs()
    {
        Debuffs.Clear();
        Console.WriteLine("All debuffs have been cleared after the battle.");
    }
}