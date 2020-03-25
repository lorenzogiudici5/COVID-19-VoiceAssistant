using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusFunction.Models
{
    public class ItalianDataCity : ItalianData
    {
        public long Fid { get; set; }

        public string CodIstatn { get; set; }

        public string Comune { get; set; }

        public NomePro Provincia { get; set; }

        public override string Name => this.Comune;
    }
}
