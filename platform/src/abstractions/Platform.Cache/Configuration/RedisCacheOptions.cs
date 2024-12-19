using System.ComponentModel.DataAnnotations;
namespace Platform.Cache.Configuration;

public class RedisCacheOptions
{
    [Required]
    public string? Host { get; set; }

    [Required]
    public string? Port { get; set; }

    public string? Password { get; set; }

    public string ConnectionString => $"{Host}:{Port}{(string.IsNullOrWhiteSpace(Password) ? string.Empty : $",password={Password}")}";
}