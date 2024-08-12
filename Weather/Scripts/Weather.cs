using System;
using System.Collections.Generic;

[Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
}

[Serializable]
public class Main
{
    public float temp;
    public float pressure;
}

[Serializable]
public class Wind
{
    public float speed;
    public float deg;
}

[Serializable]
public class WeatherInfo
{
    public int id;
    public string name;
    public long dt;
    public List<Weather> weather;
    public Main main;
    public Wind wind;
}
