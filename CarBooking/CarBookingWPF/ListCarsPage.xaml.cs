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
    /// Interaction logic for ListCarsPage.xaml
    /// </summary>
    public partial class ListCarsPage : Page
    {
        public ObservableCollection<Car> Cars { get; } = new ObservableCollection<Car>();

        private HttpClient HttpClient;

        public ListCarsPage(HttpClient http)
        {
            InitializeComponent();

            HttpClient = http;
            DataContext = this;
            GetAllCars();
        }

        public async void GetAllCars()
        {
            var response = await HttpClient.GetAsync("Cars");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Car>>(responseBody).ForEach(c => Cars.Add(c));
        }
    }
}
