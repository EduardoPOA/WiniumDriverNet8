using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energisa_WiniumWebDriver_Net
{
    #region using

    using System.Collections.Generic;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    /// <summary>
    /// Classe para gerenciar opções específicas para <see cref="WiniumDriver"/> 
    /// que usa <see href="https://github.com/2gis/Winium.StoreApps">Winium.StoreApps</see>.
    /// </summary>
    public class StoreAppsOptions : IWiniumOptions
    {
        #region Constants

        private const string ApplicationPathOption = "app";

        private const string DebugConnectToRunningAppOption = "debugConnectToRunningApp";

        private const string DependenciesOption = "dependencies";

        private const string DeviceNameOption = "deviceName";

        private const string FilesOption = "files";

        private const string LaunchDelayOption = "launchDelay";

        private const string LaunchTimeoutOption = "launchTimeout";

        #endregion

        #region Fields

        private string applicationPath;

        private bool? debugConnectToRunningApp;

        private List<string> dependencies;

        private string deviceName;

        private Dictionary<string, string> files;

        private int? launchDelay;

        private int? launchTimeout;

        #endregion

        #region Public Properties

        /// <summary>
        /// Obtém ou define o caminho local absoluto para um arquivo .appx a ser instalado e iniciado.
        /// Esse recurso não é necessário se debugConnectToRunningApp for especificado.
        /// </summary>
        public string ApplicationPath
        {
            set
            {
                this.applicationPath = value;
            }
        }

        /// <summary>
        /// Obtém ou define um valor que indica se o debug conecta ao aplicativo em execução.
        /// Se verdadeiro, a etapa de inicialização do aplicativo é ignorada.
        /// </summary>
        public bool DebugConnectToRunningApp
        {
            set
            {
                this.debugConnectToRunningApp = value;
            }
        }

        /// <summary>
        /// Obtém ou define uma lista de dependências.
        /// </summary>
        public List<string> Dependencies
        {
            set
            {
                this.dependencies = value;
            }
        }

        /// <summary>
        /// Obtém ou define o nome do emulador a ser usado para executar o teste.
        /// Observe que esse recurso não é necessário, se nenhum nome de dispositivo for especificado,
        /// então um emulador padrão será usado. Você pode especificar apenas um nome parcial,
        /// o primeiro emulador que começar com o deviceName especificado será selecionado.
        /// </summary>
        public string DeviceName
        {
            set
            {
                this.deviceName = value;
            }
        }

        /// <summary>
        /// Obtém ou define os arquivos.
        /// Cada chave do dicionário é "caminho do arquivo local", 
        /// cada valor correspondente é "caminho do arquivo remoto"        /// </summary>
        public Dictionary<string, string> Files
        {
            set
            {
                this.files = value;
            }
        }

        /// <summary>
        /// Obtém ou define o atraso de inicialização em milissegundos, a ser aguardado para permitir que os visuais sejam inicializados após o lançamento do aplicativo (após ping ou tempo limite bem-sucedido).
        /// Use-o se o sistema que executa o emulador for lento.
        /// </summary>
        public int LaunchDelay
        {
            set
            {
                this.launchDelay = value;
            }
        }

        /// <summary>
        /// Obtém ou define o tempo limite máximo, em milissegundos, a ser aguardado para o lançamento do aplicativo.        /// </summary>
        public int LaunchTimeout
        {
            set
            {
                this.launchTimeout = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converter opções para DesiredCapabilities para Winium StoreApps Driver
        /// </summary>
        /// <returns>O DesiredCapabilities para Winium StoreApps Driver com essas opções.</returns>
        public ICapabilities ToCapabilities()
        {
            var capabilityDictionary = new Dictionary<string, object>
                                           {
                                               { ApplicationPathOption, this.applicationPath }
                                           };

            if (this.files.Count > 0)
            {
                capabilityDictionary.Add(FilesOption, this.files);
            }

            if (this.debugConnectToRunningApp.HasValue)
            {
                capabilityDictionary.Add(DebugConnectToRunningAppOption, this.debugConnectToRunningApp);
            }

            if (!string.IsNullOrEmpty(this.deviceName))
            {
                capabilityDictionary.Add(DeviceNameOption, this.deviceName);
            }

            if (this.launchTimeout.HasValue)
            {
                capabilityDictionary.Add(LaunchTimeoutOption, this.launchTimeout);
            }

            if (this.launchDelay.HasValue)
            {
                capabilityDictionary.Add(LaunchDelayOption, this.launchDelay);
            }

            if (this.dependencies.Count > 0)
            {
                capabilityDictionary.Add(DependenciesOption, this.dependencies);
            }

            return new DesiredCapabilities(capabilityDictionary);
        }

        #endregion
    }
}
