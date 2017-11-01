using SAP.Middleware.Connector;
using System;

namespace RfcGenerator.Winform.Helper
{
	public interface ISapStructure
	{
		IRfcStructure ToSapObject();

		ISapStructure FromSapObject(IRfcStructure s);
	}
}
