using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Game.Models
{
    public class CauDo
    {
        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [XmlElement(ElementName = "CauTraLoi")]
        public string CauTraLoi { get; set; }

        [XmlElement(ElementName = "Hinh")]
        public string Hinh { get; set; }

        [XmlElement(ElementName = "CauTraLoiVN")]
        public string CauTraLoiVN { get; set; }

        public CauDo()
        {

        }

        public CauDo(int iD, string cauTraLoi, string hinh, string cauTraLoiVN)
        {
            ID = iD;
            CauTraLoi = cauTraLoi;
            Hinh = hinh;
            CauTraLoiVN = cauTraLoiVN;
        }
    }
}
