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

// CombatRegion with Separated Normal and Elemental Attacks
public class CombatRegion : Region
{
    private readonly string[] enemyTypes = { "Goblin", "Orc", "Skeleton", "Bandit" };
    private readonly Random random = new Random();
    private string enemyType;
    private int enemyHP;
    private int enemyAttack;

    public override void Enter(Player player)
    {
        if (player.Level == 5)
        {
            // Boss Encounter on 5th Level
            enemyType = "Boss";
            enemyHP = 150;
            enemyAttack = 30;
        }
        else
        {
            // Regular Enemy Encounter
            enemyType = enemyTypes[random.Next(enemyTypes.Length)];
            enemyHP = random.Next(30, 60);
            enemyAttack = random.Next(10, 20);
        }

        string enemyNextAction = DetermineEnemyAction();

        Console.WriteLine($"\nYou've encountered a {enemyType}!");
        Console.WriteLine($"Enemy HP: {enemyHP}");

        while (enemyHP > 0 && player.HP > 0)
        {
            Console.WriteLine($"\nEnemy's Next Action: {enemyNextAction}");
            Console.WriteLine("\nYour Move:");
            Console.WriteLine("1. Normal Attack");
            Console.WriteLine("2. Elemental Attack (Cost: 10 MP)");
            Console.WriteLine("3. Defend (reduce incoming damage)");
            Console.WriteLine("4. Use Potion");
            Console.WriteLine("5. Attempt to Flee");

            string choice = Console.ReadLine();
            int damage;

            // Clear the console after each action
            Console.Clear();

            switch (choice)
            {
                case "1": // Normal Attack
                    damage = NormalAttack();
                    enemyHP -= damage;
                    Console.WriteLine($"You performed a Normal Attack and dealt {damage} damage!");
                    break;

                case "2": // Elemental Attack
                    if (player.MP >= 10)
                    {
                        damage = ElementalAttack();
                        player.MP -= 10;  // Deduct MP for Elemental Attack
                        enemyHP -= damage;
                        Console.WriteLine($"You performed an Elemental Attack and dealt {damage} damage!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough MP to perform an Elemental Attack!");
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

            // Determine the enemy's next action
            enemyNextAction = DetermineEnemyAction();

            // Clear the console after enemy action
            Console.Clear();
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

    private int NormalAttack()
    {
        // Normal attack does fixed damage (e.g., 15)
        return 15;
    }

    private int ElementalAttack()
    {
        Console.WriteLine("Choose your Elemental Attack:");
        Console.WriteLine("1. Fire");
        Console.WriteLine("2. Ice");
        Console.WriteLine("3. Lightning");
        string elementalChoice = Console.ReadLine();

        int damage = elementalChoice switch
        {
            "1" => random.Next(20, 30), // Fire
            "2" => random.Next(15, 25), // Ice
            "3" => random.Next(25, 35), // Lightning
            _ => random.Next(15, 20), // Default (fallback)
        };
        return damage;
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

            // Choose between fight or shop
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Shop/Buff");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Simulate a combat encounter
                var combatRegion = new CombatRegion();
                combatRegion.Enter(player);
            }
            else if (choice == "2")
            {
                // Open the shop
                var shop = new Shop();
                shop.OpenShop(player);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            Console.WriteLine("\nPress any key to continue to the next level...");
            Console.ReadKey();
            currentLevel++;

            // Clear the console before continuing to the next level
            Console.Clear();
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
