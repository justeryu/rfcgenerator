using SAP.Middleware.Connector;
using System;

namespace RfcGenerator.Winform.Helper
{
	public class SapDestination
	{
		private static SAPSystemConnect conn = null;

		private static RfcDestination rfcDest = null;

		public static RfcDestination Destination
		{
			get
			{
				if (SapDestination.conn == null || SapDestination.rfcDest == null)
				{
					SapDestination.Init();
				}
				return SapDestination.rfcDest;
			}
		}

		public static void Init()
		{
			if (SapDestination.conn == null)
			{
				SapDestination.conn = new SAPSystemConnect();
				RfcConfigParameters rfc = SapDestination.conn.GetParameters("");
				SapDestination.rfcDest = RfcDestinationManager.GetDestination(rfc);
			}
		}
	}
}
