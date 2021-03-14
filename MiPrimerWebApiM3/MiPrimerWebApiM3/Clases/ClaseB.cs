using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.Controllers;

namespace MiPrimerWebApiM3
{
    public interface IClaseB 
    {
        void HacerAlgo();
    }
    public class ClaseB : IClaseB
    {
        private readonly ILogger<ClaseB> logger;

        public ClaseB(ILogger<ClaseB> logger)
        {
            this.logger = logger;
        }
        public void HacerAlgo()
        {
            logger.LogInformation("Ejecutando el metodo hacer algo");
        }
    }

    public class ClaseB2 : IClaseB
    {
        public void HacerAlgo()
        {        
        }
    }
}
