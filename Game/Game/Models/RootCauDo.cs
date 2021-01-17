using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Game.Models
{ 
    [XmlRoot(ElementName = "RootCauDo")]
    public class RootCauDo
    {
        private List<CauDo> cauDos;

        public List<CauDo> GetCauDos()
        {
            return cauDos;
        }

        public void SetCauDos(List<CauDo> value)
        {
            cauDos = value;
        }

        public RootCauDo()
        {

        }
    }
}
