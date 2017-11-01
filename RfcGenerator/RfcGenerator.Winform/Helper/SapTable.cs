using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;

namespace RfcGenerator.Winform.Helper
{
	public class SapTable<T> : List<T>
	{
		public IRfcTable ToSapObject()
		{
			Type type = typeof(T);
			RfcStructureMetadata sMeta = SapDestination.Destination.Repository.GetStructureMetadata(typeof(T).Name);
			RfcTableMetadata tMeta = new RfcTableMetadata("", sMeta);
			IRfcTable t = tMeta.CreateTable();
			for (int i = 0; i < base.Count; i++)
			{
				t.Insert(((ISapStructure)((object)base[i])).ToSapObject());
			}
			return t;
		}

		public SapTable<T> FromSapObject(IRfcTable t)
		{
			base.Clear();
			for (int i = 0; i < t.RowCount; i++)
			{
				T obj = Activator.CreateInstance<T>();
				base.Add((T)((object)((ISapStructure)((object)obj)).FromSapObject(t[i])));
			}
			return this;
		}
	}
}
