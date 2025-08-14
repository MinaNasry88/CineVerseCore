using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class RecaptchaService
{
    private readonly HttpClient _httpClient;
    private readonly string _secretKey;

    public RecaptchaService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _secretKey = config["GoogleReCaptcha:SecretKey"]!;
    }

    public async Task<bool> VerifyTokenAsync(string token)
    {
        var response = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}",
            null);

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<RecaptchaResponse>(json);
        return result!.success && result.score >= 0.5; // Adjust score threshold as needed
    }

    private class RecaptchaResponse
    {
        public bool success { get; set; }
        public float score { get; set; }
        public string? action { get; set; }
    }
}