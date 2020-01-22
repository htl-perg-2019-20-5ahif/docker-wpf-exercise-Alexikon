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
using System.Windows.Shapes;

namespace CarBookingWPF
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        private DateTime DateForBooking = DateTime.Today;

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        private static readonly HttpClient HttpClient = new HttpClient();

        public BookingWindow()
        {
            InitializeComponent();

            HttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            DataContext = this;
            GetAllCars();
        }

        public async void GetAllCars()
        {
            Cars.Clear();
            var response = await HttpClient.GetAsync("Cars/day/" + SelectedDate.Day + "." + SelectedDate.Month + "." + SelectedDate.Year);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Car>>(responseBody).ForEach(c => Cars.Add(c));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetAllCars();

            DateForBooking = SelectedDate;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var booking = new Booking();
            booking.BookedDate = DateForBooking;
            booking.CarId = (int) ((Button)sender).Tag;

            var contentBody = new StringContent(JsonConvert.SerializeObject(booking));
            await HttpClient.PostAsync("api/Cars/Bookings", contentBody);

            GetAllCars();
        }
    }
}
