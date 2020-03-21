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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int number = AllNumbers.Children.Count;
            TextBox txtNumber = new TextBox();
            txtNumber.Height = 22;
            txtNumber.Margin = new Thickness(0, 0, 0, 10);
            txtNumber.TextChanged += DataUpdated;
            txtNumber.Name = "tb" + AllNumbers.Children.Count;
            AllNumbers.Children.Add(txtNumber);

            Button btn = new Button();
            btn.Content = "Delete";
            btn.Click += Btn_Click;
            btn.Height = 22;
            btn.Margin = new Thickness(0, 0, 0, 10);
            btn.Name = "btn" + AllButtons.Children.Count;
            AllButtons.Children.Add(btn);

            ComboBox combobox = new ComboBox();

            foreach (var action in new char[4] {'+', '-', '/', '*'})
            {
                combobox.Items.Add(action);
            }

            combobox.SelectedIndex = 0;
            combobox.Height = 22;
            combobox.Margin = new Thickness(0, 0, 0, 10);
            combobox.Name = "cb" + AllComboBoxes.Children.Count;
            combobox.SelectionChanged += DataUpdated;

            AllComboBoxes.Children.Add(combobox);
        }

        private void DataUpdated(object sender, SelectionChangedEventArgs e)
        {
            UpdateResult();
        }

        private void DataUpdated(object sender, TextChangedEventArgs e)
        {
            UpdateResult();
        }

        private void UpdateResult()
        {
            double result = 0;
            decimal outDecimal;
            foreach (var item in AllNumbers.Children)
            {
                if (item is TextBox)
                {
                    double number;
                    var tb = (TextBox)item;
                    double value = Convert.ToDouble(tb.Text);

                    if (!decimal.TryParse(tb.Text, out outDecimal)) continue;

                    int id = Convert.ToInt32(tb.Name.Substring(2));
                    ComboBox cb = AllComboBoxes.Children[id] as ComboBox;

                    switch (cb.SelectedItem)
                    {
                        case '*':
                            result *= value;
                            break;
                        case '/':
                            result /= value;
                            break;
                        case '+':
                            result += value;
                            break;
                        case '-':
                            result -= value;
                            break;
                    }
                }
            }
            ResultLabel.Content = "Результат: " + result;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int deleteId = Convert.ToInt32(deleteButton.Name.Substring(3));
            AllButtons.Children.RemoveAt(deleteId);
            AllComboBoxes.Children.RemoveAt(deleteId);
            AllNumbers.Children.RemoveAt(deleteId);
            UpdateResult();
        }

        private void TextChanged(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllNumbers.Children.RemoveAt(AllNumbers.Children.Count - 1);
            AllButtons.Children.RemoveAt(AllButtons.Children.Count - 1);
            AllComboBoxes.Children.RemoveAt(AllComboBoxes.Children.Count - 1);
            UpdateResult();
        }
    }
}
