using AppWPF_Csharp_Test.Servizi;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppWPF_Csharp_Test
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string ClientId = "4d907865-b200-48f3-a7df-7b270547428f";
        private static string Tenant = "5fcae4c2-4cd5-4129-8552-8066c29493cb";
        private static string Instance = "https://login.microsoftonline.com/";

        public static IPublicClientApplication PublicClientApp { get; private set; }

        static App()
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithRedirectUri("http://localhost")
                .Build();

            TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }
        private static void Log(LogLevel level, string message, bool containsPii)
        {
            string logs = ($"{level} {message}");
            StringBuilder sb = new StringBuilder();
            sb.Append(logs);
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
            sb.Clear();
        }
    }
}
