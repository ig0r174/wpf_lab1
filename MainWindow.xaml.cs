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
            txtNumber.TextChanged += TextChanged;
            AllNumbers.Children.Add(txtNumber);

            Button btn = new Button();
            btn.Content = "Delete";
            btn.Click += btn_Click;
            AllButtons.Children.Add(btn);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextChanged(object sender, EventArgs e)
        {
            double sum = 0;
            foreach (var item in AllNumbers.Children)
            {
                if ( item is TextBox )
                {
                    double number = 0;
                    var tb = (TextBox)item;
                    if (double.TryParse(tb.Text, out number) && tb.Text.Length > 0 )
                        sum += number;
                    else MessageBox.Show("It is not a number");
                }
            }
            ResultLabel.Content = "Результат: " + sum;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllNumbers.Children.RemoveAt(AllNumbers.Children.Count - 1);
            AllButtons.Children.RemoveAt(AllButtons.Children.Count - 1);
           // TextChanged(false);
        }
    }
}
