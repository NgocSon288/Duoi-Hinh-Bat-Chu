using BTTH3_18521694.Models;
using Game.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();              
        }

        public MainPage(bool isWinAll)
        {
            InitializeComponent();
            if(isWinAll)
            {
                db = new DBContext(true);
            }
        }

        DBContext db;
         

        private void Button_Clicked(object sender, EventArgs e)
        {
            if((sender as Button).Text == "TIẾP TỤC")
            {
                db = new DBContext();
                Navigation.PushAsync(new CauHoiPage());
                return;
            }
            string name = txtTen.Text;
            if (name == null || name.Trim() != "")
            { 
                db = new DBContext(true);
                db.Player.Name = name;
                db.UpdatePlayer(); 
                Navigation.PushAsync(new CauHoiPage());
            }
        }

        private void btnThoatGame_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("", "Bạn có muốn thoát game!", "Hông");
        }
    }
}
