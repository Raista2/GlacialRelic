using System;
using System.Collections.Generic;
using System.Threading;

// Singleton Pattern untuk Player
public class Player
{
    private static Player instance;
    private static readonly object padlock = new object();

    public int HP { get; set; }
    public int MP { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public List<string> Buffs { get; set; }

    private Player()
    {
        HP = 100;
        MP = 50;
        Level = 1;
        Gold = 0;
        Buffs = new List<string>();
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
    }
}