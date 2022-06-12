using Deliverix.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Deliverix.Common.Configurations;

public static class AppConfiguration
{
    private static IConfiguration _configuration;
    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GetConfiguration(string key, string section = "AppSettings")
    {
        return _configuration[$"{section}:{key}"] ?? 
               throw new BusinessException("Configuration file does not have a value with given key");

    }
}