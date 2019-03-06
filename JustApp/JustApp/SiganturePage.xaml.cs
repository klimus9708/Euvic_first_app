using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SignaturePad.Forms;
using System.IO;

namespace JustApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SiganturePage : ContentPage
	{
		public SiganturePage ()
		{
			InitializeComponent ();
		}

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Stream image = await SignaturePAD.GetImageStreamAsync(SignatureImageFormat.Png);
        }
    }
}