﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class Joists
    {
        private double _joistSpacing;

        public Joists()
        {
            _joistSpacing = 16;
        }

        public double getSpacing()
        {
            return _joistSpacing;
        }
    }
}
