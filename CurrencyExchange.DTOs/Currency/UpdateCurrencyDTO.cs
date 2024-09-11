﻿namespace CurrencyExchange.DTOs.Currency;

public class UpdateCurrencyDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}