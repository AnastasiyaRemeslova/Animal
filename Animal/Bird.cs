using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class Bird : Animal
    {
        private int positionX, positionY;
        private bool isPredator, isSwimming, isFlying;
        private int averageWeight;
        private int possibleStepsWithoutFood, currentStepsWithoutFood = 0;
        private int requiredPortionOfFood, currentPortionOfFood = 0;
        private int maxDepth;
        private int[] habitat= { };
        private int radiusOfSight;
        Random rand = new Random();

        public Bird (int positionX, int positionY, bool isPredator, bool isSwimming, bool isFlying, int averageWeight, int[] habitat)
        {
            PositionX = positionX;
            PositionY = positionY;
            IsPredator = isPredator;
            IsSwimming = isSwimming;
            IsFlying = isFlying;
            AverageWeight = averageWeight;

            if(isSwimming)
            {
                Array.Resize(ref habitat, 3);
                habitat[2] = 2;
                MaxDepth = rand.Next(1, 30);
            }
            RequiredPortionOfFood = AverageWeight / 10;
            PossibleStepsWithoutFood = rand.Next(5, 20);
            Habitat = habitat;
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

        public bool IsSwimming
        {
            protected set
            {
                isSwimming = value;
            }
            get
            {
                return (isSwimming);
            }
        }

        public bool IsFlying
        {
            protected set
            {
                isFlying = value;
            }
            get
            {
                return (isFlying);
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

        public int MaxDepth
        {
            protected set
            {
                maxDepth = value;
            }
            get
            {
                return (maxDepth);
            }
        }

        public int RequiredPortionOfFood
        {
            protected set
            {
                if (value > 0)
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

        private bool CheckCellForEat(Cell cell)
        {
            
            for (int i = 0; i < habitat.Length; i++)
            {
                if (cell.Type == habitat[i])
                {
                    if(cell.Type == 2)
                    {
                        WaterCell waterCell = (WaterCell)cell;
                        if(waterCell.Depth <= MaxDepth)
                        {
                            return true;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public override bool Eat()
        {
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX);
            List<Animal> animals = cell.Animals;
            int food = cell.Food;
            if (CheckCellForEat(cell))
            {
                if (IsPredator)
                {
                    foreach (Animal animal in animals)
                    {
                        if (animal != this)
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
            }
            currentStepsWithoutFood++;
            currentPortionOfFood = 0;
            if (currentStepsWithoutFood >= PossibleStepsWithoutFood)
            {
                cell.RemoveAnimal(this);
                Enviroment.RemoveAnimal(this);
                return false;
            }
            return false;
        }

        private bool CheckCell(Cell cell)
        {
            if (IsFlying && cell.Type != 3) return true;
            for (int i = 0; i < habitat.Length; i++)
            {
                if (cell.Type == habitat[i])
                {
                    return true;
                }
            }
            return false;
        }

        public override Cell FindCell()
        {
            
            return null;
        }

        public override bool Move(Cell cellForMove)
        {
            Random rand = new Random();
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX);
            if(CheckCell(cellForMove))
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
