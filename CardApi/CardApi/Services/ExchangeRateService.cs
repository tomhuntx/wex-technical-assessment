using CardApi.Data;
using CardApi.Models.Config;
using CardApi.Models.ExchangeRate;
using Microsoft.Extensions.Options;

namespace CardApi.Services;

public class ExchangeRateService(HttpClient httpClient, IOptions<ExternalApis> apiConfig)
{
    private readonly string baseUrl = apiConfig.Value.TreasuryBaseUrl;

    /// <summary>
    /// Get exchange rate from treasury government URL.
    /// Assumes current currency is USD.
    /// </summary>
    /// <param name="currency">Currency as defined by treasury syntax</param>
    /// <param name="transactionDate">Optional date of transaction (defaults to now)</param>
    /// <returns>Exchange rate as a decimal (e.g. 1.52)</returns>
    public async Task<decimal?> GetExchangeRate(string currency, DateTimeOffset? transactionDate = null)
    {
        var date = (transactionDate ?? DateTimeOffset.UtcNow).UtcDateTime;
        var fromDate = date.AddMonths(-6).ToString("yyyy-MM-dd");
        var toDate = date.ToString("yyyy-MM-dd");

        var url = $"{baseUrl}?filter=country_currency_desc:eq:{currency},record_date:lte:{toDate},record_date:gte:{fromDate}" +
                  $"&sort=-record_date&page[size]=1";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();

        if (content?.Data == null || content.Data.Count == 0)
            return null;

        return decimal.Parse(content.Data[0].Exchange_rate);
    }
}
