using System;
using System.Collections.Generic;

class DungeonGame
{
    static Random random = new Random();

    
    static int health = 100;
    static int gold = 0;
    static int potions = 3;
    static int arrows = 5;
    static string[] inventory = new string[5];
    static int inventorySize = 0;

    
    static string[] dungeonMap = new string[10];
    static string[] events = { "Монстр", "Ловушка", "Сундук", "Торговец", "Пусто", "Boss" };

    static void Main(string[] args)
    {

        for (int i = 0; i < 9; i++)
        {
            dungeonMap[i] = events[random.Next(events.Length - 1)];  
        }
        dungeonMap[9] = "Boss"; 

        Console.WriteLine("Добро пожаловать в подземелье! Ваша цель - победить босса.");

       
        for (int room = 0; room < 10; room++)
        {
            Console.WriteLine($"\nВы вошли в комнату {room + 1}: {dungeonMap[room]}");
            ProcessRoom(dungeonMap[room]); 

            if (health <= 0)
            {
                Console.WriteLine("Вы погибли. Игра окончена.");
                break;
            }
            if (room == 9 && health > 0)
            {
                Console.WriteLine("Поздравляю! Вы победили босса и выиграли игру!");
                break;
            }
        }
    }


    static void ProcessRoom(string roomEvent)
    {
        switch (roomEvent)
        {
            case "Монстр":
                FightMonster();
                break;
            case "Ловушка":
                TriggerTrap();
                break;
            case "Сундук":
                OpenChest();
                break;
            case "Торговец":
                VisitMerchant();
                break;
            case "Пусто":
                Console.WriteLine("Комната пуста. Вы продолжаете свой путь.");
                break;
            case "Boss":
                FightBoss();
                break;
        }
    }


    static void FightMonster()
    {
        int monsterHealth = random.Next(20, 51);  
        Console.WriteLine($"Вы встретили монстра с {monsterHealth} HP!");

        while (monsterHealth > 0 && health > 0)
        {
            Console.WriteLine($"Ваше здоровье: {health}, Здоровье монстра: {monsterHealth}");
            Console.WriteLine("Выберите оружие: 1. Меч (10-20 урона), 2. Лук (5-15 урона, стрелы: {arrows})");
            string choice = Console.ReadLine();

            int damage = 0;
            if (choice == "1")  
            {
                damage = random.Next(10, 21);
                Console.WriteLine($"Вы нанесли монстру {damage} урона мечом!");
            }
            else if (choice == "2" && arrows > 0)  
            {
                damage = random.Next(5, 16);
                arrows--;
                Console.WriteLine($"Вы нанесли монстру {damage} урона луком! Осталось стрел: {arrows}");
            }
            else if (choice == "2" && arrows == 0)
            {
                Console.WriteLine("У вас нет стрел для лука!");
                continue;
            }

            monsterHealth -= damage;
            if (monsterHealth <= 0)
            {
                Console.WriteLine("Вы победили монстра!");
                int exp = random.Next(10, 21);
                health += exp;  
                Console.WriteLine($"Вы восстанавливаете {exp} здоровья. Ваше здоровье: {health}");
            }
            else
            {
                int monsterDamage = random.Next(5, 16);
                health -= monsterDamage;
                Console.WriteLine($"Монстр атакует и наносит вам {monsterDamage} урона.");
            }

            if (health <= 0)
            {
                Console.WriteLine("Ваше здоровье иссякло! Вы погибли.");
                break;
            }
        }
    }


    static void TriggerTrap()
    {
        int trapDamage = random.Next(10, 21);
        health -= trapDamage;
        Console.WriteLine($"Вы попали в ловушку и потеряли {trapDamage} здоровья.");
    }


    static void OpenChest()
    {
        Console.WriteLine("В сундуке загадка. Решите её, чтобы получить награду.");
        int num1 = random.Next(1, 11);
        int num2 = random.Next(1, 11);
        int correctAnswer = num1 + num2;

        Console.WriteLine($"Сколько будет {num1} + {num2}?");
        int answer = Convert.ToInt32(Console.ReadLine());

        if (answer == correctAnswer)
        {
            string reward = GetRandomChestReward();
            Console.WriteLine($"Вы правильно ответили! Вы нашли {reward}.");
            AddToInventory(reward);
        }
        else
        {
            Console.WriteLine("Ответ неверный. Попробуйте снова.");
        }
    }


    static string GetRandomChestReward()
    {
        string[] rewards = { "зелье", "золото", "стрелы" };
        return rewards[random.Next(rewards.Length)];
    }


    static void AddToInventory(string item)
    {
        if (inventorySize < 5)
        {
            inventory[inventorySize] = item;
            inventorySize++;
            if (item == "зелье") potions++;
            if (item == "золото") gold += 10;
            if (item == "стрелы") arrows += 3;
        }
        else
        {
            Console.WriteLine("Ваш инвентарь полон. Вы не можете взять больше предметов.");
        }
    }


    static void VisitMerchant()
    {
        Console.WriteLine("Торговец предлагает вам купить зелье за 30 золота.");
        if (gold >= 30)
        {
            Console.WriteLine("Вы купили зелье!");
            gold -= 30;
            AddToInventory("зелье");
        }
        else
        {
            Console.WriteLine("У вас недостаточно золота.");
        }
    }


    static void FightBoss()
    {
        int bossHealth = 150;
        Console.WriteLine($"Вы встретили Босса с {bossHealth} HP!");

        while (bossHealth > 0 && health > 0)
        {
            Console.WriteLine($"Ваше здоровье: {health}, Здоровье босса: {bossHealth}");
            Console.WriteLine("Выберите оружие: 1. Меч (10-20 урона), 2. Лук (5-15 урона, стрелы: {arrows})");
            string choice = Console.ReadLine();

            int damage = 0;
            if (choice == "1")
            {
                damage = random.Next(10, 21);
                Console.WriteLine($"Вы нанесли боссу {damage} урона мечом!");
            }
            else if (choice == "2" && arrows > 0)
            {
                damage = random.Next(5, 16);
                arrows--;
                Console.WriteLine($"Вы нанесли боссу {damage} урона луком! Осталось стрел: {arrows}");
            }
            else if (choice == "2" && arrows == 0)
            {
                Console.WriteLine("У вас нет стрел для лука!");
                continue;
            }

            bossHealth -= damage;
            if (bossHealth <= 0)
            {
                Console.WriteLine("Вы победили Босса! Поздравляем, вы выиграли!");
                break;
            }
            else
            {
                int bossDamage = random.Next(10, 21);
                health -= bossDamage;
                Console.WriteLine($"Босс атакует и наносит вам {bossDamage} урона.");
            }

            if (health <= 0)
            {
                Console.WriteLine("Ваше здоровье иссякло! Вы погибли.");
                break;
            }
        }
    }
}
