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
    /// que usa <see href="https://github.com/2gis/winphonedrive">Windows Phone Driver</see>.
    /// </summary>
    public class SilverlightOptions : IWiniumOptions
    {
        #region Constants

        private const string ApplicationPathOption = "app";

        private const string DebugConnectToRunningAppOption = "debugConnectToRunningApp";

        private const string DeviceNameOption = "deviceName";

        private const string InnerPortOption = "innerPort";

        private const string LaunchDelayOption = "launchDelay";

        private const string LaunchTimeoutOption = "launchTimeout";

        #endregion

        #region Fields

        private string applicationPath;

        private bool? debugConnectToRunningApp;

        private string deviceName;

        private int? innerPort;

        private int? launchDelay;

        private int? launchTimeout;

        #endregion

        #region Public Properties

        /// <summary>
        /// Obtém ou define o caminho local absoluto para um arquivo .xap a ser instalado e iniciado.
        /// Esta capacidade não é necessária se debugConnectToRunningApp for especificado.
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
        /// Obtém ou define a porta interna usada para comunicação entre OuterDriver e InnerDrive (injetada no aplicativo do Windows Phone).
        /// Obrigatório somente se uma porta não padrão foi especificada no aplicativo testado na chamada AutomationServer.Instance.InitializeAndStart.        /// </summary>
        public int InnerPort
        {
            set
            {
                this.innerPort = value;
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
        /// Obtém ou define o tempo limite máximo, em milissegundos, a ser aguardado para o lançamento do aplicativo.
        /// </summary>
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
        /// Converter opções para DesiredCapabilities para driver do Windows Phone
        /// </summary>
        /// <returns>O DesiredCapabilities para driver do Windows Phone com essas opções.</returns>
        public ICapabilities ToCapabilities()
        {
            var capabilityDictionary = new Dictionary<string, object>
                                           {
                                               { ApplicationPathOption, this.applicationPath }
                                           };

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

            if (!string.IsNullOrEmpty(this.deviceName))
            {
                capabilityDictionary.Add(DeviceNameOption, this.deviceName);
            }

            if (this.innerPort.HasValue)
            {
                capabilityDictionary.Add(InnerPortOption, this.innerPort);
            }

            return new DesiredCapabilities(capabilityDictionary);
        }

        #endregion
    }
}
