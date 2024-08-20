using System;
using System.Collections.Generic;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            bool isWork = true;

            const string CommandAddPlayer = "1";
            const string CommandRemovePlayer = "2";
            const string CommandBannPlayer = "3";
            const string CommandUnbannPlayer = "4";
            const string CommandShowPlayers = "5";
            const string CommandQuit = "0";

            while (isWork)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine($"{CommandAddPlayer} - Добавить игрока");
                Console.WriteLine($"{CommandRemovePlayer} - Удалить игрока");
                Console.WriteLine($"{CommandBannPlayer} - Забанить игрока");
                Console.WriteLine($"{CommandUnbannPlayer} - Разбанить игрока");
                Console.WriteLine($"{CommandShowPlayers} - Показать всех игроков");
                Console.WriteLine($"{CommandQuit} - Выйти");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddPlayer:
                        Player player = new Player();
                        database.AddPlayer(player);
                        break;

                    case CommandRemovePlayer:
                        database.RemovePlayer();
                        break;

                    case CommandBannPlayer:
                        database.Ban();
                        break;

                    case CommandUnbannPlayer:
                        database.Unban();
                        break;

                    case CommandShowPlayers:
                        database.ShowPlayers();
                        break;

                    case CommandQuit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Нет такого выбора в списке, попробуйте ещё раз.");                        
                        break;
                }
            }
        }
    }

    class Database
    {
        private Dictionary<string, Player> _playerInfo = new Dictionary<string, Player>();

        public void AddPlayer(Player player)
        {
            Console.Clear();

            Console.WriteLine("Напишите имя игрока: ");
            player.SetName(Console.ReadLine());

            Console.WriteLine("Напишите уникальный номер игрока: ");
            SetNumber(player);

            Console.WriteLine("Введите уровень игрока: ");
            player.SetLevel(Console.ReadLine());

            Console.WriteLine("Напишите статус игрока(Забанен | Не забанен): ");
            AddBanInfo(player);

            _playerInfo.Add(player.Number, player);

            Console.Clear();
        }

        public void RemovePlayer()
        {
            Console.Clear();

            Console.WriteLine("Напишите номер игрока которого хотите удалить:");
            string userInput = Console.ReadLine();

            if (_playerInfo.ContainsKey(userInput))
            {
                _playerInfo.Remove(userInput);
                Console.WriteLine($"Игрок под номером {userInput} был удален");
            }
            else
            {
                Console.WriteLine($"Игрок с номером {userInput} не найден");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void Ban()
        {
            Console.Clear();

            Console.WriteLine("Введите номер игрока:");
            string userInput = Console.ReadLine();

            if (_playerInfo.ContainsKey(userInput))
            {
                _playerInfo[userInput].Ban();
                Console.WriteLine($"Игрок под номером {userInput} был забанен");
            }
            else
            {
                Console.WriteLine("Игрок с данным номером не найден");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void Unban()
        {
            Console.Clear();

            Console.WriteLine("Введите номер игрока:");
            string userInput = Console.ReadLine();

            if (_playerInfo.ContainsKey(userInput))
            {
                _playerInfo[userInput].Unban();
                Console.WriteLine($"Игрок под номером {userInput} был разбанен");
            }
            else
            {
                Console.WriteLine("Игрок с данным номером не найден");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void ShowPlayers()
        {
            Console.Clear();

            foreach (var player in _playerInfo.Values)
            {
                Console.WriteLine($"Номер: {player.Number}, его имя: {player.Name}, уровень игрока: {player.Level}, " +
                                    $"состояние аккаунта игрока: {(player.IsBanned ? "Забанен" : "Не забанен" )}");

                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void SetNumber(Player player)
        {
            bool isNumberCorrect = false;

            while (!isNumberCorrect)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out _))
                {
                    player.SetNumber(userInput);
                    isNumberCorrect = true;
                }
                else
                {
                    Console.WriteLine("Неверный номер.");
                }
            }
        }

        public void AddBanInfo(Player player)
        {
            string banned = "Забанен";
            string notBanned = "Не забанен";
            bool isVerificationDone = false;

            while (!isVerificationDone)
            {
                string userInputBann = Console.ReadLine();

                if (userInputBann == banned)
                {
                    player.Ban();
                    isVerificationDone = true;
                }
                else if (userInputBann == notBanned)
                {
                    player.Unban();
                    isVerificationDone = true;
                }
                else
                {
                    Console.WriteLine("Нет такой команды, попробуйте ещё раз");
                }
            }
        }
    }

    class Player
    {
        public string Number { get; private set; }
        public string Name { get; private set; }
        public string Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void SetNumber(string number)
        {
            Number = number;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetLevel(string level)
        {
            Level = level;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }
}
