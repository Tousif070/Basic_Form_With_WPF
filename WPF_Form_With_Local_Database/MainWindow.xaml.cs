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
            SearchButton.Click += new RoutedEventHandler(SearchButton_Click);
            GoBackButton.Click += new RoutedEventHandler(GoBackButton_Click);
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
            User user = new User();
            user.Username = FormTbx1.Text;
            user.InternationalTeam = FormTbx2.Text;
            user.ClubTeam = FormTbx3.Text;
            user.Player = FormTbx4.Text;

            if(!form.checkEmpty(user))
            {
                String returnValue = form.checkUsername(user.Username);
                if(returnValue.Equals("username does not exist"))
                {
                    form.insertData(user);
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

        private void SearchButton_Click(object sender, RoutedEventArgs ex)
        {
            List<User> userList = new List<User>();
            User user = new User();
            user.Username = SearchTbx.Text;

            if(!user.Username.Equals(""))
            {
                userList = form.searchName(user.Username);

                if(userList != null)
                {
                    if(userList.Count != 0)
                    {
                        SecondAlert.Text = "Results Found";
                        SecondAlert.Foreground = new SolidColorBrush(Color.FromRgb(0, 153, 0));
                        SecondAlert.Visibility = Visibility.Visible;
                        UserDG.ItemsSource = userList;
                        UserDG.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        SecondAlert.Text = "No Result Found";
                        SecondAlert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                        SecondAlert.Visibility = Visibility.Visible;
                        UserDG.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    SecondAlert.Text = "Connection Error! Please Try Again Later";
                    SecondAlert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                    SecondAlert.Visibility = Visibility.Visible;
                    UserDG.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                SecondAlert.Text = "The Search Field Is Empty";
                SecondAlert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                SecondAlert.Visibility = Visibility.Visible;
                UserDG.Visibility = Visibility.Collapsed;
            }

        }

        private void SeeMoreInfo(object sender, RoutedEventArgs ex)
        {
            String objString = ex.Source.ToString();
            String[] portion = objString.Split(' ');
            User user = new User();
            user.ID = Int32.Parse(portion[1]);
            user = form.getAllInfo(user.ID);

            if(user != null)
            {
                FITbx1.Text = user.ID.ToString();
                FITbx2.Text = user.Username;
                FITbx3.Text = user.InternationalTeam;
                FITbx4.Text = user.ClubTeam;
                FITbx5.Text = user.Player;
                DataOutputPanel.Visibility = Visibility.Visible;
            }
            else
            {
                SecondAlert.Text = "Connection Error! Please Try Again Later";
                SecondAlert.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                SecondAlert.Visibility = Visibility.Visible;
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs ex)
        {
            DataOutputPanel.Visibility = Visibility.Collapsed;
        }

    }
}
