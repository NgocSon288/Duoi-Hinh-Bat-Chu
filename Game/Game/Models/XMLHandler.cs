using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Game.Models;
using Game.Assets;

namespace Game.Models
{
    public class XMLHandler
    {
        private XmlDocument doc;
         
        // khai bao cac node trong xml
        XmlElement RootCauDo;

        public XMLHandler()
        {
            doc = new XmlDocument();
            // Khai bao nut khai bao tai lieu dau trang
            XmlNode decNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            // Them node do vao trong tai lieu
            doc.AppendChild(decNode); 
        }

        #region Nghiệp vụ handler


        /// <summary>
        /// Y1 tưởng: Mới vào đăng nhập gọi hàm này truyền vào danh sách câu đố
        /// để nó khởi tạo các câu đố, mỗi lần tạo ra user thì gọi hàm này 
        /// </summary>
        /// <param name="cauDos"></param>
        public void Init(List<CauDo> cauDos)
        {
            // Khai báo các node của xml
            XmlElement cauDo, hinh, cauTraLoi, cauTraLoiVN;

            // Khai báo các thuộc tính xml
            XmlAttribute ID;

            doc = new XmlDocument();
            // Tao ra node có tên là RootCauDo
            RootCauDo = doc.CreateElement(Constant.RootCauDo);

            foreach (var cd in cauDos)
            {
                cauDo = CreateElementToXml(Constant.CauDo);

                ID = CreateAttributeToXml(Constant.ID, cd.ID.ToString());

                cauTraLoi = CreateElementToXmlWithValue(Constant.CauTraLoi, cd.CauTraLoi);

                hinh = CreateElementToXmlWithValue(Constant.Hinh, cd.Hinh);

                cauTraLoiVN = CreateElementToXmlWithValue(Constant.CauTraLoiVN, cd.CauTraLoiVN);
                
                // Add attribute
                cauDo.Attributes.Append(ID);

                // Add node
                cauDo.AppendChild(cauTraLoi);
                cauDo.AppendChild(hinh);
                cauDo.AppendChild(cauTraLoiVN);


                // Add node vào RootCauDo
                RootCauDo.AppendChild(cauDo);
            }

            doc.AppendChild(RootCauDo);

            doc.Save(GetPath(Constant.PathFileCauDoXML)); 

        }


        ///// <summary>
        ///// Lấy ra danh sách các câu đố
        ///// </summary>
        ///// <returns></returns>
        public List<CauDo> GetCauDoFormFile()
        {
            string cauTraLoi, hinh, cauTraLoiVN;
            int ID;

            List<CauDo> data = new List<CauDo>();

            // Load dữ liệu ra doc
            doc.Load(GetPath(Constant.PathFileCauDoXML));

            // Lấy ra các node có tagName là CauDo
            XmlNodeList nodeList = doc.GetElementsByTagName(Constant.CauDo);

            foreach (XmlNode node in nodeList)
            {
                ID = Convert.ToInt32(GetValueFormXmlNode(node, Constant.ID));

                cauTraLoi = GetInnerTextFormXmlNode(node, 0);

                hinh = GetInnerTextFormXmlNode(node, 1);

                cauTraLoiVN = GetInnerTextFormXmlNode(node, 2);

                data.Add(new CauDo(ID, cauTraLoi, hinh, cauTraLoiVN));
            }
            return data;
        }
        
        // Cần ta phương thức thêm, xóa, sửa


        #endregion

        #region Nghiệp vụ của node

        /// <summary>
        /// Tạo ra một node, thường dùng cho các node chứa các node khác
        /// </summary>
        /// <param name="proName"></param>
        /// <returns></returns>
        private XmlElement CreateElementToXml(string proName)
        {
            return doc.CreateElement(proName);
        }

        /// <summary>
        /// Tạo node và truyền gia trị vào node
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private XmlElement CreateElementToXmlWithValue(string proName, string value)
        {
            var element = doc.CreateElement(proName);
            element.InnerText = value;
            return element;
        }

        /// <summary>
        /// Tạo ra attribute và truyền giá trị vào attribute đó
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private XmlAttribute CreateAttributeToXml(string proName, string value)
        {
            var attribute = doc.CreateAttribute(proName);
            attribute.Value = value;
            return attribute;
        }

        /// <summary>
        /// Lấy ra giá trị nằm trong thẻ
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        private string GetValueFormXmlNode(XmlNode node, string attr)
        {
            return node.Attributes[attr].Value;
        }

        /// <summary>
        /// Lấy ra Text nằm giữa cặp thẻ
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetInnerTextFormXmlNode(XmlNode node, int index)
        {
            return node.ChildNodes[index].InnerText;
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

        #endregion

    }
}
