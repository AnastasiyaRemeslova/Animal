using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class WaterCell : Cell
    {
        private int positionX, positionY;
        private int type = 2;
        private int food;
        private int depth;
        public override int PositionX
        {
            set
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
            set
            {
                positionY = value;
            }
            get
            {
                return (positionY);
            }
        }
        public override int Type
        {
            set { }
            get
            {
                return (type);
            }
        }

        public override int Food 
        {
            set
            {
                food = value;
            }
            get
            {
                return food;
            }
        }

        public int Depth
        {
            set
            {
                depth = value;
            }
            get
            {
                return (depth);
            }
        }

        public WaterCell(int positionY, int positionX, int food, int depth)
        {
            PositionX = positionX;
            PositionY = positionY;
            Food = food;
            Depth = depth;
        }
    }
}
