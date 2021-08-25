using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CD.API.Extensions
{
    public class AppSettings
    {
        public string ChaveToken { get; set; }
        public int TempoExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
