// <copyright file="JwtSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BuberDinner.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string? Secret {get;init;} = string.Empty;
    public string? Issuer { get; init; } = string.Empty;
    public string? Audience { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; } = 60;
}
