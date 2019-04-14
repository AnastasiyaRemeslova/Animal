using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    public abstract class Animal
    {

        public abstract bool IsPredator { get; protected set; }
        public abstract int AverageWeight { get; protected set; }
        public abstract int[] Habitat { get; protected set; }
        public abstract int PositionX { get; protected set; }
        public abstract int PositionY { get; protected set; }
        public abstract int RadiusOfSight { get; protected set; }

        public abstract bool Move(Cell cell);
        public abstract bool Eat();
        //public abstract Cell FindCell();
        public abstract void Live();
        protected abstract bool CheckCellForEat(Cell cell);

        public Cell FindCellForEat()
        {
            Random rand = new Random();
            Cell[] rangeCells = Enviroment.GetRangeCells(RadiusOfSight, PositionY, PositionX);
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX), nextCell;
            int minX = PositionX, minY = PositionY, maxX = PositionX, maxY = PositionY;
            for (int i = 0; i < rangeCells.Length; i++)
            {
                if (rangeCells[i] != null && CheckCellForEat(rangeCells[i]))
                {
                    List<Animal> animals = rangeCells[i].Animals;
                    for (int j = 0; j < animals.Count; j++)
                    {
                        Animal animal = animals.ElementAt(j);
                        if (IsPredator)
                        {
                            if (animal.AverageWeight <= AverageWeight && animal != this)
                            {
                                if (i < 9)
                                {
                                    cell = rangeCells[i];
                                    return cell;
                                }
                                else
                                {
                                    int min = RadiusOfSight * 2;
                                    for (int k = 0; k < 9; k++)
                                    {
                                        if (rangeCells[k] != null)
                                        {
                                            nextCell = rangeCells[k];
                                            if (((Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY)) < min) && nextCell is WaterCell)
                                            {
                                                min = Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY);
                                                minX = nextCell.PositionX;
                                                minY = nextCell.PositionY;
                                            }
                                        }
                                    }

                                }
                                cell = Enviroment.GetCellByCoords(minY, minX);

                            }
                            if (minX == PositionX && minY == PositionY)
                            {
                                int next = rand.Next(9);
                                if (rangeCells[next] != null)
                                {
                                    cell = rangeCells[next];
                                }
                            }
                        }
                        else
                        {
                            if (animal.AverageWeight > AverageWeight && animal != this)
                            {
                                int max = 0;
                                for (int k = 0; k < 9; k++)
                                {
                                    if (rangeCells[k] != null)
                                    {
                                        nextCell = rangeCells[k];
                                        if (nextCell.Food > 0)
                                        {
                                            if ((Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY)) > max)
                                            {
                                                max = Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY);
                                                maxX = nextCell.PositionX;
                                                maxY = nextCell.PositionY;
                                            }
                                        }
                                    }
                                }
                                cell = Enviroment.GetCellByCoords(maxY, maxX);
                            }
                            if (maxX == PositionX && maxY == PositionY)
                            {
                                int next = rand.Next(9);
                                if (rangeCells[next] != null)
                                {
                                    cell = rangeCells[next];
                                }
                            }
                        }
                    }
                }
            }
            return cell;
        }
    }

}
