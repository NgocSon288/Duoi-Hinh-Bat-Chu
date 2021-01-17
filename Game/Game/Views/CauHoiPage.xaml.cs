using BTTH3_18521694.Models;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CauHoiPage : ContentPage
    {
        public CauHoiPage()
        {
            InitializeComponent();
            Load();
            LoadKyTu();
            LoadKetQua();

        }
        #region Khai báo

        DBContext db;
        CauDo cauDo;
        Player player;
        int level = 1;  // load level từ JSON  
        string ketQua = "";
        Button[] lsButtonKetQua;
        Button[] lsButtonKyTu;

        #endregion


        #region Xữ lý game

        void Load()
        {
            db = new DBContext();
            // Load thông tin user, level, ruby từ JSON
            player = db.Player;
            level = player.Level; 
            lblRuby.Text = player.Ruby.ToString();

            // cần load ra level từ user để lấy ra câu hỏi tương ứng
            cauDo = db.CauDos.SingleOrDefault(cd => cd.ID == level);
            if(cauDo == null)
            {
                Navigation.PushAsync(new MainPage(true));
            }

            ketQua = cauDo.CauTraLoi;

            // Để cho View dùng Binding hình ảnh, ...
            BindingContext = cauDo;

            lsButtonKetQua = new Button[ketQua.Count() - 1];
            lsButtonKyTu = new Button[ketQua.Count() - 1];
        }

        /// <summary>
        /// Tìm vị trí trống nằm trong lsButtonKetQua để khi nhấn vào button kytu thì nó nhãy vào
        /// </summary>
        int TimViTri()
        {
            int i = 0;
            foreach (var item in lsButtonKetQua)
            {
                if (item.Text == "")
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        bool isFullKyTu()
        {
            foreach (var item in lsButtonKetQua)
            {
                if (item.Text == "")
                    return false;
            }
            return true;
        }

        bool XuLyGame()
        {
            string result = "";
            foreach (var item in lsButtonKetQua)
            {
                result += item.Text;
            }

            return result == ketQua.Remove(ketQua.IndexOf(' '), 1);
        }

        int IndexFirstNotMatching()
        {
            string temp = ketQua.Remove(ketQua.IndexOf(' '),1);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].ToString() != lsButtonKetQua[i].Text)
                    return i;
            }

            return -1;
        }

        private void Wingame()
        {
            player.Ruby += 10;
            player.Level++;
            db.UpdatePlayer();
            Navigation.PushAsync(new KetQuaPage(cauDo.CauTraLoiVN));
        }

        void EnableKyTu(char s)
        {
            foreach(var item in stackKyTu1.Children)
            {
                var btn = item as Button;
                if (btn == null)
                    break;
                if(btn.Text == s.ToString())
                {
                    btn.Opacity = 0;
                    return;
                }
            }
            foreach (var item in stackKyTu2.Children)
            {
                var btn = item as Button;
                if (btn == null)
                    return;
                if (btn.Text == s.ToString())
                {
                    btn.Opacity = 0;
                    return;
                }
            }
        }

        #endregion

        #region Test Load button kết quả

        void LoadKetQua()
        {
            var ls = ketQua.Split(' ');
            int dem = 0;

            for (int i = 0; i < ls.Length; i++)
            {
                for (int j = 0; j < ls[i].Length; j++)
                {
                    Button btn = new Button()
                    {
                        Text = ""
                    };

                    btn.Clicked += Btn_Clicked;

                    if (i == 0)
                    {
                        stackKetQua1.Children.Add(btn);
                    }
                    else
                    {
                        stackKetQua2.Children.Add(btn);
                    }
                    lsButtonKetQua[dem++] = btn;
                }
            }
        }


        #endregion

        #region Test Load button ky tu
        void LoadKyTu()
        {
            string KetQuaTemp = RemoveSpace(ketQua);
            List<char> kq = RandomString(KetQuaTemp, 18);

            foreach (var item in kq)
            {
                Button btn = new Button()
                {
                    Text = item.ToString()
                };

                btn.Clicked += Btn_Clicked1;

                if (stackKyTu1.Children.Count <= 8)
                {
                    stackKyTu1.Children.Add(btn);
                }
                else
                {
                    stackKyTu2.Children.Add(btn);
                }
            }

        }

        string RemoveSpace(string s)
        {
            while (s.IndexOf(" ") > 0)
            {
                s = s.Remove(s.IndexOf(" "), 1);
            }
            return s;
        }

        List<char> RandomString(string s, int length)
        {
            Random rand = new Random();

            while (!(s.Length == 18))
            {
                s = Reverse(s.Insert(rand.Next(0, s.Length), ((char)rand.Next(65, 91)).ToString()), 2);
            }

            return new List<char>(s.ToCharArray());
        }

        string Reverse(string s, int count)
        {
            Random rand = new Random();
            while (count >= 0)
            {
                int index = rand.Next(0, s.Length);
                char temp = s[index];
                s = s.Remove(index, 1);
                s = s.Insert(s.Length, temp.ToString());

                count--;
            }
            return s;
        }

        #endregion

        #region Event
        /// <summary>
        /// Nhấn các ký tự kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "")
                return;
            btn.Text = "";

            int index = lsButtonKetQua.IndexOf(btn);

            lsButtonKyTu[index].Opacity = 1;
            lsButtonKyTu[index] = new Button();

        }

        /// <summary>
        /// Nhấn các ký tự
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Clicked1(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = TimViTri();

            if (index < 0 || btn.Opacity == 0)
            {
                return;
            }

            lsButtonKyTu[index] = btn;
            btn.Opacity = 0;

            lsButtonKetQua[index].Text = btn.Text;

            if (isFullKyTu())
            {
                if (!XuLyGame())
                {
                    DisplayAlert("Thông báo!", "Kết quả không đúng!", "OK");
                }
                else
                {
                    Wingame();
                }

                return;
            }

        }

       

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            if(player.Ruby < 5)
            {
                await DisplayAlert("Thông báo!", "Bạn không đủ Ruby để nhận được gợi ý!", "Ok");
                return;
            }    
            if (await DisplayAlert("Gợi ý", "Gợi ý sẽ trừ 5 Ruby!", "Ok", "Cancel") == true)
            {
                var a = lsButtonKetQua;
                int index = IndexFirstNotMatching();
                if (index < 0)
                    return;
                player.Ruby -= 5;
                lblRuby.Text = player.Ruby.ToString();
                db.UpdatePlayer();

                string temp = ketQua.Remove(ketQua.IndexOf(' '),1);
                lsButtonKetQua[index].Text = temp[index].ToString();
                lsButtonKetQua[index].IsEnabled = false;
                EnableKyTu(temp[index]);
                if (index >= temp.Length - 1)
                {
                    Wingame();
                }
            } 
        }

        #endregion
    }
}