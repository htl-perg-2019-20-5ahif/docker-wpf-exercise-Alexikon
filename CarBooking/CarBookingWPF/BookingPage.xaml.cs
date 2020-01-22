using CarBooking.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarBookingWPF
{
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        private DateTime SelectedDateValue = DateTime.Today;

        public DateTime SelectedDate
        {
            get => SelectedDateValue;
            set
            {
                SelectedDateValue = value;
                GetAllCars();
            }
        }

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        private static HttpClient HttpClient;

        public BookingPage(HttpClient http)
        {
            InitializeComponent();

            HttpClient = http;
            DataContext = this;
            GetAllCars();
        }

        public async void GetAllCars()
        {
            Cars.Clear();
            var response = await HttpClient.GetAsync("Cars/day/" + SelectedDateValue.Day + "." + SelectedDateValue.Month + "." + SelectedDateValue.Year);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Car>>(responseBody).ForEach(c => Cars.Add(c));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var booking = new Booking() { BookedDate = SelectedDateValue, CarId = int.Parse(((Button)sender).Tag.ToString()) };

            var contentBody = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
            await HttpClient.PostAsync("Bookings", contentBody);

            GetAllCars();
        }
    }
}
