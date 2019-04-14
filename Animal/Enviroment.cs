using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Animal
{
    class Enviroment
    {
        private static Enviroment instance;
        private static Cell[,] cell;
        private static List<Animal> fishes = new List<Animal>();
        private static List<Animal> birds = new List<Animal>();
        private static List<Animal> mammals = new List<Animal>();
        private static int n, m, numberOfFishes, numberOfBirds, numberOfMammals;
        public static int N
        {
            get { return n; }
            private set { n = value; }
        }
        public static int M
        {
            get { return m; }
            private set { m = value; }
        }
        public static int NumberOfFishes
        {
            get { return fishes.Count; }
            private set { numberOfFishes = value; }
        }
        public static int NumberOfBirds
        {
            get { return birds.Count; }
            private set { numberOfBirds = value; }
        }
        public static int NumberOfMammals
        {
            get { return mammals.Count; }
            private set { numberOfMammals = value; }
        }
        private Enviroment() { }
        private Enviroment(int n, int m, int numberOfFishes, int numberOfBirds, int numberOfMammals)
        {
            N = n;
            M = m;
            NumberOfFishes = numberOfFishes;
            NumberOfBirds = numberOfFishes;
            NumberOfMammals = numberOfMammals;
            cell = CreateCell(n, m);
            fishes = CreateFishes(numberOfFishes);
            birds = CreateBirds(numberOfBirds);
            mammals = CreateMammals(numberOfMammals);
        }

        public static Enviroment getInstance(int n, int m, int numberOfFishes, int numberOfBirds, int numberOfMammals)
        {
            if (instance == null)
            {
                instance = new Enviroment(n,m, numberOfFishes, numberOfBirds, numberOfMammals);
            }
            return instance;
        }
        

        public static Cell GetCellByCoords(int y, int x)
        {
            if ((x >= 0 && x <= M+1) && (y >= 0 && y <= N+1))
            {
                return cell[y, x];
            }
            return null;

        }

        private static bool CheckCellByCoords(int y, int x)
        {
            if( (x > 0 && x <M) && (y > 0 && y < N))
            {
                Cell cell = GetCellByCoords(y, x);
                return true;

            }
            return false;
        }

        public static Cell[] GetRangeCells(int radius, int y, int x)
        {
            Cell[] cells = new Cell[(radius * 2 + 1)* (radius * 2 + 1)];
            cells[0] = GetCellByCoords(y, x);
            int n = 1, j = 1;
            int newX = x, newY = y;
            while (n != (radius * 2 + 1))
            {
                //left
                for(int i=1; i<=n; i++)
                {
                    newX -= 1;
                    if (CheckCellByCoords(newY, newX))
                    {
                        cells[j] = GetCellByCoords(newY, newX);
                    }
                    j++;
                }
                //down
                for (int i = 1; i <= n; i++)
                {
                    newY += 1;
                    if (CheckCellByCoords(newY, newX))
                    {
                        cells[j] = GetCellByCoords(newY, newX);
                    }
                    j++;
                }
                n++;
                //right
                for (int i = 1; i <= n; i++)
                {
                    newX += 1;
                    if (CheckCellByCoords(newY, newX))
                    {
                        cells[j] = GetCellByCoords(newY, newX);
                    }
                    j++;
                }
                //up
                for (int i = 1; i <= n; i++)
                {
                    newY -= 1;
                    if (CheckCellByCoords(newY, newX))
                    {
                        cells[j] = GetCellByCoords(newY, newX);
                    }
                    j++;
                }
                n++;
            }

            for (int i = 1; i < n; i++)
            {
                newX -= 1;
                if (CheckCellByCoords(newY, newX))
                {
                    cells[j] = GetCellByCoords(newY, newX);
                }
                j++;
            }


            return cells;
        }

        public static List<Animal> GetAnimals(int type)
        {
            switch (type)
            {
                case 0:
                    return fishes;
                case 1:
                    return birds;
                case 2:
                    return mammals;
                default:
                    return null;
            }
        }


        public static void RemoveAnimal(Animal animal)
        {
            if(animal is Fish)
                fishes.Remove(animal);

            if(animal is Bird)
                birds.Remove(animal);

            if (animal is Mammal)
                mammals.Remove(animal);
        }

        public void Process(Form1 form)
        {
            while(true)
            {
                Thread.Sleep(200);
                for (int i = 0; i < Enviroment.NumberOfFishes; i++)
                {
                    fishes.ElementAt(i).Live();
                    form.Draw();
                }
               
                for (int i = 0; i < Enviroment.NumberOfBirds; i++)
                {
                    birds.ElementAt(i).Live();
                    form.Draw();
                }
                
               for (int i = 0; i < Enviroment.NumberOfMammals; i++)
               {
                   mammals.ElementAt(i).Live();
                   form.Draw();

               }
               
                if (NumberOfFishes == 0 && NumberOfBirds == 0 && NumberOfMammals == 0)
                {
                    form.Draw(); return;
                }
            }
        }

        public static Cell[,] CreateCell(int n, int m)
        {
            Cell[,] cell = new Cell[n + 2, m + 2];
            int typeOfCell, food, depth;
            Random rand = new Random();
            for (int i = 0; i < n + 2; i++)
            {
                for (int j = 0; j < m + 2; j++)
                {
                    if (i == 0 || j == 0 || i == n + 1 || j == m + 1)
                    {
                        cell[i, j] = new BorderCell(i, j);
                    }
                    else
                    {
                        typeOfCell = rand.Next(0, 3);
                        switch (typeOfCell)
                        {
                            case 0:
                                food = rand.Next(0, 50);
                                cell[i, j] = new LandCell(i, j, food);
                                break;
                            case 1:
                                food = rand.Next(0, 50);
                                cell[i, j] = new ForestCell(i, j, food);
                                break;
                            case 2:
                                food = rand.Next(0, 50);
                                depth = rand.Next(1, 100);
                                cell[i, j] = new WaterCell(i, j, food, depth);
                                break;
                        }
                    }

                }
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (cell[i, j].Type != cell[i + 1, j].Type && cell[i, j].Type != cell[i, j + 1].Type && cell[i, j].Type != cell[i - 1, j].Type && cell[i, j].Type != cell[i, j - 1].Type)
                    {
                        typeOfCell = cell[i, j].Type;
                        if (cell[i + 1, j].Type != 3)
                        {
                            typeOfCell = cell[i + 1, j].Type;
                        }
                        else if (cell[i - 1, j].Type != 3)
                        {
                            typeOfCell = cell[i - 1, j].Type;
                        }

                        switch (typeOfCell)
                        {
                            case 0:
                                food = rand.Next(0, 50);
                                cell[i, j] = new LandCell(i, j, food);
                                break;
                            case 1:
                                food = rand.Next(0, 50);
                                cell[i, j] = new ForestCell(i, j, food);
                                break;
                            case 2:
                                food = rand.Next(0, 50);
                                depth = rand.Next(1, 100);
                                cell[i, j] = new WaterCell(i, j, food, depth);
                                break;
                        }
                    }

                }

            }
            return cell;
        }

 
        public static List<Animal> CreateFishes(int numberOfFishes)
        {
            Random rand = new Random();
            int positionX, positionY;
            int averageWeight;
            bool isPredator;
            for (int i = 0; i < numberOfFishes; i++)
            {
                do
                {
                    positionX = rand.Next(1, cell.GetLength(1) - 1);
                    positionY = rand.Next(1, cell.GetLength(0) - 1);
                } while (cell[positionY, positionX].Type != 2);
                averageWeight = rand.Next(1,100);
                isPredator = (rand.Next(0, 2) == 0 ? true : false);
                fishes.Add(new Fish(positionX, positionY, isPredator, averageWeight));
                cell[positionY, positionX].AddAnimal(fishes.Last());
            }
            return fishes;
        }

        public static List<Animal> CreateBirds(int numberOfBirds)
        {
            Random rand = new Random();
            int positionX, positionY, averageWeight;
            int[] habitat = {};
            bool isPredator, isSwimming, isFlying;
            for (int i = 0; i < numberOfBirds; i++)
            {
                Array.Resize(ref habitat, 2);
                habitat[0] = rand.Next(0, 2);
                habitat[1] = rand.Next(0, 2);
                if(habitat[1] == habitat[0])
                {
                    Array.Resize(ref habitat, 1);
                }
                do
                {
                    positionX = rand.Next(1, cell.GetLength(1) - 1);
                    positionY = rand.Next(1, cell.GetLength(0) - 1);
                } while (cell[positionY, positionX].Type != habitat[0]);
                averageWeight = rand.Next(1, 100);
                isPredator = (rand.Next(0, 2) == 0 ? true : false);
                isSwimming = (rand.Next(0, 2) == 0 ? true : false);
                isFlying = (rand.Next(0, 2) == 0 ? true : false);
                birds.Add(new Bird(positionX, positionY, isPredator, isSwimming, isFlying, averageWeight, habitat));
                cell[positionY, positionX].AddAnimal(birds.Last());
                Console.WriteLine("Bird " + i + ": " + positionX + " " + positionY + " " + isPredator + " " + isSwimming + " " + isFlying + " " + averageWeight);
            }
            return birds;
        }

        public static List<Animal> CreateMammals(int numberOfMammals)
        {
            Random rand = new Random();
            int positionX, positionY, averageWeight;
            int[] habitat = { };
            bool isPredator, isSwimming;
            for (int i = 0; i < numberOfMammals; i++)
            {
                Array.Resize(ref habitat, 2);
                habitat[0] = rand.Next(0, 2);
                habitat[1] = rand.Next(0, 2);
                if (habitat[1] == habitat[0])
                {
                    Array.Resize(ref habitat, 1);
                }
                do
                {
                    positionX = rand.Next(1, cell.GetLength(1) - 1);
                    positionY = rand.Next(1, cell.GetLength(0) - 1);
                } while (cell[positionY, positionX].Type != habitat[0]);
                averageWeight = rand.Next(1, 100);
                isPredator = (rand.Next(0, 2) == 0 ? true : false);
                isSwimming = (rand.Next(0, 2) == 0 ? true : false);
                mammals.Add(new Mammal(positionX, positionY, isPredator, isSwimming, averageWeight, habitat));
                cell[positionY, positionX].AddAnimal(birds.Last());
                Console.WriteLine("Mammal " + i + ": " + positionX + " " + positionY + " " + isPredator + " " + isSwimming + " " + averageWeight);
            }
            return mammals;
        }
    }
}
