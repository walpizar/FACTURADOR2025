using System.Configuration;
namespace CommonLayer
{
    public class AppSetting
    {

        Configuration config;

        public AppSetting()
        {

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }


        public string GetConnectionString(string key)
        {

            return config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
        }

        public void saveConnectionsString(string key, string value)
        {

            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified);

        }


    }
}
