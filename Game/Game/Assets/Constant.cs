using System;
using Game.Models;

namespace Game.Assets
{
    public static class Constant
    {
        public static string RootCauDo = "RootCauDo";
        public static string ID = "ID";
        public static string CauDo = "CauDo";
        public static string Hinh = "Hinh";
        public static string CauTraLoi = "CauTraLoi";
        public static string CauTraLoiVN = "CauTraLoiVN";
        public static string Rubi = "Rubi";
        public static string Level = "Level";

        public static string PathFileCauDoXML = "PathFileCauDoXML.xml";
        public static string PathFileUserJSON = "PathFileUserJSON.json";

        public static XMLHandler xml = new XMLHandler();
        public static JSONHandler json = new JSONHandler();

    }
}
