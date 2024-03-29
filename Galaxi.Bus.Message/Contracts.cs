﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Bus.Message
{
    public record TickedCreated
    {
        public int FunctionId { get; init; }
        public int NumSeat { get; init; }
        public string Email { get; init; }
    }

    public record MovieDetails
    {
        public int FunctionId { get; init; }
        public int NumSeat { get; init; }
        public string Email { get; init; }
    }
}
