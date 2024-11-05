using Microsoft.IdentityModel.Tokens;
using System.Windows;
using DAL = Library.DataAccessLayer;


namespace PromocodeApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PromocodeCountLabel.Content = DAL.GetCountActivePromocodes();
        }

        private void ActivationPromocodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (PromocodeTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Поле ввода не должно быть пустым");
                return;
            }
                

            if (DAL.IsPromocodeExist(PromocodeTextBox.Text))
            {
                DAL.PromocodeActivate(PromocodeTextBox.Text);
                PromocodeCountLabel.Content = DAL.GetCountActivePromocodes();
                MessageBox.Show("Промокод успешно активирован!");
            }
            else
            {
                MessageBox.Show("Промокод не найден или уже был активирован!");
            }

        }
    }
}