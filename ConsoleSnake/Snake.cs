using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleSnake
{
    class Snake
    {
        private List<Point> snake;
        private Direction direction;
        private int step = 1;
        private Point tail;
        private Point head;
        bool rotate = true;
        
        public Snake (int x, int y, int length)
        {
            direction = Direction.RIGHT;
            snake = new List<Point>();
            for (int i = x - length; i < x; i++)
            {
                Point p = (i, y, '*');
                snake.Add(p);
                p.Draw();
            }
        }

        /*методы движения и поворотав зависимости от направления */
        public Point GetHead() => snake.Last(); // этот метод насколько я понял возвращает последний элемент списка. 
        /* public Point GetHead() {return snake.Last();}  вроде это эквивалентно */

        public void Move()
        {
            head = GetNextPoint();
            snake.Add(head);
            tail = snake.First();
            snake.Remove(tail);
            tail.Clear();
            head.Draw();
            rotate = true;
        }

        public Point GetNextPoint()
        {
            Point p = GetHead();
            switch (direction)
            {
                case Direction.LEFT:
                    p.x -= step;
                    break;
                case Direction.RIGHT:
                    p.x += step;
                    break;
                case Direction.UP:
                    p.y -= step;
                    break;
                case Direction.DOWN:
                    p.y += step;
                    break;
            }
            return p;
        }

        public bool IsHit (Point p)
        {
            for (int i = snake.Count - 2; i > 0; i--)
            {
                if (snake[i] == p)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Eat (Point p)
        {
            head = GetNextPoint();
            if (head == p)
            {
                snake.Add(head);
                head.Draw();
                return true;
            }
            return false;
        }


            public void Rotation(ConsoleKey key)
        {
            if (rotate)
            {
                switch (direction)
                {
                    case Direction.LEFT: //TODO как это ? возможно просто перепутано местами. 
                    case Direction.RIGHT:
                        if (key == ConsoleKey.DownArrow)
                            direction = Direction.DOWN;
                        else if (key == ConsoleKey.UpArrow)
                            direction = Direction.UP;
                        break;
                    case Direction.UP:
                    case Direction.DOWN:
                        if (key == ConsoleKey.LeftArrow)
                            direction = Direction.LEFT;
                        else if (key == ConsoleKey.RightArrow)
                            direction = Direction.RIGHT;
                        break;
                }
                rotate = false;
            }
        }
    }
}
