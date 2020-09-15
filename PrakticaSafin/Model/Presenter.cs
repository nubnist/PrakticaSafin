using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using PrakticaSafin.Data;

namespace PrakticaSafin
{
    public class Presenter
    {
        private Admin admin = null;
        private Logic logic = null;
        

        public Presenter(Admin admin)
        {
            this.admin = admin;
            logic = new Logic();

            admin.tablesList_SelectionChangedEvent += Admin_tablesList_SelectionChangedEvent;
            admin.save_ClickEvent += Admin_save_ClickEvent;
        }

        private void Admin_save_ClickEvent(object sender, EventArgs e)
        {
            logic.SetToBase(logic.selectTable);
        }

        // Заполнение таблиц значениями из коллекций
        private void Admin_tablesList_SelectionChangedEvent(object sender, EventArgs e)
        {
            admin.lbl1.Visibility = Visibility.Collapsed;
            logic.selectTable = (SelectTable)(sender as ComboBox).SelectedIndex;
            switch (logic.selectTable)
            {
                case SelectTable.Cars:
                    admin.dataGrid.ItemsSource = logic.cars;
                    admin.dataGrid.Columns[0].Header = "Номер";
                    admin.dataGrid.Columns[1].Header = "Марка";
                    admin.dataGrid.Columns[2].Header = "Кузов";
                    admin.dataGrid.Columns[3].Header = "Привод";
                    admin.dataGrid.Columns[4].Header = "Передача";
                    admin.dataGrid.Columns[5].Header = "Мотор";
                    admin.dataGrid.Columns[6].Header = "Фары";
                    admin.dataGrid.Columns[7].Header = "Климат-контролль";
                    admin.dataGrid.Columns[8].Header = "Цвет";
                    admin.dataGrid.Columns[9].Header = "Количество дверей";
                    admin.dataGrid.Columns[10].Header = "Количество мест";
                    admin.dataGrid.Columns[11].Header = "Обивка салона";
                    break;
                case SelectTable.Renters:
                    admin.dataGrid.ItemsSource = logic.renters;
                    admin.dataGrid.Columns[0].Header = "Id";
                    admin.dataGrid.Columns[1].Header = "Фамилия";
                    admin.dataGrid.Columns[2].Header = "Имя";
                    admin.dataGrid.Columns[3].Header = "Отчество";
                    admin.dataGrid.Columns[4].Header = "Адрес";
                    admin.dataGrid.Columns[5].Header = "Телефон";
                    break;
                case SelectTable.ContractRent:
                    admin.dataGrid.ItemsSource = logic.contractRents;
                    admin.dataGrid.Columns[0].Header = "Арендатор (Id)";
                    admin.dataGrid.Columns[1].Header = "Машина №";
                    admin.dataGrid.Columns[2].Header = "Стоимость";
                    admin.dataGrid.Columns[3].Header = "Начало аренды";
                    admin.dataGrid.Columns[4].Header = "Конец аренды";
                    admin.dataGrid.Columns[5].Header = "Нормер парковки";
                    admin.dataGrid.Columns[6].Header = "Пароль парковки";
                    break;
                case SelectTable.Colors:
                    admin.dataGrid.ItemsSource = logic.colors;
                    admin.dataGrid.Columns[0].Header = "Цвет машины";
                    break;
                case SelectTable.Lights:
                    admin.dataGrid.ItemsSource = logic.lights;
                    admin.dataGrid.Columns[0].Header = "Фары";
                    break;
            }
        }

        // Обновление даных в случае ошибки
        public void ReloadTables()
        {
            logic.renters.Clear();
            logic.cars.Clear();
            logic.contractRents.Clear();
            logic.colors.Clear();
            logic.lights.Clear();
            logic.GetterFromBase();
        }
    }
}
