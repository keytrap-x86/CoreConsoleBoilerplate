using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreConsoleBoilerplate
{
    public class App : IApp
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public App(ILogger<App> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }





        /// <summary>
        ///     App's main run method
        /// </summary>
        public void Run()
        {
            // Example of getting a value from appsettings
            var myValue = _config.GetValue<int>("MyValue");
            _logger.LogInformation("My value is {mySuperValue}", myValue);

            // Your code goes here
            System.Console.ReadLine();
        }





        #region Private fields

        private readonly ILogger<App> _logger;
        private readonly IConfiguration _config;

        #endregion
    }
}
