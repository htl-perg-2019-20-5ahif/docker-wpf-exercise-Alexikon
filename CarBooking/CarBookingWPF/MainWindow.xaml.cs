using CarBooking.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace CarBookingWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        private static readonly HttpClient HttpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            HttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            DataContext = this;
            GetAllCars();
        }

        public async void GetAllCars()
        {
            var response = await HttpClient.GetAsync("Cars");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Car>>(responseBody).ForEach(c => Cars.Add(c));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new BookingWindow().Show();
        }
    }
}
