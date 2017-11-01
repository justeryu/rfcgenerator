using SAP.Middleware.Connector;
using System;

namespace RfcGenerator.Winform.Helper
{
	public class SAPSystemConnect : IDestinationConfiguration
	{
		public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

		public bool ChangeEventsSupported()
		{
			throw new NotImplementedException();
		}

		public RfcConfigParameters GetParameters(string destinationName)
		{
			RfcConfigParameters result;
			try
			{
				ConnectionInformations ci = new ConnectionInformations();
				result = new RfcConfigParameters
				{
					{
						"NAME",
						ci.Name
					},
					{
						"ASHOST",
						ci.AppServerHost
					},
					{
						"SYSNR",
						ci.SystemNumber
					},
					{
						"SYSID",
						ci.SystemID
					},
					{
						"USER",
						ci.User
					},
					{
						"PASSWD",
						ci.Password
					},
					{
						"CLIENT",
						ci.Client
					},
					{
						"LANG",
						ci.Language
					},
					{
						"POOL_SIZE",
						ci.PoolSize
					},
					{
						"MAX_POOL_SIZE",
						ci.MaxPoolSize
					},
					{
						"IDLE_TIMEOUT",
						ci.IdleTimeout
					}
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
