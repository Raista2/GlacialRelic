using System;
using System.Collections.Generic;
using System.Threading;

public class BuffRegion : Region
{
    private readonly string[] possibleBuffs = { "Strength Up", "Defense Up", "Speed Up" };

    public override void Enter(Player player)
    {
        Random rnd = new Random();
        string buff = possibleBuffs[rnd.Next(possibleBuffs.Length)];
        player.Buffs.Add(buff);
        Console.WriteLine($"\nYou've entered a mysterious shrine!");
        Console.WriteLine($"You received the {buff} buff!");
    }
}