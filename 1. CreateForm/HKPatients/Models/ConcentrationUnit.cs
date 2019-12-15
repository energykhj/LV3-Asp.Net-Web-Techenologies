﻿using System;
using System.Collections.Generic;

namespace HKPatients.Models
{
    public partial class ConcentrationUnit
    {
        public ConcentrationUnit()
        {
            Medication = new HashSet<Medication>();
        }

        public string ConcentrationCode { get; set; }

        public ICollection<Medication> Medication { get; set; }
    }
}
