using Game.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.Models
{
    public class JSONHandler
    {
        public JSONHandler()
        {

        }

        public void WriteDataToFile(Player data)
        {
            // Mo file ra
            using (StreamWriter file = File.CreateText(GetPath(Constant.PathFileUserJSON)))
            {
                JsonSerializer serializer = new JsonSerializer();
                // Ghi vao file, ghi de, nen du lieu cu mat di
                serializer.Serialize(file, data);
            }
        }

        public Player ReadDataFromFile()
        {
            // Mo file ra
            using (StreamReader file = File.OpenText(GetPath(Constant.PathFileUserJSON)))
            {
                JsonSerializer serializer = new JsonSerializer();
                // Doc du lieu tu file ra, va conver thanh List<Player>
                return (Player)serializer.Deserialize(file, typeof(Player));
            }
        }  



        /// <summary>
        /// Lấy đường dãn file tron hệ thống mobile
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns></returns>
        private string GetPath(string PathName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, PathName);
        }
    }
}
