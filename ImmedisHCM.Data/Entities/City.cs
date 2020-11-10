﻿using System;

namespace ImmedisHCM.Data.Entities
{
    public class City
    {
        public virtual Guid Id  { get; set; }
        public virtual string Name { get; set; }
        public virtual Country Country { get; set; }
    }
}