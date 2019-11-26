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

namespace WPF_Form_With_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FormBL form = new FormBL();

        public MainWindow()
        {
            InitializeComponent();

            ExitButton.Click += new RoutedEventHandler(ExitButton_Click);
            FormMenuButton.Click += new RoutedEventHandler(FormMenuButton_Click);
            SearchInfoMenuButton.Click += new RoutedEventHandler(SearchInfoMenuButton_Click);
            SubmitButton.Click += new RoutedEventHandler(SubmitButton_Click);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs ex)
        {
            Application.Current.Shutdown();
        }

        private void FormMenuButton_Click(object sender, RoutedEventArgs ex)
        {
            FormPanel.Visibility = Visibility.Visible;
            SearchPanel.Visibility = Visibility.Collapsed;
            DataOutputPanel.Visibility = Visibility.Collapsed;
        }

        private void SearchInfoMenuButton_Click(object sender, RoutedEventArgs ex)
        {
            SearchPanel.Visibility = Visibility.Visible;
            FormPanel.Visibility = Visibility.Collapsed;
            DataOutputPanel.Visibility = Visibility.Collapsed;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs ex)
        {
            form.Username = FormTbx1.Text;
            form.InternationalTeam = FormTbx2.Text;
            form.ClubTeam = FormTbx3.Text;
            form.Player = FormTbx4.Text;

            if(!form.checkEmpty())
            {
                String returnValue = form.checkUsername();
                if(returnValue.Equals("username does not exist"))
                {
                    form.insertData();
                    Alert.Text = "Data Stored Successfully";
                    Alert.Foreground = new SolidColorBrush(Color.FromRgb(0, 153, 0));
                    Alert.Visibility = Visibility.Visible;

                    FormTbx1.Text = "";
                    FormTbx2.Text = "";
                    FormTbx3.Text = "";
                    FormTbx4.Text = "";
                }
                else if(returnValue.Equals("username exists"))
                {
                    Alert.Text = "Username Already Exists";
                    Alert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                    Alert.Visibility = Visibility.Visible;
                }
                else if(returnValue.Equals("exception"))
                {
                    Alert.Text = "Connection Error! Please Try Again Later";
                    Alert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                    Alert.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Alert.Text = "Fill Up All The Fields";
                Alert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                Alert.Visibility = Visibility.Visible;
            }

        }

    }
}
