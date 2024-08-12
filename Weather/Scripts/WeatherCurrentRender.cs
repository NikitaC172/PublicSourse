using MyWeather;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherCurrentRender : MonoBehaviour
{
    [SerializeField] private WeatherCurrent _weatherCurrent;
    [SerializeField] private TMP_Text _idCity;
    [SerializeField] private TMP_Text _date;
    [SerializeField] private TMP_Text _temp;
    [SerializeField] private TMP_Text _pressure;
    [SerializeField] private TMP_Text _wind;
    [SerializeField] private TMP_Text _condition;

    private void OnEnable()
    {
        _weatherCurrent.WeatherCurrentReceived += RenderWeather;
    }

    private void RenderWeather(WeatherInfo weatherInfo)
    {
        _idCity.text = weatherInfo.name;
        SetTime(weatherInfo.dt);
        SetTemp(weatherInfo.main.temp);
        SetPressure(weatherInfo.main.pressure);
        SetWind(weatherInfo.wind.speed, weatherInfo.wind.deg);
        SetCondition(weatherInfo.weather[0].description);
    }

    private void SetTemp(float temp)
    {
        _temp.text = math.round(temp).ToString() + "∞—";
    }

    private void SetPressure(float pressure)
    {
        _pressure.text = pressure.ToString()+" √œ‡";
    }

    private void SetWind(float wind, float direction)
    {
        _wind.text = wind.ToString() + " Ï/Ò   " + direction.ToString() + "∞";
    }

    private void SetCondition(string conditon)
    {
        _condition.text = conditon;
    }

    private void SetTime(long unixTime)
    {
        var localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime.ToLocalTime();
        _date.text = localDateTimeOffset.ToString();
    }
}
