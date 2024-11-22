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

// Abstract Region Class
public abstract class Region
{
    public abstract void Enter(Player player);
}

// CombatRegion with Improved Mechanics
public class CombatRegion : Region
{
    private readonly string[] enemyTypes = { "Goblin", "Orc", "Skeleton", "Bandit" };
    private readonly Random random = new Random();

    public override void Enter(Player player)
    {
        string enemyType = enemyTypes[random.Next(enemyTypes.Length)];
        int enemyHP = random.Next(30, 60);
        int enemyAttack = random.Next(10, 20);
        string enemyNextAction = DetermineEnemyAction();

        Console.WriteLine($"\nYou've encountered a {enemyType}!");
        Console.WriteLine($"Enemy HP: {enemyHP}");

        while (enemyHP > 0 && player.HP > 0)
        {
            Console.WriteLine($"\nEnemy's Next Action: {enemyNextAction}");
            Console.WriteLine("\nYour Move:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Magic (costs 20 MP)");
            Console.WriteLine("3. Defend (reduce incoming damage)");
            Console.WriteLine("4. Use Potion");
            Console.WriteLine("5. Attempt to Flee");

            string choice = Console.ReadLine();
            int damage;

            switch (choice)
            {
                case "1": // Attack
                    damage = random.Next(15, 25);
                    enemyHP -= damage;
                    Console.WriteLine($"You attacked the {enemyType} and dealt {damage} damage!");
                    break;

                case "2": // Use Magic
                    if (player.MP >= 20)
                    {
                        damage = random.Next(30, 45);
                        enemyHP -= damage;
                        player.MP -= 20;
                        Console.WriteLine($"You cast a spell and dealt {damage} damage!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough MP!");
                    }
                    break;

                case "3": // Defend
                    Console.WriteLine("You brace yourself to reduce incoming damage.");
                    player.Buffs.Add("Defend");
                    break;

                case "4": // Use Potion
                    UsePotion(player);
                    break;

                case "5": // Flee
                    if (random.Next(0, 2) == 0) // 50% chance to flee
                    {
                        Console.WriteLine("You successfully fled!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You failed to flee!");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    continue;
            }

            // Enemy performs action
            ExecuteEnemyAction(enemyNextAction, player, ref enemyAttack);

            // Determine the enemy's next action for the next turn
            enemyNextAction = DetermineEnemyAction();
        }

        if (player.HP > 0)
        {
            Console.WriteLine($"\nYou defeated the {enemyType}!");
            int goldReward = random.Next(10, 30);
            player.Gold += goldReward;
            Console.WriteLine($"You received {goldReward} gold!");
        }
        else
        {
            Console.WriteLine("\nYou were defeated by the enemy!");
        }
    }

    private string DetermineEnemyAction()
    {
        string[] actions = { "Attack", "Defend", "Special Attack" };
        return actions[random.Next(actions.Length)];
    }

    private void ExecuteEnemyAction(string action, Player player, ref int enemyAttack)
    {
        Random rnd = new Random();
        switch (action)
        {
            case "Attack":
                int damage = rnd.Next(enemyAttack / 2, enemyAttack);
                if (player.Buffs.Contains("Defend"))
                {
                    damage /= 2;
                    Console.WriteLine("Your defense reduced the damage!");
                    player.Buffs.Remove("Defend");
                }
                player.HP -= damage;
                Console.WriteLine($"The enemy attacked you for {damage} damage!");
                break;

            case "Defend":
                Console.WriteLine("The enemy defends, reducing your next attack's damage!");
                enemyAttack += 5; // Temporary increase in defense
                break;

            case "Special Attack":
                int specialDamage = rnd.Next(enemyAttack + 5, enemyAttack + 15);
                player.HP -= specialDamage;
                Console.WriteLine($"The enemy used a special attack and dealt {specialDamage} damage!");
                break;
        }
    }

    private void UsePotion(Player player)
    {
        if (player.Buffs.Contains("Health Potion"))
        {
            player.HP += 30;
            player.Buffs.Remove("Health Potion");
            Console.WriteLine("You used a Health Potion and restored 30 HP!");
        }
        else if (player.Buffs.Contains("Mana Potion"))
        {
            player.MP += 20;
            player.Buffs.Remove("Mana Potion");
            Console.WriteLine("You used a Mana Potion and restored 20 MP!");
        }
        else
        {
            Console.WriteLine("You don't have any potions!");
        }
    }
}

// BossRegion with Action Prediction
public class BossRegion : Region
{
    public override void Enter(Player player)
    {
        Console.WriteLine("\nBOSS BATTLE! The Dungeon Master awaits!");

        int bossHP = 200;
        int bossAttack = 25;
        string bossNextAction = DetermineBossAction();

        while (bossHP > 0 && player.HP > 0)
        {
            Console.WriteLine($"\nBoss HP: {bossHP}");
            Console.WriteLine($"Boss's Next Action: {bossNextAction}");
            Console.WriteLine("\nYour Move:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Magic (costs 20 MP)");
            Console.WriteLine("3. Defend (reduce incoming damage)");
            Console.WriteLine("4. Use Potion");

            string choice = Console.ReadLine();
            int damage;

            switch (choice)
            {
                case "1": // Attack
                    damage = new Random().Next(20, 30);
                    bossHP -= damage;
                    Console.WriteLine($"You attacked the boss and dealt {damage} damage!");
                    break;

                case "2": // Use Magic
                    if (player.MP >= 20)
                    {
                        damage = new Random().Next(35, 50);
                        bossHP -= damage;
                        player.MP -= 20;
                        Console.WriteLine($"You cast a spell and dealt {damage} damage!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough MP!");
                    }
                    break;

                case "3": // Defend
                    Console.WriteLine("You brace yourself to reduce incoming damage.");
                    player.Buffs.Add("Defend");
                    break;

                case "4": // Use Potion
                    UsePotion(player);
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    continue;
            }

            // Boss performs action
            ExecuteBossAction(bossNextAction, player, bossAttack);

            // Determine the boss's next action
            bossNextAction = DetermineBossAction();
        }

        if (player.HP > 0)
        {
            Console.WriteLine("\nCongratulations! You defeated the Dungeon Master!");
        }
        else
        {
            Console.WriteLine("\nYou were defeated by the Dungeon Master!");
        }
    }

    private string DetermineBossAction()
    {
        string[] actions = { "Attack", "Defend", "Special Attack" };
        return actions[new Random().Next(actions.Length)];
    }

    private void ExecuteBossAction(string action, Player player, int bossAttack)
    {
        Random rnd = new Random();
        switch (action)
        {
            case "Attack":
                int damage = rnd.Next(bossAttack / 2, bossAttack);
                if (player.Buffs.Contains("Defend"))
                {
                    damage /= 2;
                    Console.WriteLine("Your defense reduced the damage!");
                    player.Buffs.Remove("Defend");
                }
                player.HP -= damage;
                Console.WriteLine($"The boss attacked you for {damage} damage!");
                break;

            case "Defend":
                Console.WriteLine("The boss defends, reducing your next attack's damage!");
                break;

            case "Special Attack":
                int specialDamage = rnd.Next(bossAttack + 10, bossAttack + 30);
                player.HP -= specialDamage;
                Console.WriteLine($"The boss used a special attack and dealt {specialDamage} damage!");
                break;
        }
    }

    private void UsePotion(Player player)
    {
        if (player.Buffs.Contains("Health Potion"))
        {
            player.HP += 30;
            player.Buffs.Remove("Health Potion");
            Console.WriteLine("You used a Health Potion and restored 30 HP!");
        }
        else if (player.Buffs.Contains("Mana Potion"))
        {
            player.MP += 20;
            player.Buffs.Remove("Mana Potion");
            Console.WriteLine("You used a Mana Potion and restored 20 MP!");
        }
        else
        {
            Console.WriteLine("You don't have any potions!");
        }
    }
}

// Factory for creating regions
public class RegionFactory
{
    public static Region CreateRegion(string type)
    {
        return type switch
        {
            "combat" => new CombatRegion(),
            "boss" => new BossRegion(),
            _ => throw new ArgumentException("Invalid region type"),
        };
    }
}

// Main Game Class
public class Game
{
    private readonly Player player;
    private readonly Random random;
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
                // Random combat region for normal levels
                var combatRegion = RegionFactory.CreateRegion("combat");
                combatRegion.Enter(player);

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

// Program Entry Point
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}
