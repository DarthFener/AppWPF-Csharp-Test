
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Identity.Client;

namespace AppWPF_Csharp_Test.ViewModel
{


 
        public class MainViewModel : ViewModelBase
        {
            private string textVal = "Cambiami";
            
	

	public MainViewModel()
            {
                OnTextChangeButtonClickedCommand = new RelayCommand(OnTextChangeButtonClickedHandler);
                OnLoggingInCommand = new RelayCommand(OnLoggingInHandler);
                OnLogoutCommand = new RelayCommand(OnLogoutHandler);
                OnConectCommand = new RelayCommand(OnConnectHandler);
            }

            public string TextVal
            {
                get { return textVal; }
                set { Set(() => TextVal, ref textVal, value); }
            }

        #region Command
        public RelayCommand OnTextChangeButtonClickedCommand { get; private set; }
            public RelayCommand OnLoggingInCommand { get; private set; }
            public RelayCommand OnLogoutCommand { get; private set; }
            public RelayCommand OnConectCommand { get; private set; }
           
        private void OnTextChangeButtonClickedHandler()
            {
                TextVal = "Testo 2";
            }
        private async void OnLoggingInHandler()
            {
            string[] scopes = new string[] { "user.read" };

            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;
            try
            {

                

                authResult = await(app as PublicClientApplication).AcquireTokenInteractive(scopes)
                    .ExecuteAsync();

                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error Acquiring Token:{Environment.NewLine}{ex}");
            }
        }
        private async void OnLogoutHandler()
        {
            IEnumerable<IAccount> accounts = await App.PublicClientApp.GetAccountsAsync();
            try
            {
                while (accounts.Any())
                {
                    await App.PublicClientApp.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await App.PublicClientApp.GetAccountsAsync();
                }

                UpdateSignInState(false);
            }
            catch (MsalException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error signing-out user: {ex.Message}");
            }
        }
        private async void OnConnectHandler()
        {
            IEnumerable<IAccount> account = await App.PublicClientApp.GetAccountsAsync();
            if (account.Any())
            {
                System.Diagnostics.Debug.WriteLine($"Username: {account.FirstOrDefault().Username}" + Environment.NewLine);
                // System.Diagnostics.Debug.WriteLine($"Token Expires: {authResult.ExpiresOn.ToLocalTime()}" + Environment.NewLine);
            } else
            {
                System.Diagnostics.Debug.WriteLine($"Username: null" + Environment.NewLine);
            }
        }
        #endregion

        private void UpdateSignInState(bool signedIn)
        {
            if (signedIn)
            {
                System.Diagnostics.Debug.WriteLine("Accesso completato");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine( "Risultato: null");
                System.Diagnostics.Debug.WriteLine("tokeninfo: null");

              
            }
        }

        private void DisplayBasicTokenInfo(AuthenticationResult authResult)
        {
          
            if (authResult != null)
            {
                System.Diagnostics.Debug.WriteLine($"Username: {authResult.Account.Username}" + Environment.NewLine);
                System.Diagnostics.Debug.WriteLine($"Token Expires: {authResult.ExpiresOn.ToLocalTime()}" + Environment.NewLine);
            }
        }
    }

}