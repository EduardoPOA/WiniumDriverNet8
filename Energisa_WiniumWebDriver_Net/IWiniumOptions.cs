using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energisa_WiniumWebDriver_Net
{
    #region using

    using OpenQA.Selenium;

    #endregion

    /// <summary>
    /// Define a interface para gerenciar opções específicas para <see cref="WiniumDriver"/>
    /// </summary>
    public interface IWiniumOptions
    {
        #region Public Methods and Operators

        /// <summary>
        ///Converter opções para DesiredCapabilities para um dos drivers Winium 
        /// </summary>
        /// <returns>Os recursos desejados para o driver Winium com essas opções.</returns>
        ICapabilities ToCapabilities();

        #endregion
    }
}
