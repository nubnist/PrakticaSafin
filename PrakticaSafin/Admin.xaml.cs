using System;
using System.Windows;
using System.Windows.Controls;

namespace PrakticaSafin
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Admin : Window
    {
        Presenter presenter;
        public Admin()
        {
            InitializeComponent();
            presenter = new Presenter(this);
        }

       
        public event EventHandler save_ClickEvent = null;
        // Обновление таблицы в БД
        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                save_ClickEvent(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nПроверьте корректность введенных данных", "ОШИБКА!!!");
                presenter.ReloadTables();
                dataGrid.ItemsSource = null;
                tablesList.SelectedIndex = -1;
                lbl1.Visibility = Visibility.Visible;
            }
        }

        public event EventHandler tablesList_SelectionChangedEvent = null;
        // Выбор другой таблицы
        private void tablesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tablesList_SelectionChangedEvent(sender, e);
        }
    }
}
