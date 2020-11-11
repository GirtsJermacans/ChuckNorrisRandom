using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChuckNorrisFun
{
    /// <summary>
    /// First time connecting to API and first time using WPF
    /// not sure if it was done the most effective way (certainly could look better)
    /// but wanted to try something new and interesting, and for the 7 day notice I think this is nice little project
    /// Youtube channel "IAmTimCorey" had plenty of good info on how to use System.Net / System.Net.Http / System.Threading.Tasks to set up HttpClient
    /// </summary>
    public partial class MainWindow : Window
    {
        // attributes
        ChuckJokeModel joke;
        private bool loadOnce = false;
        private string text = "";
        private int index = 0;

        // constructor
        public MainWindow()
        {
            InitializeComponent();
            ConnectionAPI.InitializeClient();
            btnGenerate.IsEnabled = false;
            btnSearch.IsEnabled = false;
        }

        // operations

        /* 
         * index is set to 0 - when opening application - connects to endpoint for random joke
         * loadOnce - makes sure joke is not loaded without pressing button
         * loads random joke or specific joke - based on index
         */
        private async Task LoadJoke(int index)
        {
            joke = await Process.LoadRandomChuckJoke(index);
            btnGenerate.IsEnabled = true;
            btnGenerate.Content = "Get a Joke";
            btnSearch.IsEnabled = true;

            if (loadOnce)
            {
                text = joke.Value;
                JokeArea.Text = text;
            }
            else
                loadOnce = true;
        }

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            await LoadJoke(index);
        }

        /*
         * Makes sure we can't interact with application before we get positive response from API
         * Particular API on later hours are acting up for some reason, therefore a try / catch
         */
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!loadOnce)
                    await LoadJoke(index);
            }
             catch(Exception)
            {
                MessageBox.Show("Something Went Wrong - On evenings particular API seem to be slower than usually", "PROBLEM");
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWord searchWord = new SearchWord();
            this.Visibility = Visibility.Hidden;
            searchWord.Show();

        }

        /*
         * Provides index for requesting random jokes specific categories
         */
        private void CbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = cbxCategories.SelectedIndex;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
