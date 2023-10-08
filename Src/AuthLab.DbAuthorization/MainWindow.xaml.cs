using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AuthLab.DbAuthorization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string label)
        {
            InitializeComponent();
            mainLabel.Content = label;
        }

        public string Label 
        { 
            get
            {
                return mainLabel.Content.ToString()!;
            }
            set
            {
                mainLabel.Content = value;
            }
        }

        public bool HasPasswordEntered { get; private set; }

        public string Password { get; private set; } = null!;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Пароль не может быть пустым!");
                return;
            }

            HasPasswordEntered = true;
            Password = passwordBox.Password;

            MessageBox.Show("Пароль сохранен");
        }
    }
}
