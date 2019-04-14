using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class Fish : Animal
    {
        private int positionX, positionY;
        private bool isPredator;
        private int averageWeight;
        private int[] habitat = { 2 };

        private int possibleStepsWithoutFood, currentStepsWithoutFood=0;
        private int requiredPortionOfFood, currentPortionOfFood=0;
        private int radiusOfSight;
        Random rand = new Random();
        public Fish(int positionX, int positionY, bool isPredator, int averageWeight)
        {
            PositionX = positionX;
            PositionY = positionY;
            IsPredator = isPredator;
            AverageWeight = averageWeight;

            RequiredPortionOfFood = AverageWeight / 10;
            PossibleStepsWithoutFood = rand.Next(5, 20);
            RadiusOfSight = rand.Next(1, 5);
        }

        public override int PositionX
        {
            protected set
            {
                positionX = value;
            }
            get
            {
                return (positionX);
            }
        }

        public override int PositionY
        {
            protected set
            {
                positionY = value;
            }
            get
            {
                return (positionY);
            }

        }

        public override bool IsPredator
        {
            protected set
            {
                isPredator = value;
            }
            get
            {
                return (isPredator);
            }
        }

        public override int AverageWeight
        {
            protected set
            {
                averageWeight = value;
            }
            get
            {
                return (averageWeight);
            }
        }

        public override int[] Habitat
        {
            protected set
            {
                habitat = value;
            }
            get
            {
                return (habitat);
            }
        }

        public int PossibleStepsWithoutFood
        {
            protected set
            {
                possibleStepsWithoutFood = value;
            }
            get
            {
                return (possibleStepsWithoutFood);
            }
        }

        public int RequiredPortionOfFood
        {
            protected set
            {
                if(value>0)
                requiredPortionOfFood = value;
                else
                requiredPortionOfFood = 1;
            }
            get
            {
                return (requiredPortionOfFood);
            }
        }

        public override int RadiusOfSight
        {
            protected set
            {
                radiusOfSight = value;
            }
            get
            {
                return (radiusOfSight);
            }
        }


        public override bool Eat()
        {
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX);
            if (cell != null)
            {
                List<Animal> animals = cell.Animals;
                int food = cell.Food;
                if (IsPredator)
                {
                    foreach (Animal animal in animals)
                    {
                        if (animal != this && (animal is Fish))
                        {
                            if (averageWeight > animal.AverageWeight)
                            {
                                cell.RemoveAnimal(animal);
                                Enviroment.RemoveAnimal(animal);
                                currentPortionOfFood += AverageWeight;
                            }
                            if (currentPortionOfFood >= RequiredPortionOfFood)
                            {
                                currentPortionOfFood = 0;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (food >= RequiredPortionOfFood)
                    {
                        food -= RequiredPortionOfFood;
                        Enviroment.GetCellByCoords(PositionY, PositionX).Food = food;
                        return true;
                    }

                }
                currentStepsWithoutFood++;
                currentPortionOfFood = 0;
                if (currentStepsWithoutFood >= PossibleStepsWithoutFood)
                {
                    cell.RemoveAnimal(this);
                    Enviroment.RemoveAnimal(this);
                    return false;
                }
            }
            return false;
        }

        public override Cell FindCell()
        {
            Cell[] rangeCells = Enviroment.GetRangeCells(RadiusOfSight, PositionY, PositionX);
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX), nextCell;
            int minX = PositionX, minY = PositionY;
            for (int i = 0; i < rangeCells.Length; i++)
            {
                if (rangeCells[i] != null)
                {
                    List<Animal> animals = rangeCells[i].Animals;
                    for (int j = 0; j < animals.Count; j++)
                    {
                        Animal animal = animals.ElementAt(j);
                        if (isPredator)
                        {
                            if (animal is Fish && animal.AverageWeight <= AverageWeight && animal != this)
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
                                        nextCell = rangeCells[k];
                                        if ((Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY)) < min)
                                        {
                                            min = Math.Abs(rangeCells[i].PositionX - nextCell.PositionX) + Math.Abs(rangeCells[i].PositionY - nextCell.PositionY);
                                            minX = nextCell.PositionX;
                                            minY = nextCell.PositionY;
                                        }
                                    }
                                    
                                }
                                cell = Enviroment.GetCellByCoords(minY, minX);

                            }
                        }
                    }
                }
            }
            return cell;
        }

        private bool CheckCell(Cell cell)
        {
            if (cell != null)
            {
                if (cell.Type == habitat[0])
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Move(Cell cellForMove)
        {
            Random rand = new Random();
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX);
            if (CheckCell(cellForMove))
            {
                PositionX = cellForMove.PositionX;
                PositionY = cellForMove.PositionY;
                cell.RemoveAnimal(this);
                cellForMove.AddAnimal(this);
                return true;
            }
            return false;
        }

        public override void Live()
        {
            Cell cell = FindCell();
            Move(cell);
            Eat();
        }
    }
}
 