using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace ConsoleSnake
{
    class Program
    {
        static readonly int x = 80; //buffer and window sizes
        static readonly int y = 26;
        static Walls walls;
        static void Main(string[] args)
        {
            Console.SetWindowSize(x + 1, y + 1); 
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false; // скрытие отображение курсора

            walls = new Walls(x, y, '#');
            FoodFactory food = new FoodFactory(x, y, '%');
            food.CreateFood();

            /*возможно, стоит потом сделать метод для конфигурации консоли*/
            Console.ReadLine(); //без этой срани консоль постоянно выводит путь к файлу и все портит
        }
    }

    struct Point
    {
        /*эта структура выводит на консоль "графику". Другими словами это тип данных - точка, которая сожержит координаты и символы которые будут выводиться
         * на консоль*/
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) =>  //TODO разобрать что делают эти две строчки
        new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        

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
