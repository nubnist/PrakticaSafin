using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PrakticaSafin
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        public int id;
        public Logic logic = new Logic();
        public List<int> carsNumbers = new List<int>();
        public List<double> costs = new List<double>();
        public int selectPark;

        public User(int id)
        {
            InitializeComponent();
            foreach (var item in logic.cars)
                carsNumbers.Add(item.Number);
            selectCar.ItemsSource = carsNumbers;

            costs.Add(10);
            costs.Add(50);
            costs.Add(10);
            costs.Add(400);
            costs.Add(1000);
            costSelect.ItemsSource = costs;

            this.id = id;
            SetInfoOfRenter();
            SearchPark();
        }


        // Заполенеие textBox`ов данными о арендаторе
        private void SetInfoOfRenter()
        {
            foreach (var item in logic.renters)
            {
                if (item.Id == id)
                {
                    lastName.Text = item.LastName;
                    FirstName.Text = item.FirstName;
                    middleName.Text = item.MiddleName;
                    address.Text = item.Address;
                    phone.Text = item.Phone;
                    saveRenter.Visibility = Visibility.Hidden;
                }

            }
        }

        // Сохранение в базе информации о арендаторе
        private void saveRenter_Click(object sender, RoutedEventArgs e)
        {
            logic.renters.Add(new Renter
            {
                Id = id,
                LastName = lastName.Text,
                FirstName = FirstName.Text,
                MiddleName = middleName.Text,
                Address = address.Text,
                Phone = phone.Text
            });
            logic.SetToBase(Data.SelectTable.Renters);
            saveRenter.Visibility = Visibility.Hidden;
        }

        // Сохранение в базе договора
        private void saveContract_Click(object sender, RoutedEventArgs e)
        {
            logic.contractRents.Add(new ContractRent
            {
                Renter = id,
                Car = Convert.ToInt32(selectCar.Text),
                Cost = Convert.ToDouble(costSelect.Text),
                StartRent = (DateTime)startRentCal.SelectedDate,
                FinishRent = (DateTime)finishRentCal.SelectedDate,
                CarparklNumber = Convert.ToInt32(parkNumBox.Text),
                PasswordPark = passwordPark.Text
            });
            logic.SetToBase(Data.SelectTable.ContractRent);
            saveContract.Visibility = Visibility.Hidden;
            parkNumBox.Visibility = Visibility.Hidden;
            SearchPark();

        }

        // Рассчет стоимости
        private void calcCost_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan span = (TimeSpan)(finishRentCal.SelectedDate - startRentCal.SelectedDate);
            fullCost.Content = Convert.ToDouble(costSelect.Text) * span.TotalDays;
        }

        // Поиск парковки
        public void SearchPark()
        {
            int i;
            for (i = 0; i < logic.contractRents.Count; i++)
            {
                if (id == logic.contractRents[i].Renter)
                {
                    selectCar.SelectedItem = logic.contractRents[i].Car;
                    costSelect.SelectedItem = logic.contractRents[i].Cost;
                    startRentCal.SelectedDate = logic.contractRents[i].StartRent;
                    finishRentCal.SelectedDate = logic.contractRents[i].FinishRent;
                    passwordPark.Text = logic.contractRents[i].PasswordPark;
                    selectCar.SelectedItem = logic.contractRents[i].Car;
                    costSelect.SelectedItem = logic.contractRents[i].Cost;

                    saveContract.Visibility = Visibility.Hidden;
                    switch (logic.contractRents[i].CarparklNumber)
                    {
                        case 1:
                            car1.Background = Brushes.Green;
                            car1.Foreground = Brushes.Black;
                            break;
                        case 2:
                            car2.Background = Brushes.Green;
                            car1.Foreground = Brushes.Black;
                            break;
                        case 3:
                            car3.Background = Brushes.Green;
                            car3.Foreground = Brushes.Black;
                            break;
                        case 4:
                            car4.Background = Brushes.Green;
                            car4.Foreground = Brushes.Black;
                            break;
                        case 5:
                            car5.Background = Brushes.Green;
                            car5.Foreground = Brushes.Black;
                            break;
                        case 6:
                            car6.Background = Brushes.Green;
                            car6.Foreground = Brushes.Black;
                            break;
                        case 7:
                            car7.Background = Brushes.Green;
                            car7.Foreground = Brushes.Black;
                            break;
                        case 8:
                            car8.Background = Brushes.Green;
                            car8.Foreground = Brushes.Black;
                            break;
                        case 9:
                            car9.Background = Brushes.Green;
                            car9.Foreground = Brushes.Black;
                            break;
                        case 10:
                            car10.Background = Brushes.Green;
                            car10.Foreground = Brushes.Black;
                            break;
                        case 11:
                            car11.Background = Brushes.Green;
                            car11.Foreground = Brushes.Black;
                            break;
                        case 12:
                            car12.Background = Brushes.Green;
                            car12.Foreground = Brushes.Black;
                            break;
                        case 13:
                            car13.Background = Brushes.Green;
                            car13.Foreground = Brushes.Black;
                            break;
                        case 14:
                            car14.Background = Brushes.Green;
                            car14.Foreground = Brushes.Black;
                            break;
                        case 15:
                            car15.Background = Brushes.Green;
                            car15.Foreground = Brushes.Black;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public List<Car> cars = new List<Car>();
        // Поиск машин
        private void search_Click(object sender, RoutedEventArgs e)
        {
            cars.Clear();
            if ((bool)color.IsChecked)
            {
                foreach (var item in logic.cars)
                    if (item.Color == searchCar.Text)
                        cars.Add(item);
            }
            else
                foreach (var item2 in logic.cars)
                    if (item2.Light == searchCar.Text)
                        cars.Add(item2);

            if (searchCar.Text == "")
            {
                dataGrid.ItemsSource = null;
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = cars;
        }

        #region Вспомогательные визуальные функции
        private void searchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            cars.Clear();
            if ((bool)color.IsChecked)
            {
                foreach (var item in logic.cars)
                    if (item.Color.StartsWith(searchCar.Text))
                        cars.Add(item);
            }
            else
                foreach (var item2 in logic.cars)
                    if (item2.Light.StartsWith(searchCar.Text))
                        cars.Add(item2);
            if (searchCar.Text == "")
            {
                dataGrid.ItemsSource = null;
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = cars;
        }

        private void color_Checked(object sender, RoutedEventArgs e)
        {
            cars.Clear();
            if ((bool)color.IsChecked)
            {
                foreach (var item in logic.cars)
                    if (item.Color.StartsWith(searchCar.Text))
                        cars.Add(item);
            }
            else
                foreach (var item2 in logic.cars)
                    if (item2.Light.StartsWith(searchCar.Text))
                        cars.Add(item2);
            if (searchCar.Text == "")
            {
                dataGrid.ItemsSource = null;
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = cars;
        }
        #endregion
    }
}
