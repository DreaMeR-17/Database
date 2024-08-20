using System;
using System.Collections.Generic;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandRemovePlayer = "2";
            const string CommandBannPlayer = "3";
            const string CommandUnbannPlayer = "4";
            const string CommandShowPlayers = "5";
            const string CommandQuit = "0";

            Database database = new Database();

            bool isWork = true;

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
                        database.AddPlayer();
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
        private Dictionary<int, Player> _player = new Dictionary<int, Player>();

        public void AddPlayer()
        {
            Console.Clear();

            Console.WriteLine("Напишите имя игрока: ");
            string name = (Console.ReadLine());

            Console.WriteLine("Введите уровень игрока: ");
            string level = (Console.ReadLine());

            Player player = new Player(name, level);

            while (_player.ContainsKey(player.Number) == true)
            {
                player = new Player(name, level);
            }

            _player.Add(player.Number, player);

            Console.Clear();
        }

        public void RemovePlayer()
        {
            Console.Clear();

            Console.WriteLine("Напишите номер игрока которого хотите удалить:");
            int number = GetNumber();

            if (_player.ContainsKey(number))
            {
                _player.Remove(number);
                Console.WriteLine($"Игрок под номером {number} был удален");
            }
            else
            {
                Console.WriteLine($"Игрок с номером {number} не найден");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void Ban()
        {
            Console.Clear();

            if (TryGetPlayer(out Player gettedPlayer) == true)
            {
                gettedPlayer.Ban();

                Console.WriteLine($"Игрок с номером {gettedPlayer.Number} забанен.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void Unban()
        {
            Console.Clear();

            if (TryGetPlayer(out Player gettedPlayer) == true)
            {
                gettedPlayer.Unban();

                Console.WriteLine($"Игрок с номером {gettedPlayer.Number} раззабанен.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void ShowPlayers()
        {
            Console.Clear();

            foreach (var player in _player.Values)
            {
                Console.WriteLine($"Номер: {player.Number}, его имя: {player.Name}, уровень игрока: {player.Level}, " +
                                    $"состояние аккаунта игрока: {(player.IsBanned ? "Забанен" : "Не забанен")}");

                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }

        private bool TryGetPlayer(out Player gettedPlayer)
        {
            if (_player.Count != 0)
            {
                Console.WriteLine("Введите номер игрока :");

                int number = GetNumber();

                if (_player.ContainsKey(number))
                {
                    gettedPlayer = _player[number];

                    return true;
                }
                else
                {
                    Console.WriteLine($"Игрок с номером {number} не найден.");
                    gettedPlayer = null;

                    return false;
                }
            }
            else
            {
                Console.WriteLine("База данных пуста.");
                gettedPlayer = null;

                return false;
            }
        }

        private int GetNumber()
        {
            int number = 0;
            string userInput = Console.ReadLine();

            while (int.TryParse(userInput, out number) != true)
            {
                Console.Write($"Неверный формат {userInput}.\nВведите целочисленное значение:");
                userInput = Console.ReadLine();
            }

            return number;
        }
    }

    class Player
    {
        private static int s_number = 0;

        public Player(string name, string level)
        {
            Number = ++s_number;
            Name = name;
            Level = level;
            IsBanned = false;
        }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Level { get; private set; }
        public bool IsBanned { get; private set; }

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
