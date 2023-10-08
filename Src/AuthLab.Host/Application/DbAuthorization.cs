using AuthLab.DbAuthorization;
using AuthLab.DbEncryption.Concrete;
using System.Windows.Threading;

namespace AuthLab.Host.Application;

public class DbAuthorization
{
    private DbEncoder _dbEncoder;
    private string _encryptedDbPath;
    private string _password = null!;

    /// <summary>
    /// Db authorization
    /// </summary>
    /// <param name="encryptedDbPath">Зашифрованная бд, используется только для расшифрования</param>
    /// <param name="tempDbPath">Путь на бд, которая хранится временно, и которая впоследствии (при выходе из проги) будет шифроваться</param>
    public DbAuthorization(string encryptedDbPath, string tempDbPath)
    {
        _encryptedDbPath = encryptedDbPath;
        _dbEncoder = new DbEncoder(encryptedDbPath, tempDbPath);
    }

    public async Task DbLogin() //Запуск приложения
    {
        if (EncryptedDbExists(_encryptedDbPath)) // Существует зашифрованная бд, пытаемся расшифровать
        {
            var enteredPassword = await DbAuthorizationRunner.CreateFormAndGetPasswordAsync("Введите ключ базы данных");
            while (true) 
            {
                if (_dbEncoder.DecryptDatabase(enteredPassword))
                {
                    _password = enteredPassword;
                    DbAuthorizationRunner.CloseWindow();
                    break;
                }
                enteredPassword = await DbAuthorizationRunner.CreateFormAndGetPasswordAsync("Ключ базы данных неверный, введите снова");
            }
            return;
        }
        // БД никакой нет, надо создать пароль, потом после выхода из проги зашифровать бд.
        _password = await DbAuthorizationRunner.CreateFormAndGetPasswordAsync("База данных не создана. Придумайте ключ базы данных");
        DbAuthorizationRunner.CloseWindow();
    }

    public void DbLogout() //Завершение приложения
    {
        _dbEncoder.EncryptDatabase(_password);
    }

    public static bool EncryptedDbExists(string EncryptedDbPath)
        => File.Exists(EncryptedDbPath);



    #region WpfRunner

    public static class DbAuthorizationRunner
    {
        static MainWindow? mainWindow = null;

        [STAThread]
        public static async Task<string> CreateFormAndGetPasswordAsync(string label)
        {
            if (mainWindow == null)
            {
                Thread wpfThread = new Thread(() =>
                {
                    mainWindow = new MainWindow(label);
                    mainWindow.Show();

                    Dispatcher.Run();
                });

                wpfThread.SetApartmentState(ApartmentState.STA);
                wpfThread.Start();
            }
            else
                SetLabel(label);

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
                    mainWindow!.Dispatcher.Invoke(() =>
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
            mainWindow?.Dispatcher.Invoke(mainWindow.Close);
            mainWindow = null;
        }

        public static void SetLabel(string label)
        {
            mainWindow?.Dispatcher.Invoke(() =>
            {
                mainWindow.Label = label;
            });
        }
    }

    #endregion
}

