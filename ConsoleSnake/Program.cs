using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Timers;
using System.Threading;

namespace ConsoleSnake
{
  enum Direction
    {
        LEFT, 
        RIGHT,
        UP,
        DOWN
    }
    class Program
    {
        static readonly int x = 80; //buffer and window sizes
        static readonly int y = 26;
        static int score = 0;
        static bool accesCycle = true;
        static Walls walls;
        static Snake snake;
        static FoodFactory foodFactory;
        static System.Threading.Timer time;
        static void Main(string[] args)
        { 
            ConsoleConfig();
            walls = new Walls(x, y, '#');
            snake = new Snake(x / 2, y / 2, 3);
            foodFactory = new FoodFactory(x, y, '$');
            foodFactory.CreateFood();
            time = new System.Threading.Timer(Loop, null, 0, 200);

            while (accesCycle == true)
            {
                if (Console.KeyAvailable) // нажата ли клавиша 
                {
                    ConsoleKeyInfo key = Console.ReadKey(); // возвращает нажатые клавиши
                    snake.Rotation(key.Key);
                }
            }
            Console.ReadLine();
        }

        static void ConsoleConfig() // Console configuration on start app 
        {
            Console.Title = "Snake console";
            Console.SetWindowPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetWindowSize(x + 1, y + 1);
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false; // скрытие отображение курсора
        }
        static void GameOver()
        {
            accesCycle = false;
            Console.Clear();
            Console.SetCursorPosition(30, 12);
            Console.Write("Score: " + score);
        }
        static void Loop(object obj)
        {
            if (walls.IsHit(snake.GetHead()) || snake.IsHit(snake.GetHead()))
            {
                //time.Change(0, Timeout.Infinite); я так и не понял зачем это, но оно мешает
                GameOver();
            }
            else if (snake.Eat(foodFactory.food))
            {
                foodFactory.CreateFood();
                score++;
            }
            else
            {
                snake.Move();
            }
        }
    }


    struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) => 
        new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        public static bool operator == (Point a, Point b) =>
            (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
            (a.x != b.x && a.y != b.y) ? true : false;

        public void Draw() //вывод символа
        {
            DrawPoint(ch);
        }

        public void Clear() // очистка
        {
            DrawPoint(' ');
        }

        private void DrawPoint(char tch)
        {
            Console.SetCursorPosition(x, y); //задает координаты курсору
            Console.Write(tch); //выводит символ который указан в параметрах
        }
    }
}
