using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace MyWeather
{
    public class WeatherCurrent : MonoBehaviour
    {
        [SerializeField] private WeatherApi _weatherApi;

        private bool _isWorking = false;
        private const string WeatherPeriod = "current";

        public Action<WeatherInfo> WeatherCurrentReceived;

        public void GetWeather()
        {
            StartCoroutine(GetWeatherCor());
            
            /*HttpWebRequest request =(HttpWebRequest)WebRequest.Create(String.Format("https://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric&lang=ru", _weatherApi.IdCity,_weatherApi.IdUser));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
            WeatherCurrentReceived.Invoke(info);
            Debug.Log(jsonResponse);*/
        }

        private IEnumerator GetWeatherCor()
        {
            if (_isWorking == false)
            {
                Debug.Log("Send");
                _isWorking = true;
                using (UnityWebRequest req = UnityWebRequest.Get(String.Format("https://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric&lang=ru", _weatherApi.IdCity, _weatherApi.IdUser)))
                {
                    yield return req.SendWebRequest();

                    while (!req.isDone)
                    {
                        yield return null;
                    }

                    byte[] result = req.downloadHandler.data;

                    if (result != null)
                    {
                        string weatherJSON = System.Text.Encoding.Default.GetString(result);
                        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(weatherJSON);
                        WeatherCurrentReceived.Invoke(info);
                    }
                    else
                    {
                        Debug.LogWarning("Cant receive");
                    }
                }

                _isWorking = false;
            }
        }
    }
}
