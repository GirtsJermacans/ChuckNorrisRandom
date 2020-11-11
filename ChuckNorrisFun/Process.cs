using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorrisFun
{
    /* 
     * Using async since when making calls to web - there can be delays
     * Task used since it's easy to implement with 'async' and 'await'
     * 3 different async calls to api:
     *      1) returns random joke value for random joke or a joke from specified category
     *      2) returns 2 values - number of total jokes for specific word; list of ID's of the particular jokes
     *      3) returns specific joke value from previously gained ID
     * API endpoints taken from - https://api.chucknorris.io/
     * Security Protocols tried to apply because sometimes the particular API was very slow and failing to give positive reponse
     * I read that it can be due to Security Protocols it uses. I could not find what specific they use, so I found this little code snippet on website that should cover all of them
     * All credit for Security Protocols used goes to ---> https://codeshare.co.uk/blog/how-to-fix-the-error-authentication-failed-because-the-remote-party-has-closed-the-transport-stream/
    */

    public class Process
    {
        public static async Task<ChuckJokeModel>LoadRandomChuckJoke(int index)
        {
            string url = "";
            if (index == 0)
            {
                url = $"https://api.chucknorris.io/jokes/random";
            }
            else if (index == 1)
            {
                url = $"https://api.chucknorris.io/jokes/random?category={ "animal" }";
            }
            else if (index == 2)
            {
                url = $"https://api.chucknorris.io/jokes/random?category={ "celebrity" }";
            }
            else if (index == 3)
            {
                url = $"https://api.chucknorris.io/jokes/random?category={ "sport" }";
            }
            else if (index == 4)
            {
                url = $"https://api.chucknorris.io/jokes/random?category={ "dev" }";
            }
            else if (index == 5)
            {
                url = $"https://api.chucknorris.io/jokes/random?category={ "food" }";
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpResponseMessage respone = await ConnectionAPI.Client.GetAsync(url))
            {
                if (respone.IsSuccessStatusCode)
                {
                    ChuckJokeModel chuck = await respone.Content.ReadAsAsync<ChuckJokeModel>();
                    return chuck;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public static async Task<ChuckJokeModel>SearchChuckJokeWord(string word)
        {
            string url = "";
            url = $"https://api.chucknorris.io/jokes/search?query={ word }";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpResponseMessage respone = await ConnectionAPI.Client.GetAsync(url))
            {
                if (respone.IsSuccessStatusCode)
                {
                    ChuckJokeModel chuck = await respone.Content.ReadAsAsync<ChuckJokeModel>();
                    return chuck;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }

        public static async Task<ChuckJokeModel>GetJokesFromList(string key)
        {
            string url = "";
            url = $"https://api.chucknorris.io/jokes/" + key;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (HttpResponseMessage respone = await ConnectionAPI.Client.GetAsync(url))
            {
                if (respone.IsSuccessStatusCode)
                {
                    ChuckJokeModel chuck = await respone.Content.ReadAsAsync<ChuckJokeModel>();
                    return chuck;
                }
                else
                {
                    throw new Exception(respone.ReasonPhrase);
                }
            }
        }
    }
}
