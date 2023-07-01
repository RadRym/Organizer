using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2.TeklaModels.Views
{
    public class ListsOfObjectGroups
    {
        public static List<string> materials = new List<string>() 
        {
            "S235JR",
            "S275JR",
            "S355JR",
            "C8/10",
            "C12/15",
            "C16/20",
            "C20/25",
            "C25/30",
            "C30/37",
            "C35/45",
            "C40/50",
            "C45/55",
            "C50/60",
        };

        public static List<string> profiles()
        {
            List<string> profiles = new List<string>();
            string connectionString = "Server=localhost;Port=3306;Database=steeldb;Uid=root;Pwd=Zbyszek.45;";
            string query = "SELECT Name FROM steeldb.iprofiles WHERE NOT (" +
                "Name LIKE '%IPEAA%' OR " +
                "Name LIKE '%IPEA%' OR " +
                "Name LIKE '%AA' OR " +
                "Name LIKE '%C') AND " +
                "LENGTH(Name) <= 6;";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        profiles.Add(StringManipulations.InsertLastCharacterToThirdPosition(name));
                    }
                }
            }
            return profiles;
        }
    }
    
}
