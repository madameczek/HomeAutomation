using Actors.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actors.Services
{
    public class LocalQueue
    {
        private LocalContext context;

        public LocalQueue(LocalContext context)
        {
            this.context = context;
        }
    }
}
