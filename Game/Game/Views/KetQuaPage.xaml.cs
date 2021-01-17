using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KetQuaPage : ContentPage
    {
        public KetQuaPage(string ketQua)
        {
            InitializeComponent();
            BindingContext = ketQua;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CauHoiPage());
        }
    }
}