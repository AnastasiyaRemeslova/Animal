using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    abstract public class Cell
    {
        private List<Animal> animals = new List<Animal>();

        public abstract int PositionX { get; set; }
        public abstract int PositionY { get; set; }
        public abstract int Type { get; set; }
        public abstract int Food { get; set; }
        public List<Animal> Animals
        {
            get
            {
                return animals;
            }
            set
            { }
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void RemoveAnimal(Animal animal)
        {
            animals.Remove(animal);
        }

        public void AddFood(int food)
        {
            Food += food;
        }

    }
}
