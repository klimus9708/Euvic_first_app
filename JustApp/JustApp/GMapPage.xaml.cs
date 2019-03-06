using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;

namespace JustApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GMapPage : ContentPage
	{
        Pin pin1 = null;
        public GMapPage ()
		{
			InitializeComponent ();


            //Wybór typu map

            var mapTypeValuse = new List<MapType>();
            foreach (var mapType in Enum.GetValues(typeof(MapType)))
            {
                mapTypeValuse.Add((MapType)mapType);
                pickerMapType.Items.Add(Enum.GetName(typeof(MapType), mapType));
            }
            pickerMapType.SelectedIndexChanged += (sender, e) =>
            {
                map.MapType = mapTypeValuse[pickerMapType.SelectedIndex];
            };
            pickerMapType.SelectedIndex = 0;


            //włącznik lokalizacji 
            switchMyLocationEnabled.Toggled += (sender, e) =>
            {
                map.MyLocationEnabled = e.Value;
            };
            switchMyLocationEnabled.IsToggled = map.MyLocationEnabled;

            //przycisk lokalizacji 

            switchMyLocationButtonEnabled.Toggled += (sender, e) =>
            {
                map.UiSettings.MyLocationButtonEnabled = e.Value;
            };


            switchMyLocationButtonEnabled.IsToggled = map.UiSettings.MyLocationButtonEnabled;



        }
        //Konwersja wpisywanego adressu na współrzędne

        private async void Adres_Clicked(object sender, EventArgs e)
        {
            

            var addresTyped = MyBar.Text;
            try
            {
                var address = addresTyped;
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    pin1 = new Pin()
                    {
                        Type = PinType.Place,
                        Label = addresTyped,
                        Address = addresTyped,
                        Position = new Position(location.Latitude, location.Longitude),
                        Rotation = 33.3f,
                        Tag = "id_newPin",
                        IsVisible = true

                    };
                    
                    map.Pins.Add(pin1);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Distance.FromMeters(3000)));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            map.Pins.Clear();
            pin1 = null;
        }
    }
}
	
