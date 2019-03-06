using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JustApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GMapButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GMapPage());
        }

        private async void SignatureButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SiganturePage());
        }
    }
}
