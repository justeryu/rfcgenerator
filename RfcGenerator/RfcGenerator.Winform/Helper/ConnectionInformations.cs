using System;
using System.Configuration;

namespace RfcGenerator.Winform.Helper
{
	public class ConnectionInformations
	{
		public string Name = "";

		public string AppServerHost = "";

		public string SystemNumber = "";

		public string SystemID = "";

		public string User = "";

		public string Password = "";

		public string Client = "";

		public string Language = "";

		public string PoolSize = "";

		public string MaxPoolSize = "";

		public string IdleTimeout = "";

		public ConnectionInformations()
		{
			try
			{
				this.Name = ConfigurationSettings.AppSettings["Name"];
				this.AppServerHost = ConfigurationSettings.AppSettings["AppServerHost"];
				this.SystemNumber = ConfigurationSettings.AppSettings["SystemNumber"];
				this.SystemID = ConfigurationSettings.AppSettings["SystemID"];
				this.User = ConfigurationSettings.AppSettings["User"];
				this.Password = ConfigurationSettings.AppSettings["Password"];
				this.Client = ConfigurationSettings.AppSettings["Client"];
				this.Language = ConfigurationSettings.AppSettings["Language"];
				this.PoolSize = ConfigurationSettings.AppSettings["PoolSize"];
				this.MaxPoolSize = ConfigurationSettings.AppSettings["MaxPoolSize"];
				this.IdleTimeout = ConfigurationSettings.AppSettings["IdleTimeout"];
			}
			catch
			{
			}
		}
	}
}
