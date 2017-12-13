using Microsoft.Extensions.Configuration;
using System.IO;

namespace Station.Common.Classes
{
    public class AppConfiguration
    {
        static IConfigurationRoot configuration;

        static public IConfigurationRoot Configuration
        {
            get
            {
                if (configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

                    configuration = builder.Build();
                }

                return configuration;
            }
        }
    }
}