using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class BorderCell : Cell
    {
        private int positionX, positionY;
        private int type = 3;
        private int food = 0;

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

            }
            get
            {
                return 0;
            }
        }


        public BorderCell(int positionY, int positionX)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}
