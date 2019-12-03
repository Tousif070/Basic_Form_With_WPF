using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace WPF_Form_With_Database
{
    class FormBL
    {
        public bool checkEmpty(User user)
        {
            if (user.Username.Equals("") || user.InternationalTeam.Equals("") || user.ClubTeam.Equals("") || user.Player.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String checkUsername(String username)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "SELECT * FROM myfootballinfo WHERE username='" + username + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    if(mySqlCommand.ExecuteReader().HasRows)
                    {
                        return "username exists";
                    }
                    else
                    {
                        return "username does not exist";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " Check Username Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "exception";
            }
        }

        public void insertData(User user)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "INSERT INTO myfootballinfo (username, internationalteam, clubteam, player) values ('" + user.Username + "','" + user.InternationalTeam + "','" + user.ClubTeam + "','" + user.Player + "');";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " Insert Data Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<User> searchName(String name)
        {
            List<User> userList = new List<User>();
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "SELECT id, username, player FROM myfootballinfo WHERE username REGEXP '^" + name + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    while(dataReader.Read())
                    {
                        User user = new User();
                        user.ID = dataReader.GetInt32(0);
                        user.Username = dataReader.GetString(1);
                        user.Player = dataReader.GetString(2);
                        userList.Add(user);
                    }
                    return userList;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " Search Name Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public User getAllInfo(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "SELECT id, username, internationalteam, clubteam, player FROM myfootballinfo WHERE id=" + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    User user = new User();
                    if(dataReader.Read())
                    {
                        user.ID = dataReader.GetInt32(0);
                        user.Username = dataReader.GetString(1);
                        user.InternationalTeam = dataReader.GetString(2);
                        user.ClubTeam = dataReader.GetString(3);
                        user.Player = dataReader.GetString(4);
                    }
                    return user;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " Get All Info Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

    }
}
