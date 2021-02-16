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
            //head = GetNExtPoint();
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
    }
}
