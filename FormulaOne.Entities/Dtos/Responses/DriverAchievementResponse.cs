﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Entities.Dtos.Responses
{
    public class DriverAchievementResponse
    {
        public Guid DriverId { get; set; }
        public int WorldChaimpionship { get; set; }
        public int FastestLab { get; set; }
        public int PolePosition { get; set; }
        public int Wins { get; set; }
    }
}
