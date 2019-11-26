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
        private String username = "";
        private String internationalTeam = "";
        private String clubTeam = "";
        private String player = "";

        public String Username
        {
            set
            {
                username = value;
            }
        }

        public String InternationalTeam
        {
            set
            {
                internationalTeam = value;
            }
        }

        public String ClubTeam
        {
            set
            {
                clubTeam = value;
            }
        }

        public String Player
        {
            set
            {
                player = value;
            }
        }

        public bool checkEmpty()
        {
            if (username.Equals("") || internationalTeam.Equals("") || clubTeam.Equals("") || player.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String checkUsername()
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

        public void insertData()
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "INSERT INTO myfootballinfo (username, internationalteam, clubteam, player) values ('" + username + "','" + internationalTeam + "','" + clubTeam + "','" + player + "');";

            try
            {
                using (MySqlCommand mySqlCommand=new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " Insert Data Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
