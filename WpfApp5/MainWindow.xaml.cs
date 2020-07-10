using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image btnOn;
        Image btnOff;
        StringContent content;
        HttpClient client;
        private bool IsOn;

        public MainWindow()
        {
            InitializeComponent();

            btnOn = new Image
            {
                Source = new BitmapImage(new Uri(@"/WpfApp5;component/Images/btnon.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Fill,
                Height = 256,
                Width = 256
            };
            btnOff = new Image
            {
                Source = new BitmapImage(new Uri(@"/WpfApp5;component/Images/btnOff.png", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Fill,
                Height = 256,
                Width = 256
            };
            client = new HttpClient();
            GetState();

        }
        private async Task GetState()
        {
            var result = await client.GetStringAsync("http://localhost:8080/rest/items/Presence_Mobile_John/state");

            if (result == "ON")
            {
                IsOn = true;
                On_off.Content = btnOn;
            }
            else
            {
                IsOn = false;
                On_off.Content = btnOff;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Clicked";
            if (!IsOn)
            {
                 content = new StringContent("ON");
                (sender as Button).Content = btnOn;
                IsOn = true;
            }
            else
            {
                content = new StringContent("OFF");
                (sender as Button).Content = btnOff;
                IsOn = false;
            }
                client.PutAsync("http://localhost:8080/rest/items/Presence_Mobile_John/state",content );
        }

    }
}
