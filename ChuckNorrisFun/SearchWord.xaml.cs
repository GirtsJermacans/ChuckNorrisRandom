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
using System.Windows.Shapes;

namespace ChuckNorrisFun
{
    /// <summary>
    /// Interaction logic for SearchWord.xaml
    /// </summary>
    public partial class SearchWord : Window
    {
        // attributes
        private string word = "";
        private string text = "";
        private int index;
        ChuckJokeModel joke;

        // constructor
        public SearchWord()
        {
            InitializeComponent();
            btnNext.IsEnabled = false;
        }

        // operations

        /*
         * Requests from API total for specific word
         * If total > 0, it also gets list of ID's for all these jokes
         * Uses first ID from list to call LoadSpecificJoke that loads first joke
         */
        private async Task LoadSearchResult(string word)
        {
            joke = await Process.SearchChuckJokeWord(word);

            text = joke.Total.ToString();
            txtShowResult.Text = "There are " + text + " jokes";

            if (joke.Total > 0)
            {
                btnNext.Content = "Connecting...";
                await LoadSpecificJoke(joke, index);
                btnNext.IsEnabled = true;
                btnNext.Content = "Next";
            }
        }

        /*
         * Small validation - if last joke in list is reached - goes back to first joke in a list
         * Provides index for next joke in a list
         */
        private async Task LoadNextFromList()
        {
            if (index < joke.Result.Length-1)
                index += 1;
            else
                index = 0;

            await LoadSpecificJoke(joke, index);
        }

        /*
         * Method that gets specific joke based on it's ID
         * Creates within private scope another ChuckJokeModel for getting specific joke - avoids deleting list of id's previously gained
         */
        private async Task LoadSpecificJoke(ChuckJokeModel joke, int index)
        {
            string id = "";
            id = joke.Result[index].ID;
            ChuckJokeModel joke2 = await Process.GetJokesFromList(id);
            txtShowJoke.Text = joke2.Value;
        }

        /*
         * Was thinking about this since I didn't use WPF before, I wasn't sure how to do this the best way
         * I don't like the below because it creates new instance of MainWindow, however I did not close the previous instance when coming to SearchWord window
         */
        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /*
         * A little validaion for input:
         *      - can't be empty string
         *      - can't uses spaces
         *      - can't be less than 4 characters - sometimes API connection struggles to deal with anything less than 4 :(
         * if all good - calls LoadSearchResult() with input as formal parameter
         */
        private async void BtnSearchWord_Click(object sender, RoutedEventArgs e)
        {
            word = txtEnterName.Text;
            btnNext.IsEnabled = false;

            if (word != "")
            {
                if (word.Contains(" "))
                    txtShowResult.Text = "No Spaces";
                else if (word.Length <= 3)
                    txtShowResult.Text = "Minimum 4 Characters";
                else
                {
                    index = 0;
                    await LoadSearchResult(word);
                }
            }
            else
                txtShowResult.Text = "Please Type a Word";
        }

        private async void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            await LoadNextFromList();
        }
    }
}
