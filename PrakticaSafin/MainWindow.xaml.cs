using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PrakticaSafin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logic logic;
        public MainWindow()
        {
            InitializeComponent();
            logic = new Logic();
        }

        #region Визуальные настройки
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void closeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            loginBorder.Background = Brushes.Red;
            passwodBorder.Background = Brushes.White;
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwodBorder.Background = Brushes.Red;
            loginBorder.Background = Brushes.White;
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(login.Text))
                loginLBL.Visibility = Visibility.Collapsed;
            else
                loginLBL.Visibility = Visibility.Visible;
        }
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password))
                passwordLBL.Visibility = Visibility.Collapsed;
            else
                passwordLBL.Visibility = Visibility.Visible;

            if (logic.CheckPassword(password.Password))
            {
                passwordStatus.Visibility = Visibility.Hidden;
                register.Visibility = Visibility.Visible;
                aitorization.Visibility = Visibility.Visible;
            }
            else
            {
                passwordStatus.Visibility = Visibility.Visible;
                register.Visibility = Visibility.Hidden;
                aitorization.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        // Регистрация нового пользователя
        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (!logic.CheckAccount(login.Text, password.Password))
            {
                logic.RegisterNewUser(login.Text, password.Password);
                MessageBox.Show("Аккаунт успешно зарегестрирован, вы можете войти в него!");
            }
            else
                MessageBox.Show("Аккаунт либо существует, либо неверный формат пароля, вы не можете его зарегестрировать!");
        }

        // Авторизация пользователя
        private void aitorization_Click(object sender, RoutedEventArgs e)
        {
            if (logic.CheckAccount(login.Text, password.Password))
            {
                new User(logic.FindId(login.Text)).Show();
                this.Close();
            }
            else
                MessageBox.Show("Неверный логин или пароль!");
        }

        private void superUser_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            this.Close();
        }
    }
}
