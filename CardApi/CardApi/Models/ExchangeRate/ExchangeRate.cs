namespace CardApi.Models.ExchangeRate;

// Rate defined at Treasury Rates of Exchange API
// Defined https://fiscaldata.treasury.gov/datasets/treasury-reporting-rates-exchange/treasury-reporting-rates-of-exchange#dataset-properties
public class ExchangeRate
{
    public string Exchange_rate { get; set; } = string.Empty;
}
