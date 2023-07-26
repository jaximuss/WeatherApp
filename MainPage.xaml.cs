using System.Text.Json;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	int count = 0;

    private WeatherData weatherData;

    // Event handler for the button click to fetch weather data.
    private async void OnGetWeatherClicked(object sender, EventArgs e)
    {
        string apiKey = "YOUR_API_KEY"; // Replace with your OpenWeatherMap API key
        string cityOrZipCode = cityEntry.Text; // Get the city name or ZIP code from the entry field

        // Fetch weather data from the API
        weatherData = await GetWeatherDataAsync(cityOrZipCode, apiKey);

        // Update the UI with weather information
        if (weatherData != null)
        {
            cityNameLabel.Text = weatherData.Name;
            temperatureLabel.Text = $"{weatherData.Main.Temp}°C";
            descriptionLabel.Text = weatherData.Weather[0].Description;
        }
        else
        {
            cityNameLabel.Text = "City not found";
            temperatureLabel.Text = "";
            descriptionLabel.Text = "";
        }
    }
    public MainPage()
	{
		InitializeComponent();
	}

    private async Task<WeatherData> GetWeatherDataAsync(string cityOrZipCode, string apiKey)
    {
        using (var httpClient = new HttpClient())
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityOrZipCode}&appid={apiKey}&units=metric";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherData>(content);
                return weatherData;
            }

            return null;
        }
    }
}

