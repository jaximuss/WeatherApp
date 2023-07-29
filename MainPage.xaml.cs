using System.Text.Json;
namespace WeatherApp;

public partial class MainPage : ContentPage
{
    private WeatherApiResponse weatherData;

    // Event handler for the button click to fetch weather data.
    private async void OnGetWeatherClicked(object sender, EventArgs e)
    {
        //the api string no key required
        string URL = "https://api.open-meteo.com/v1/forecast?latitude=6.5&longitude=3.375&rain,is_day&current_weather=true&temperature_unit=fahrenheit";
        
        //try catch for debugging
        try
        {
            // Fetch weather data from the API
            Console.WriteLine("API URL: " + URL);
            weatherData = await GetWeatherDataAsync(URL);
            Console.WriteLine("API Response: " + JsonSerializer.Serialize(weatherData));

            // Update the UI with weather information
            if (weatherData != null)
            {
                // Log the received latitude, longitude, and timezone
                Console.WriteLine("Latitude: " + weatherData.Latitude);
                Console.WriteLine("Longitude: " + weatherData.Longitude);
                Console.WriteLine("Timezone: " + weatherData.Timezone);

                cityNameLabel.Text = "Location: " + weatherData.Latitude + ", " + weatherData.Longitude;
                descriptionLabel.Text = "Timezone: " + weatherData.Timezone;
            }
            else
            {
                cityNameLabel.Text = "Weather data not available";
                temperatureLabel.Text = "";
                descriptionLabel.Text = "";
            }
        }
        catch (Exception ex)
        {
            // Log or display the exception details
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            cityNameLabel.Text = "Error fetching weather data";
            temperatureLabel.Text = "";
            descriptionLabel.Text = "";
        }
    }

    public MainPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// for fetching the weather data from the api and deserializing it then returning the weather data
    /// </summary>
    /// <param name="apiUrl"></param>
    /// <returns></returns>
    private async Task<WeatherApiResponse> GetWeatherDataAsync(string apiUrl)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherApiResponse>(content);
                return weatherData;
            }

            return null;
        }
    }
}
