using System.ComponentModel.DataAnnotations;
namespace Platform.Cache.Configuration;

public class RedisCacheOptions
{
    [Required]
    public string? Host { get; set; }

    [Required]
    public string? Port { get; set; }

    public string? Password { get; set; }

    public bool AllowAdmin { get; set; }

    public string Server => $"{Host}:{Port}";

    public string ConnectionString => $"{Server}{(string.IsNullOrWhiteSpace(Password) ? string.Empty : $",password={Password}")}{(AllowAdmin ? ",allowAdmin=true" : string.Empty)}";
}