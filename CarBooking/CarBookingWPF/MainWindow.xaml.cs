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
        private static readonly HttpClient HttpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            HttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = new ListCarsPage(HttpClient);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage.Content = new BookingPage(HttpClient);
        }
    }
}
