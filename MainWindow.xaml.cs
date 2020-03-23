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
            
            TextBox txtNumber = new TextBox()
            {
                Height = 22,
                Margin = new Thickness(0, 0, 0, 10),
                Name = "tb" + AllNumbers.Children.Count
             };

            txtNumber.TextChanged += SetHandler;
            AllNumbers.Children.Add(txtNumber);

            Button btn = new Button()
            {
                Content = "Delete",
                Height = 22,
                Margin = new Thickness(0, 0, 0, 10),
                Name = "btn" + AllButtons.Children.Count
            };

            btn.Click += Btn_Click;
            AllButtons.Children.Add(btn);

            ComboBox combobox = new ComboBox()
            {
                Height = 22,
                Margin = new Thickness(0, 0, 0, 10),
                Name = "cb" + AllComboBoxes.Children.Count
            };

            foreach (var action in new char[4] {'+', '-', '/', '*'})
            {
                combobox.Items.Add(action);
            }

            combobox.SelectedIndex = 0;
            combobox.SelectionChanged += SetHandler;
            AllComboBoxes.Children.Add(combobox);
        }

        private void SetHandler(object sender, SelectionChangedEventArgs e)
        {
            UpdateResult();
        }

        private void SetHandler(object sender, TextChangedEventArgs e)
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
                    var tb = (TextBox)item;
                    if (!decimal.TryParse(tb.Text, out outDecimal)) continue;

                    double value = Convert.ToDouble(tb.Text);

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
