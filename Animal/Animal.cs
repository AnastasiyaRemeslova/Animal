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
        public abstract Cell FindCell();
        public abstract void Live();
    }

}
