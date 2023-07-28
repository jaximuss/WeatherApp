namespace WeatherApp
{
    public class WeatherData
    {
        public string Name { get; set; }
        public WeatherInfo Main { get; set; }
        public List<WeatherDescription> Weather { get; set; }
    }

    public class WeatherInfo
    {
        public double Temp { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
    }

}