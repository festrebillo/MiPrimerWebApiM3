using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3
{
    public class ClaseA
    {
        private readonly ClaseB claseB;
        public ClaseA(ClaseB claseB)
        {
            this.claseB = claseB;   
        }
        public void RealizarAccion()
        {
            claseB.HacerAlgo();
        }
    }
}
