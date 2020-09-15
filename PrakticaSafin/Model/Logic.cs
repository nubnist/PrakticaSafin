/*Практическая работа по профессиональному модулю МДК 03.01 "Технология разработки программного обеспечения"
* Название: Аренда автомобиля
* Разработал: Панфилов Илия Олегович, группа ТМП-61.
* Дата и номер версии: 15.04.2020 
* Язык: C#
* Краткое описание:
* Данная программа является информационной системой аренды автомобилей.
* Задание:
* 1. Создание базы данных для информационной системы аренды автомобилей
* 2. Создание пользовательских форм
* 3. Создание формы договора аренды
* 4. Создание формы авторизации и регистрации
**********************************************************************
* Logic - основная логика программы;
* MainWindow - форма регистрации и авторизации пользователя;
* User - форма составления договора аренды и заполения данных о пользователе.
* Admin - форма редактирования таблиц БД.
*/


using System.Collections.Generic;
using System.Data.SqlClient;
using PrakticaSafin.Data;
using System.Text.RegularExpressions;

namespace PrakticaSafin
{
    public class Logic
    {
        #region Колекции хранящие даные из БД
        public List<Renter> renters; // Арендаторы
        public List<Car> cars;       // Автомобили
        public List<ContractRent> contractRents; // Договоры аренды
        public List<Colors> colors; // Цвета
        public List<Lights> lights; // Фары
        public List<Accounts> accounts; // Аккаунты
        #endregion

        #region Строка подключения
        private string connectionString =
            @"Data Source=.\SQLEXPRESS;
            Initial Catalog=RentCar;
            Integrated Security=True";
        #endregion


        public SelectTable selectTable; // Выбранная таблица

        // Конструктор Logic
        public Logic()
        {
            renters = new List<Renter>();
            cars = new List<Car>();
            contractRents = new List<ContractRent>();
            colors = new List<Colors>();
            lights = new List<Lights>();
            accounts = new List<Accounts>();
            //double b = Convert.ToDouble("232");

            GetterFromBase(); // Заполнение коллекций значениями из базы данных
        }

        // Запись новых значений в базу
        public void SetToBase(SelectTable selectTable)
        {
            SqlCommand command;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<string> nameTables = new List<string> { "Car", "Color", "ContractRect", "Light", "Renter" };
                switch (selectTable)
                {
                    case SelectTable.Cars:
                        command = new SqlCommand($"DELETE FROM Car", connection);
                        command.ExecuteNonQuery();
                        foreach (var item in cars)
                        {
                            command = new SqlCommand(
                                $@"INSERT INTO Car
                                (number, brand, body, privod, peredacha, motor, light,
                                climate, color, dorsCount, placeCount, obivka) VALUES 
                                ({item.Number}, '{item.Brand}', '{item.Body}', '{item.Privod}', '{item.Peredacha}',
                                '{item.Motor}', '{item.Light}', '{item.Climate}', '{item.Color}',
                                {item.DorsCount}, {item.PlaceCount}, '{item.Obivka}')"
                                , connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    case SelectTable.Renters:
                        command = new SqlCommand($"DELETE FROM Renter", connection);
                        command.ExecuteNonQuery();
                        foreach (var item in renters)
                        {
                            command = new SqlCommand(
                                $@"INSERT INTO Renter (id, lastName, firstName, middleName, adres, phone)
                                VALUES ({item.Id}, '{item.LastName}', '{item.FirstName}', '{item.MiddleName}',
                                '{item.Address}', '{item.Phone}')", connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    case SelectTable.ContractRent:
                        command = new SqlCommand($"DELETE FROM ContractRent", connection);
                        command.ExecuteNonQuery().ToString();
                        foreach (var item in contractRents)
                        {
                            command = new SqlCommand(
                                $@"INSERT INTO ContractRent (renter, car, cost, startRent, finish, carparkNumber, passwordPark) VALUES
                                ('{item.Renter}', '{item.Car}', '{item.Cost}', '{item.StartRent.ToString()}', '{item.FinishRent.ToString()}',
                                '{item.CarparklNumber}', '{item.PasswordPark}')", connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    case SelectTable.Colors:
                        command = new SqlCommand($"DELETE FROM Color", connection);
                        command.ExecuteNonQuery().ToString();
                        foreach (var item in colors)
                        {
                            command = new SqlCommand(
                                $@"INSERT INTO Color (color) VALUES ('{item.Color}')", connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    case SelectTable.Lights:
                        command = new SqlCommand($"DELETE FROM Light", connection);
                        command.ExecuteNonQuery();
                        foreach (var item in lights)
                        {
                            command = new SqlCommand(
                                $@"INSERT INTO Light (light) VALUES ('{item.Light}')", connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        // Заполняет коллекции значениями из базы данных
        public void GetterFromBase()
        {
            SqlCommand command; // Для команд к базе данных
            SqlDataReader reader; // Для считывания в него данных и базы данных
            // Заполнение арендаторами
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Заполнение арендаторами
                connection.Open();
                command = new SqlCommand("SELECT * FROM Renter", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    renters.Add(
                        new Renter
                        {
                            Id = reader.GetInt32(0),
                            LastName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            MiddleName = reader.GetString(3),
                            Address = reader.GetString(4),
                            Phone = reader.GetString(5)
                        });
                }
                reader.Close();

                // Заполнение цветами
                command = new SqlCommand("SELECT * FROM Color", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    colors.Add(new Colors { Color = reader.GetString(0) });
                }
                reader.Close();

                // Заполнение фарами
                command = new SqlCommand("SELECT * FROM Light", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lights.Add(new Lights { Light = reader.GetString(0) });
                }
                reader.Close();

                // Заполнение автомобилями
                command = new SqlCommand("SELECT * FROM Car", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cars.Add(
                        new Car
                        {
                            Number = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Body = reader.GetString(2),
                            Privod = reader.GetString(3),
                            Peredacha = reader.GetString(4),
                            Motor = reader.GetString(5),
                            Light = reader.GetString(6),
                            Climate = reader.GetString(7),
                            Color = reader.GetString(8),
                            DorsCount = reader.GetInt32(9),
                            PlaceCount = reader.GetInt32(10),
                            Obivka = reader.GetString(11)
                        });
                }
                reader.Close();

                // Заполнение контрактами
                command = new SqlCommand("SELECT * FROM ContractRent", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contractRents.Add(
                        new ContractRent
                        {
                            Renter = reader.GetInt32(0),
                            Car = reader.GetInt32(1),
                            Cost = reader.GetDouble(2),
                            StartRent = reader.GetDateTime(3),
                            FinishRent = reader.GetDateTime(4),
                            CarparklNumber = reader.GetInt32(5),
                            PasswordPark = reader.GetString(6),
                        });
                }
                reader.Close();

                // Заполнение аккаунтами
                command = new SqlCommand("SELECT * FROM Account", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accounts.Add(new Accounts
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2)
                    });
                }
                reader.Close();
            }
        }

        // Проверка наличия аккаунта в базе
        public bool CheckAccount(string login, string password)
        {

            foreach (var item in accounts)
            {
                if (item.Login == login && item.Password == password)
                    return true;
            }
            return false;
        }

        // Поиск Id аккаунта
        public int FindId(string login)
        {
            int id = 0;
            foreach (var item in accounts)
            {
                if (item.Login == login)
                    id = item.Id;
            }
            return id;
        }

        // Регистрация новго пользователя
        public void RegisterNewUser(string login, string password)
        {
            SqlCommand command;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand($@"INSERT INTO Account (login, password) VALUES ('{login}', '{password}')", connection);
                command.ExecuteNonQuery();
            }
            GetterFromBase();
        }

        // Проверка пароля на корректность
        public bool CheckPassword(string password)
        {
            bool result = false;
            if (password.Length > 6
                && password.StartsWith("14")
                && (Regex.IsMatch(password, @"[А-Я]")
                || Regex.IsMatch(password, @"[A-Z]"))
                && Regex.Matches(password, @"[!@#$%^]").Count > 1)
            {
                result = true;
            }

            return result;
        }

    }
}
