using UnityEngine;

namespace MyWeather
{
    public class WeatherApi : MonoBehaviour
    {
        [SerializeField] private string _idUser;
        [SerializeField] private string _idCity;

        public string IdCity => _idCity;
        public string IdUser => _idUser;

        public void SetIdCity(string idCity)
        {
            _idCity = idCity;
        }
    }
}
