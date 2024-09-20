using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energisa_WiniumWebDriver_Net
{
    using OpenQA.Selenium;
    #region using

    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    #endregion

    /// <summary>
    /// Expõe o serviço fornecido pelo executável nativo do driver Winium.
    /// </summary>
    /// <example>
    /// <code>
    /// [TestFixture]
    /// public class Testing
    /// {
    ///     private IWebDriver driver;
    ///     <para></para>
    ///     [SetUp]
    ///     public void SetUp()
    ///     {
    ///         var service =
    ///                WiniumDriverService.CreateDesktopService("path_to_driver_executible_folder");
    ///
    ///        service.SuppressInitialDiagnosticInformation = false;
    ///        service.EnableVerboseLogging = true;
    ///
    ///         var options = new DesktopOptions { ApplicationPath = @"‪C:\Windows\System32\notepad.exe" };
    ///         driver = new WiniumDriver(service, options, TimeSpan.FromMinutes(30));
    ///     }
    ///     <para></para>
    ///     [Test]
    ///     public void TestGoogle()
    ///     {
    ///        /*
    ///         *   Rest of the test
    ///         */
    ///     }
    ///     <para></para>
    ///     [TearDown]
    ///     public void TearDown()
    ///     {
    ///         driver.Quit();
    ///     } 
    /// }
    /// </code>
    /// </example>
    public class WiniumDriverService : DriverService
    {
        #region Constants

        private const string DesktopDriverServiceFileName = "Winium.Desktop.Driver.exe";

        private const string SilverlightDriverServiceFileName = "WindowsPhoneDriver.OuterDriver.exe";

        private const string StoreAppsDriverServiceFileName = "Winium.StoreApps.Driver.exe";

        #endregion

        #region Static Fields

        private static readonly Uri DesktopDriverDownloadUrl = new Uri(
            "https://github.com/2gis/Winium.Desktop/releases");

        private static readonly Uri SilverlightDriverDownloadUrl =
            new Uri("https://github.com/2gis/winphonedriver/releases");

        private static readonly Uri StoreAppsDriverDownloadUrl =
            new Uri("https://github.com/2gis/Winium.StoreApps/releases");

        private static readonly Uri WiniumDownloUrl = new Uri("https://github.com/2gis/Winium");

        #endregion

        #region Fields

        private bool enableVerboseLogging;

        private string logPath = string.Empty;

        #endregion

        #region Constructors and Destructors

        private WiniumDriverService(string executablePath, string executableFileName, int port, Uri downloadUrl)
            : base(executablePath, port, executableFileName, downloadUrl)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Obtém ou define um valor que indica se o log detalhado deve ser habilitado para o executável do Winium Driver.
        /// O padrão é <see langword="false"/>.
        /// </summary>
        public bool EnableVerboseLogging
        {
            get
            {
                return this.enableVerboseLogging;
            }

            set
            {
                this.enableVerboseLogging = value;
            }
        }

        /// <summary>
        /// Obtém ou define o local do arquivo de log gravado pelo executável do driver Winium.
        /// </summary>
        public string LogPath
        {
            get
            {
                return this.logPath;
            }

            set
            {
                this.logPath = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Obtém os argumentos da linha de comando para o serviço de driver.
        /// </summary>
        protected override string CommandLineArguments
        {
            get
            {
                StringBuilder argsBuilder = new StringBuilder(base.CommandLineArguments);

                if (this.SuppressInitialDiagnosticInformation)
                {
                    argsBuilder.Append(" --silent");
                }

                if (this.enableVerboseLogging)
                {
                    argsBuilder.Append(" --verbose");
                }

                if (!string.IsNullOrEmpty(this.logPath))
                {
                    argsBuilder.AppendFormat(CultureInfo.InvariantCulture, " --log-path={0}", this.logPath);
                }

                return argsBuilder.ToString();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Cria uma instância padrão do WiniumDriverService usando um caminho especificado para o executável do Winium Driver com o nome fornecido.
        /// </summary>
        /// <param name="driverPath">O diretório contendo o executável do Winium Driver.</param>
        /// <param name="driverExecutableFileName">O nome do arquivo executável do Winium Driver.</param>
        /// <returns>Um <see cref="WiniumDriverService"/> usando uma porta aleatória.</returns>

        public static WiniumDriverService CreateDefaultService(string driverPath, string driverExecutableFileName)
        {
            return new WiniumDriverService(
                driverPath,
                driverExecutableFileName,
                FindFreePort(),
                WiniumDownloUrl);
        }

        /// <summary>
        /// Cria uma instância padrão do WiniumDriverService usando um caminho especificado para o Winium Desktop Driver.
        /// </summary>
        /// <param name="driverPath">O diretório contendo o executável do Winium Desktop Driver.</param>
        /// <returns>Um <see cref="WiniumDriverService"/> usando Winium Desktop e uma porta aleatória.</returns>
        public static WiniumDriverService CreateDesktopService(string driverPath)
        {
            return new WiniumDriverService(
                driverPath,
                DesktopDriverServiceFileName,
                FindFreePort(),
                DesktopDriverDownloadUrl);
        }

        /// <summary>
        /// Cria uma instância padrão do WiniumDriverService usando um caminho especificado para o WindowsPhone Driver.
        /// </summary>
        /// <param name="driverPath">O diretório contendo o executável do WindowsPhone Driver.</param>
        /// <returns>Um <see cref="WiniumDriverService"/> usando WindowsPhone Driver e uma porta aleatória.</returns>
        public static WiniumDriverService CreateSilverlightService(string driverPath)
        {
            return new WiniumDriverService(
                driverPath,
                SilverlightDriverServiceFileName,
                FindFreePort(),
                SilverlightDriverDownloadUrl);
        }

        /// <summary>
        /// Cria uma instância padrão do WiniumDriverService usando um caminho especificado para o Winium StoreApps Driver.
        /// </summary>
        /// <param name="driverPath">O diretório contendo o executável do Winium StoreApps Driver.</param>
        /// <returns>Um <see cref="WiniumDriverService"/> usando Winium StoreApps e uma porta aleatória.</returns>
        public static WiniumDriverService CreateStoreAppsService(string driverPath)
        {
            return new WiniumDriverService(
                driverPath,
                StoreAppsDriverServiceFileName,
                FindFreePort(),
                StoreAppsDriverDownloadUrl);
        }

        #endregion

        private static int FindFreePort()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 0));
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
            finally
            {
                socket.Close();
            }
        }
    }
}
