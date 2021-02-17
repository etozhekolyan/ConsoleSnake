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
        static Walls walls;
        static Snake snake;
        static FoodFactory foodFactory;
        static System.Threading.Timer time;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(x + 1, y + 1); 
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false; // скрытие отображение курсора

            walls = new Walls(x, y, '#');
            snake = new Snake(x / 2, y / 2, 3);
            foodFactory = new FoodFactory(x, y, '@');
            foodFactory.CreateFood();
            time = new System.Threading.Timer(Loop, null, 0, 200);

            while (true)
            {
                if (Console.KeyAvailable) // нажата ли клавиша 
                {
                    ConsoleKeyInfo key = Console.ReadKey(); // возвращает нажатые клавиши
                    snake.Rotation(key.Key);
                }
            }

            /*возможно, стоит потом сделать метод для конфигурации консоли*/
            Console.ReadLine(); //без этой срани консоль постоянно выводит путь к файлу и все портит
        }
        static void Loop(object obj)
        {
            if (walls.IsHit(snake.GetHead()) || snake.IsHit(snake.GetHead()))
            {
                time.Change(0, Timeout.Infinite);
            }
            else if (snake.Eat(foodFactory.food))
            {
                foodFactory.CreateFood();
            }
            else
            {
                snake.Move();
            }
        }
    }


    struct Point
    {
        /*эта структура выводит на консоль "графику". Другими словами это тип данных - точка, которая сожержит координаты и символы которые будут выводиться
         * на консоль*/
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
