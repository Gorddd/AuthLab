using AuthLab.DbAuthorization;
using System.Windows.Threading;

namespace AuthLab.Host.Application;

public class DbAuthorization
{
    public string _encryptedDbPath;
    public string _tempDbPath;
    public string _password = null!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="encryptedDbPath">Зашифрованная бд, используется только для расшифрования</param>
    /// <param name="tempDbPath">Путь на бд, которая хранится временно, и которая впоследствии (при выходе из проги) будет шифроваться</param>
    public DbAuthorization(string encryptedDbPath, string tempDbPath)
    {
        _encryptedDbPath = encryptedDbPath;
        _tempDbPath = tempDbPath;
    }

    public async Task<bool> DbLogin() //Запуск приложения
    {
        if (EncryptedDbExists(_encryptedDbPath)) // Существует зашифрованная бд, пытаемся расшифровать
        {
            
        }
        // БД никакой нет, надо создать пароль, потом после выхода из проги зашифровать бд.
        _password = await DbAuthorizationRunner.CreateFormAsync("База данных не создана. Придумайте ключ базы данных");
        DbAuthorizationRunner.CloseWindow();

        return true;
    }

    public void DbLogout() //Завершение приложения
    {

    }

    public static bool EncryptedDbExists(string EncryptedDbPath)
        => File.Exists(EncryptedDbPath);



    public static class DbAuthorizationRunner
    {
        static MainWindow mainWindow = null!;

        [STAThread]
        public static async Task<string> CreateFormAsync(string label)
        {
            Thread wpfThread = new Thread(() =>
            {
                mainWindow = new MainWindow(label);
                mainWindow.Show();

                Dispatcher.Run();
            });

            wpfThread.SetApartmentState(ApartmentState.STA);
            wpfThread.Start();

            return await GetPasswordAsync();
        }

        private static async Task<string> GetPasswordAsync()
        {
            string password = null!;
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    mainWindow.Dispatcher.Invoke(() =>
                    {
                        if (mainWindow.HasPasswordEntered)
                        {
                            password = mainWindow.Password;
                        }
                    });

                    if (password != null)
                        break;
                }
            });
            return password;
        }

        public static void CloseWindow()
        {
            mainWindow.Dispatcher.Invoke(mainWindow.Close);
        }

        public static void SetLabel(string label)
        {
            mainWindow.Dispatcher.Invoke(() =>
            {
                mainWindow.Label = label;
            });
        }
    }
}

