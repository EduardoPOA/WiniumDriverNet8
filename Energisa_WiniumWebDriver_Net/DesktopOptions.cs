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
    /// O tipo de simulador de teclado.
    /// </summary>
    public enum KeyboardSimulatorType
    {
        /// <summary>
        /// Baseado na classe SendKeys.<see href="https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys(v=vs.110).aspx">See more</see>.
        /// </summary>
        BasedOnWindowsFormsSendKeysClass,

        /// <summary>
        /// Baseado no Windows Input Simulator.
        /// Para métodos adicionais, deve ser convertido para KeyboardSimulatorExt.<see href="http://inputsimulator.codeplex.com/">See more</see>.
        /// </summary>
        BasedOnInputSimulatorLib
    }

    /// <summary>
    /// Classe para gerenciar opções específicas para <see cref="WiniumDriver"/> 
    /// que usa <see href="https://github.com/2gis/Winium.Desktop">Winium.Desktop</see>.
    /// </summary>
    public class DesktopOptions : IWiniumOptions
    {
        #region Constants

        private const string ApplicationPathOption = "app";

        private const string ArgumentsOption = "args";

        private const string DebugConnectToRunningAppOption = "debugConnectToRunningApp";

        private const string KeyboardSimulatorOption = "keyboardSimulator";

        private const string LaunchDelayOption = "launchDelay";

        #endregion

        #region Fields

        private string applicationPath;

        private string arguments;

        private bool? debugConnectToRunningApp;

        private KeyboardSimulatorType? keyboardSimulator;

        private int? launchDelay;

        #endregion

        #region Public Properties

        /// <summary>
        /// Obtém ou define o caminho local absoluto para um arquivo .exe a ser iniciado.
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
        /// Obtém ou define argumentos de inicialização do aplicativo em teste.
        /// </summary>
        public string Arguments
        {
            set
            {
                this.arguments = value;
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
        /// Obtém ou define o tipo de simulador de teclado.
        /// </summary>
        public KeyboardSimulatorType KeyboardSimulator
        {
            set
            {
                this.keyboardSimulator = value;
            }
        }

        /// <summary>
        /// Obtém ou define o atraso de inicialização em milissegundos,
        /// a ser aguardado para permitir que os visuais sejam inicializados
        /// após o início do aplicativo.       
        /// </summary>
        public int LaunchDelay
        {
            set
            {
                this.launchDelay = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converter opções para DesiredCapabilities para Winium Desktop Driver
        /// </summary>
        /// <returns>Os recursos desejados para o driver de desktop Winium com essas opções.</returns>
        public ICapabilities ToCapabilities()
        {
            var capabilityDictionary = new Dictionary<string, object>
                                           {
                                               { ApplicationPathOption, this.applicationPath }
                                           };

            if (!string.IsNullOrEmpty(this.arguments))
            {
                capabilityDictionary.Add(ArgumentsOption, this.arguments);
            }

            if (this.debugConnectToRunningApp.HasValue)
            {
                capabilityDictionary.Add(DebugConnectToRunningAppOption, this.debugConnectToRunningApp);
            }

            if (this.keyboardSimulator.HasValue)
            {
                capabilityDictionary.Add(KeyboardSimulatorOption, this.keyboardSimulator);
            }

            if (this.launchDelay.HasValue)
            {
                capabilityDictionary.Add(LaunchDelayOption, this.launchDelay);
            }

            return new DesiredCapabilities(capabilityDictionary);
        }

        #endregion
    }
}
