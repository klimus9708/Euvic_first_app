using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using Plugin.ExternalMaps;

namespace JustApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GMapPage : ContentPage
	{
        Pin pin1 = null;
        Pin CustomPin;
        public string stan;
        int i;




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
        //Konwersja wpisywanego adresu na współrzędne

        public async void Adres_Clicked(object sender, EventArgs e)
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
                
                pin1.Clicked += Pin1_Clicked;
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

        private void Pin1_Clicked(object sender, EventArgs e)
        {
            //CrossExternalMaps.Current.NavigateTo("Wybrany adres",pin1.Position.Latitude,pin1.Position.Longitude);
        }


        //Włącznie pinu z przycisku
        public void ButtonTurnOnPins_Clicked(object sender, EventArgs e)
        {

            {
                CustomPin = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Euvic",
                    Address = "Puławska 182",
                    Position = new Position(52.181698, 21.021615),
                };
                CustomPin.Tag = "Genrowany tag"; //<---- na tym dziłaj 
                map.Pins.Add(CustomPin);

                map.PinClicked += Map_PinClicked;
                map.InfoWindowClicked += InfoWindow_Clicked;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(CustomPin.Position, Distance.FromKilometers(0.5)), true);
            }

        }

        //wywołanie akcji dla kliknięcia pinu
        async void Map_PinClicked(object sender, PinClickedEventArgs e)
        {

            if (i % 2 == 0)
            {
                stan = "Zapłacone";
            }
            else
            {
                stan = "Nie zapłacone";
            }
            string messege = ("\nPłatność: " + stan + "\n" + "adres: Puławska 182" + /*adres*/ "\n" + "");
            await DisplayAlert("Dane:", $"{e.Pin.Tag}" + messege, "Close");
           

        }


        //wywoałeni akcji dla kliknięci info window

        void InfoWindow_Clicked(object sender, InfoWindowClickedEventArgs e)
        {

            Device.OpenUri(new Uri("https://www.euvic.pl/o-nas/o-euvic/"));
        }


        //usuwanie wszystkich pinów 

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            map.Pins.Clear();
            pin1 = null;
        }


        //obsługa guzika dla zmiany stanu 
        private void ChangeStan_Clicked(object sender, EventArgs e)
        {
            i++;
        }

    }
}
	
