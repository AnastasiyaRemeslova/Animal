using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class Mammal : Animal
    {
        private int positionX, positionY;
        private bool isPredator, isSwimming, isFemale;
        private int averageWeight;
        private int speed;

        private int possibleStepsWithoutFood, currentStepsWithoutFood = 0;
        private int requiredPortionOfFood, currentPortionOfFood = 0;
        private int maxDepth;
        private int[] habitat = { };
        private int radiusOfSight;
        private int progeny;

        Random rand = new Random();

        public Mammal(int positionX, int positionY, bool isPredator, bool isSwimming, int averageWeight, int[] habitat, bool isFemale)
        {
            PositionX = positionX;
            PositionY = positionY;
            IsPredator = isPredator;
            IsSwimming = isSwimming;
            AverageWeight = averageWeight;
            Speed = rand.Next(2, 5);

            if (isSwimming)
            {
                Array.Resize(ref habitat, habitat.Length + 1);
                habitat[habitat.Length - 1] = 2;
                MaxDepth = rand.Next(1, 30);
            }
            RequiredPortionOfFood = AverageWeight / 10;
            PossibleStepsWithoutFood = rand.Next(5, 20);
            Habitat = habitat;
            RadiusOfSight = rand.Next(1, 5);
            IsFemale = isFemale;
            if (IsFemale)
            {
                Progeny = rand.Next(1, 3);
            }
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

        public override bool IsFemale
        {
            protected set
            {
                isFemale = value;
            }
            get
            {
                return (isFemale);
            }
        }

        public override int Progeny
        {
            protected set
            {
                progeny = value;
            }
            get
            {
                return (progeny);
            }
        }

        public override int Speed
        {
            protected set
            {
                speed = value;
            }
            get
            {
                return (speed);
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

        protected override bool CheckCellForEat(Cell cell)
        {

            for (int i = 0; i < habitat.Length; i++)
            {
                if (cell.Type == habitat[i])
                {
                    if (cell.Type == 2)
                    {
                        WaterCell waterCell = (WaterCell)cell;
                        if (waterCell.Depth <= MaxDepth)
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
                            if(animal is Bird)
                            {
                                Bird bird = (Bird)animal;
                                if (bird.IsFlying) break;
                            }
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
            for (int i = 0; i < habitat.Length; i++)
            {
                if (cell.Type == habitat[i] && (Math.Abs(cell.PositionX - PositionX) + Math.Abs(cell.PositionY - PositionY) <= 2))
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
                currentStepsWithoutFood++;
                PositionX = cellForMove.PositionX;
                PositionY = cellForMove.PositionY;
                cell.RemoveAnimal(this);
                cellForMove.AddAnimal(this);
                return true;
            }
            return false;
        }

        protected override bool CheckAnimalForEat(Animal animal)
        {
            for (int i = 0; i < animal.Habitat.Length; i++)
            {
                for (int j = 0; j < Habitat.Length; j++)
                {
                    if (animal.Habitat[i] == Habitat[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override bool CheckAnimalForPropagate(Animal animal)
        {
            if (animal is Mammal)
            {
                Mammal mammal = (Mammal)animal;
                int k = 0;
                for (int i = 0; i < mammal.Habitat.Length; i++)
                {
                    for (int j = 0; j < Habitat.Length; j++)
                    {
                        if (mammal.Habitat[i] == Habitat[j])
                        {
                            k++;
                        }
                    }
                }
                if (k > 0 && mammal.IsFemale != IsFemale && mammal.IsPredator == IsPredator && mammal.IsSwimming == IsSwimming && Math.Abs(mammal.AverageWeight - AverageWeight) < 50 && mammal != this)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Propagate()
        {
            Cell cell = Enviroment.GetCellByCoords(PositionY, PositionX);
            List<Animal> animals = cell.Animals;
            int progeny = 0;
            Random rand = new Random();
            int averageWeight;
            bool isFemale;
            if (CheckCellForEat(cell) && cell.Animals.Count < 10)
            {
                foreach (Animal animal in animals)
                {
                    if (CheckAnimalForPropagate(animal))
                    {
                        if (animal.IsFemale)
                        {
                            progeny = animal.Progeny;
                        }
                        if (this.isFemale)
                        {
                            progeny = Progeny;
                        }
                        for (int i = 0; i < progeny; i++)
                        {
                            if (animal.AverageWeight >= AverageWeight)
                            {
                                averageWeight = rand.Next(AverageWeight, animal.AverageWeight);
                            }
                            else
                            {
                                averageWeight = rand.Next(animal.AverageWeight, AverageWeight);
                            }
                            isFemale = (rand.Next(0, 2) == 0 ? true : false);
                            Mammal child = new Mammal(PositionX, PositionY, IsPredator, IsSwimming, averageWeight, Habitat, isFemale);
                            cell.AddAnimal(child);
                            Enviroment.AddAnimal(child);
                        }
                        return true;
                    }
                }

            }
            return false;
        }

        public override void Live()
        {
            Cell cell;
            bool isAte = false, isPropagated = false;
            int i = Speed;
            while (!isAte && i > 0)
            {
                cell = FindCellForEat();
                Move(cell);
                isAte = Eat();
                i--;
            }

            while (!isPropagated && i > 0 && isAte)
            {
                cell = FindCellForPropagate();
                Move(cell);
                isPropagated = Propagate();
                i--;
            }
        }
    }
}