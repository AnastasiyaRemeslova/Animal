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
        private bool isPredator, isFemale;
        private int averageWeight;
        private int[] habitat = { 2 };
        int speed;
        private int possibleStepsWithoutFood, currentStepsWithoutFood=0;
        private int requiredPortionOfFood, currentPortionOfFood=0;
        private int radiusOfSight;
        private int progeny=0;
        Random rand = new Random();
        public Fish(int positionX, int positionY, bool isPredator, int averageWeight, bool isFemale)
        {
            PositionX = positionX;
            PositionY = positionY;
            IsPredator = isPredator;
            AverageWeight = averageWeight;
            Speed = rand.Next(2, 5);

            RequiredPortionOfFood = AverageWeight / 10;
            PossibleStepsWithoutFood = rand.Next(5, 20);
            RadiusOfSight = rand.Next(2, 5);
            IsFemale = isFemale;
            if (IsFemale)
            {
                Progeny = rand.Next(2, 4);
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
            protected set   {   averageWeight = value;  }
            get {   return (averageWeight); }
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

        protected override bool CheckAnimalForEat(Animal animal)
        {
            for (int i = 0; i < animal.Habitat.Length; i++)
            {
                if (animal.Habitat[i] == 2)
                {
                    return true;
                }
            }
            return false;
        }


        protected override bool CheckCellForEat(Cell cell)
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

        private bool CheckCell(Cell cell)
        {
            if (cell != null)
            {
                if (cell.Type == habitat[0] && (Math.Abs(cell.PositionX - PositionX) + Math.Abs(cell.PositionY - PositionY)<=2))
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

        protected override bool CheckAnimalForPropagate(Animal animal)
        {
            if(animal is Fish)
            {
                Fish fish = (Fish)animal;

               if (fish.IsFemale != IsFemale && Math.Abs(fish.AverageWeight - AverageWeight) < 10 && fish != this)
                return true;
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
            if (CheckCellForEat(cell) && cell.Animals.Count<6)
            {
                foreach (Animal animal in animals)
                {
                    if (CheckAnimalForPropagate(animal))
                    {
                        if (animal.IsFemale)
                        {
                            progeny = animal.Progeny;
                        }
                        if(this.isFemale)
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
                            Fish child = new Fish(PositionX, PositionY, IsPredator, averageWeight, isFemale);
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

            while (!isPropagated && i>0 && isAte)
            {
                cell = FindCellForPropagate();
                Move(cell);
                isPropagated = Propagate();
                i--;
            }
        }
    }
}
 