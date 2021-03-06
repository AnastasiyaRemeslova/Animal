﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class ForestCell : Cell
    {
        private int positionX, positionY;
        private int type =1;
        private int food;

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

        public ForestCell(int positionY, int positionX, int food)
        {
            PositionX = positionX;
            PositionY = positionY;
            Food = food;
        }
    }
}
