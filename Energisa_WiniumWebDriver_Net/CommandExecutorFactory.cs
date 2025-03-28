﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energisa_WiniumWebDriver_Net
{
    #region using

    using System;
    using System.Reflection;

    using OpenQA.Selenium.Remote;

    #endregion

    internal static class CommandExecutorFactory
    {
        #region Public Methods and Operators

        public static ICommandExecutor GetHttpCommandExecutor(Uri remoteAddress, TimeSpan commandTimeout)
        {
            var seleniumAssembly = Assembly.Load("WebDriver");
            var commandType = seleniumAssembly.GetType("OpenQA.Selenium.Remote.HttpCommandExecutor");
            ICommandExecutor commandExecutor = null;

            if (null != commandType)
            {
                commandExecutor =
                    Activator.CreateInstance(commandType, new object[] { remoteAddress, commandTimeout }) as
                    ICommandExecutor;
            }

            return commandExecutor;
        }

        #endregion
    }
}
