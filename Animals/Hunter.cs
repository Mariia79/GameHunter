﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animals
{
    public class Hunter : Rabbit
    {

        public Hunter(Size fieldSize) : base(fieldSize)
        {
            IsFoodToWolfs = true;

        }

        public override void Move()
        {

        }
    }
}
