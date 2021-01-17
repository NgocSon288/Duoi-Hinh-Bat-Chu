using System;
using System.Collections.Generic;
using Game.Assets;
using Game.Models;

namespace BTTH3_18521694.Models
{
    public class DBContext
    {
        public List<CauDo> CauDos { get; set; }
        public Player Player { get; set; }

        /// <summary>
        /// Các lần sau chỉ cần lấy dữ liệu ra, vì file đã tồn tại
        /// </summary>
        public DBContext()
        {
            CauDos = Constant.xml.GetCauDoFormFile();
            Player = Constant.json.ReadDataFromFile();
        }

        /// <summary>
        /// Áp dụng cho lần đầu tiên tạo user
        /// </summary>
        /// <param name="isFirstTime"></param>
        public DBContext(bool isFirstTime)
        {
            if (isFirstTime)
            {
                // Khởi tạo nếu là lần đầu tiên
                Constant.xml.Init(new List<CauDo>()
                {
                    new CauDo(1,"KINH DO", "i1.jfif", "Kinh độ"),
                    new CauDo(2,"XAU HO", "i2.jpg", "Xấu hổ"),
                    new CauDo(3,"BAO CAO", "i3.jpg", "Báo cáo"),
                    new CauDo(4,"BAO THUC", "i4.jpg", "Báo thức"),
                    new CauDo(5,"NOI GIAN", "i5.jpg", "Nội gián"),
                    new CauDo(6,"BAO LA", "i6.jpg", "Bao la"),
                    new CauDo(7,"NOI THAT", "i7.jpg", "Nội thất"),
                    new CauDo(8,"BAT MAT", "i8.jpg", "Bắt mắt"),
                    new CauDo(9,"BANH IN", "i9.jpg", "Bánh in"),
                    new CauDo(10,"BON CHON", "i10.jpg", "Bồn chồn")
                });
                Constant.json.WriteDataToFile(new Player("Nguyễn Phương", 1, 10));
                CauDos = Constant.xml.GetCauDoFormFile();
                Player = Constant.json.ReadDataFromFile();
            }
        }

        /// <summary>
        /// Cập nhật thông tin user đó
        /// </summary>
        public void UpdatePlayer()
        {
            Constant.json.WriteDataToFile(Player);
        }
         
    }
}
