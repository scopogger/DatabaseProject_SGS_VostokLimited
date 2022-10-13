using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;

namespace WPFworkSchedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[,] arrComboFill = new string[,] {
                { "Москва", "ц. М-А", "Анатолий" }, { "Москва", "ц. М-Г", "Фёдор" },
                { "Москва", "ц. М-Б", "Александр" }, { "Москва", "ц. М-Г", "Лев" },
                { "Москва", "ц. М-В", "София" }, { "Москва", "ц. М-ДЕ", "Ульяна" },
                { "Санкт-Петербург", "ц. СП-А", "Мария" }, { "Санкт-Петербург", "ц. СП-В", "Вера" },
                { "Санкт-Петербург", "ц. СП-А", "Максим" }, { "Санкт-Петербург", "ц. СП-Г", "Николай" },
                { "Санкт-Петербург", "ц. СП-Б", "Михаил" }, { "Санкт-Петербург", "ц. СП-Д", "Владислав" },
                { "Санкт-Петербург", "ц. СП-В", "Артём" }, { "Санкт-Петербург", "ц. СП-Ц", "Арсений" },
                { "Новосибирск", "ц. Н-А", "Даниил" }, { "Новосибирск", "ц. Н-А", "Тимур" },
                { "Новосибирск", "ц. Н-А", "Анна" }, { "Новосибирск", "ц. Н-Б", "Алёна" },
                { "Новосибирск", "ц. Н-А", "Иван" }, { "Новосибирск", "ц. Н-В", "Мирон" },
                { "Омск", "ц. О-А", "Александра" }, { "Омск", "ц. О-П", "Юлия" },
                { "Омск", "ц. О-Б", "Дарья" }, { "Омск", "ц. О-П", "Диана" },
                { "Омск", "ц. О-В", "Данил" }, { "Омск", "ц. О-Я0", "Виктор" },
                { "Омск", "ц. О-Г", "Екатерина" }, { "Омск", "ц. О-Я1", "Олег" },
                { "Омск", "ц. О-Д", "Матвей" }, { "Омск", "ц. О-Я2", "Богдан" },
                { "Самара", "ц. С-А", "Ксения" }, { "Самара", "ц. С-Б", "Мирослава" },
                { "Самара", "ц. С-Б", "Арина" }, { "Самара", "ц. С-Ж", "Демид" },
                { "Пермь", "ц. П-А", "Егор" }, { "Пермь", "ц. П-ГД", "Есения" },
                { "Пермь", "ц. П-А", "Ева" }, { "Пермь", "ц. П-Е", "Антон" },
                { "Пермь", "ц. П-Б", "Илья" }, { "Пермь", "ц. П-У", "Злата" },
                { "Пермь", "ц. П-В", "Тимофей" }, { "Пермь", "ц. П-С", "Майя" },
                { "Пермь", "ц. П-В", "Василиса" }, { "Пермь", "ц. П-Т", "Ника" }
            };

        public MainWindow()
        {
            InitializeComponent();
            List<string> citList = new List<string>();

            for (int row = 0; row < arrComboFill.GetLength(0); ++row)
            {
                citList.Add(arrComboFill[row, 0]);
                citList = citList.Distinct().ToList();
            }

            ObservableCollection<string> oCitList;
            oCitList = new System.Collections.ObjectModel.ObservableCollection<string>(citList);
            cbCity.ItemsSource = oCitList;

            cbShift.Items.Add("Первая смена");
            cbShift.Items.Add("Вторая смена");
            cbShift.Items.Add("Третья смена");
            cbShift.Items.Add("Четвёртая смена");

            cbDepartment.IsEnabled = false;
            cbWorker.IsEnabled = false;
            rbFirstGroup.IsEnabled = false;
            rbSecondGroup.IsEnabled = false;
            cbShift.IsEnabled = false;
            btSave.IsEnabled = false;
        }


        /// <summary>
        /// SAVE JSON EVENT
        /// </summary>
        public class WorkerJson
        {
            public string City { get; set; }
            public string Department { get; set; }
            public string Worker { get; set; }
            public string Group { get; set; }
            public string Shift { get; set; }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("Город");
                writer.WriteValue(cbCity.SelectedItem.ToString());
                writer.WritePropertyName("Цех");
                writer.WriteValue(cbDepartment.SelectedItem.ToString());
                writer.WritePropertyName("Сотрудник");
                writer.WriteValue(cbWorker.SelectedItem.ToString());
                writer.WritePropertyName("Бригада");
                writer.WriteValue(rbFirstGroup.IsChecked == true ? "Дневная бригада (с 8:00 до 20:00)" : "Ночная бригада (с 20:00 до 8:00)");
                writer.WritePropertyName("Смена");
                writer.WriteValue(cbShift.SelectedItem.ToString());
                writer.WriteEndObject();
            }

            string stringJson = sb.ToString();

            //MessageBox.Show(stringJson, "Saved!");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Запись рабочей смены";
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "Json files (.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, stringJson);
        }


        /// <summary>
        /// COMBOBOX VALUE EVENTS (RESET/PROCEED)
        /// </summary>
        /// 
        string prevCity = "";
        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCity.SelectedItem != prevCity)
            {
                cbDepartment.SelectedIndex = -1;
                cbWorker.SelectedIndex = -1;
                cbShift.SelectedIndex = -1;
                cbDepartment.IsEnabled = true;
                cbWorker.IsEnabled = false;
                rbFirstGroup.IsEnabled = false;
                rbSecondGroup.IsEnabled = false;
                cbShift.IsEnabled = false;
                btSave.IsEnabled = false;


                List<string> depList = new List<string>();

                for (int row = 0; row < arrComboFill.GetLength(0); ++row)
                {
                    if (arrComboFill[row, 0] == cbCity.SelectedItem)
                    {
                        depList.Add(arrComboFill[row, 1]);
                        depList = depList.Distinct().ToList();
                    }
                }

                ObservableCollection<string> oDepList;
                oDepList = new System.Collections.ObjectModel.ObservableCollection<string>(depList);
                cbDepartment.ItemsSource = oDepList;
            }

            if (cbCity.SelectedItem != null)
                prevCity = cbCity.SelectedItem.ToString();
        }

        string prevDep = "";
        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDepartment.SelectedItem != prevDep)
            {
                cbWorker.SelectedIndex = -1;
                cbShift.SelectedIndex = -1;
                cbWorker.IsEnabled = true;
                rbFirstGroup.IsEnabled = false;
                rbSecondGroup.IsEnabled = false;
                cbShift.IsEnabled = false;
                btSave.IsEnabled = false;


                List<string> workerList = new List<string>();

                for (int row = 0; row < arrComboFill.GetLength(0); ++row)
                {
                    if (arrComboFill[row, 1] == cbDepartment.SelectedItem)
                    {
                        workerList.Add(arrComboFill[row, 2]);
                        workerList = workerList.Distinct().ToList();
                    }
                }

                ObservableCollection<string> oWorkList;
                oWorkList = new System.Collections.ObjectModel.ObservableCollection<string>(workerList);
                cbWorker.ItemsSource = oWorkList;
            }

            if (cbDepartment.SelectedItem != null)
                prevDep = cbDepartment.SelectedItem.ToString();
        }

        string prevWorker = "";
        private void cbWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWorker.SelectedItem != prevWorker)
            {
                rbFirstGroup.IsEnabled = true;
                rbSecondGroup.IsEnabled = true;
                cbShift.SelectedIndex = -1;
            }

            if (cbWorker.SelectedItem != null)
                prevWorker = cbWorker.SelectedItem.ToString();

            cbShift.IsEnabled = true;
        }

        private void rbFirstGroup_Click(object sender, RoutedEventArgs e)
        {
            //cbShift.IsEnabled = true;
        }

        private void rbSecondGroup_Click(object sender, RoutedEventArgs e)
        {
            //cbShift.IsEnabled = true;
        }

        private void cbShift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbShift != null)
                btSave.IsEnabled = true;
        }
    }
}
