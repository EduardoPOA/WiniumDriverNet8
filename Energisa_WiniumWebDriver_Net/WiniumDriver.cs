using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energisa_WiniumWebDriver_Net
{
    #region using

    using System;

    using OpenQA.Selenium.Remote;

    #endregion

    /// <summary>
    /// Fornece um mecanismo para escrever testes usando o driver Winium.
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
    ///         var options = new DesktopOptions { ApplicationPath = @"‪C:\Windows\System32\notepad.exe" };
    ///         driver = new WiniumDriver(options);
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
    public class WiniumDriver : RemoteWebDriver
    {
        #region Constructors and Destructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="WiniumDriver"/> usando o caminho especificado
        /// para o diretório que contém o arquivo executável Winium.Driver e as opções.
        /// </summary>
        /// <param name="winiumDriverDirectory">
        /// O caminho completo para o diretório que contém o executável Winium.Webdriver.
        /// </param>
        /// <param name="options">
        /// O <see cref="DesktopOptions"/> para ser usado com o driver Winium.
        /// </param>
        public WiniumDriver(string winiumDriverDirectory, IWiniumOptions options)
            : this(winiumDriverDirectory, options, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="WiniumDriver"/> usando o caminho especificado
        /// para o diretório que contém o arquivo executável Winium.Driver, 
        /// opções e tempo limite de comando.     
        /// </summary>
        /// <param name="winiumDriverDirectory">
        /// O caminho completo para o diretório que contém o arquivo executável Winium.Webdriver.
        /// </param>
        /// <param name="options">
        /// O <see cref="DesktopOptions"/> para ser usado com o driver Winium.
        /// </param>
        /// <param name="commandTimeout">
        /// O tempo máximo de espera para cada comando.
        /// </param>
        public WiniumDriver(string winiumDriverDirectory, IWiniumOptions options, TimeSpan commandTimeout)
            : this(CreateDefaultService(options.GetType(), winiumDriverDirectory), options, commandTimeout)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância do <see cref="WiniumDriver"/> classe usando o especificado
        /// <see cref="WiniumDriverService"/> e opções.
        /// </summary>
        /// <param name="service">O <see cref="WiniumDriverService"/> para usar.</param>
        /// <param name="options">O <see cref="DesktopOptions"/> usado para inicializar o driver.</param>
        public WiniumDriver(WiniumDriverService service, IWiniumOptions options)
            : this(service, options, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="WiniumDriver"/> usando o <see cref="WiniumDriverService"/> especificado.
        /// </summary>
        /// <param name="service">O <see cref="WiniumDriverService"/> a ser utilizado.</param>
        /// <param name="options">O objeto <see cref="IWiniumOptions"/> a ser usado com o driver Winium.</param>
        /// <param name="commandTimeout">O tempo máximo de espera para cada comando.</param>
        public WiniumDriver(WiniumDriverService service, IWiniumOptions options, TimeSpan commandTimeout)
            : base(new WiniumDriverCommandExecutor(service, commandTimeout), options.ToCapabilities())
        {
            this.InitWiniumDriverCommands();
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="WiniumDriver"/> usando o endereço remoto e as opções especificadas.
        /// </summary>
        /// <param name="remoteAddress">URI contendo o endereço do servidor remoto do WiniumDriver (por exemplo, http://127.0.0.1:4444/wd/hub).</param>
        /// <param name="options">O objeto <see cref="IWiniumOptions"/> a ser usado com o driver Winium.</param>
        public WiniumDriver(Uri remoteAddress, IWiniumOptions options)
            : this(remoteAddress, options, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="WiniumDriver"/> usando o endereço remoto, as capacidades desejadas e o tempo limite de comando especificados.
        /// </summary>
        /// <param name="remoteAddress">URI contendo o endereço do servidor remoto do WiniumDriver (por exemplo, http://127.0.0.1:4444/wd/hub).</param>
        /// <param name="options">O objeto <see cref="IWiniumOptions"/> a ser usado com o driver Winium.</param>
        /// <param name="commandTimeout">O tempo máximo de espera para cada comando.</param>
        public WiniumDriver(Uri remoteAddress, IWiniumOptions options, TimeSpan commandTimeout)
            : base(CommandExecutorFactory.GetHttpCommandExecutor(remoteAddress, commandTimeout), options.ToCapabilities())
        {
            this.InitWiniumDriverCommands();
        }

        #endregion

        #region Methods

        private static WiniumDriverService CreateDefaultService(Type optionsType, string directory)
        {
            if (optionsType == typeof(DesktopOptions))
            {
                return WiniumDriverService.CreateDesktopService(directory);
            }

            if (optionsType == typeof(StoreAppsOptions))
            {
                return WiniumDriverService.CreateStoreAppsService(directory);
            }

            if (optionsType == typeof(SilverlightOptions))
            {
                return WiniumDriverService.CreateSilverlightService(directory);
            }

            throw new ArgumentException(
                "Option type must be type of DesktopOptions, StoreAppsOptions or SilverlightOptions",
                "optionsType");
        }

        private void InitWiniumDriverCommands()
        {
            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "findDataGridCell",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/cell/{row}/{column}"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "getDataGridColumnCount",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/column/count"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "getDataGridRowCount",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/row/count"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "scrollToDataGridCell",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/scroll/{row}/{column}"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "selectDataGridCell",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/datagrid/select/{row}/{column}"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "scrollToListBoxItem",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/listbox/scroll"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "findMenuItem",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/item/{path}"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "selectMenuItem",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/select/{path}"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "isComboBoxExpanded",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expanded"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "expandComboBox",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "collapseComboBox",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "findComboBoxSelectedItem",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items/selected"));

            this.CommandExecutor.CommandInfoRepository.TryAddCommand(
                "scrollToComboBoxItem",
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/scroll"));
        }

        #endregion
    }
}

