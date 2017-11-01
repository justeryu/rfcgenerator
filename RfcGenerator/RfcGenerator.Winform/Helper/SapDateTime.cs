using System;

namespace RfcGenerator.Winform.Helper
{
	public class SapDateTime
	{
		public enum DataType
		{
			Date,
			Time
		}

		private DateTime? _datetime;

		public SapDateTime.DataType Type;

		public static SapDateTime Empty
		{
			get
			{
				return new SapDateTime();
			}
		}

		public SapDateTime(string s, SapDateTime.DataType t)
		{
			try
			{
				this.Type = t;
				this._datetime = new DateTime?((DateTime)Convert.ChangeType(s, typeof(DateTime)));
			}
			catch
			{
			}
		}

		public SapDateTime()
		{
		}

		public override string ToString()
		{
			string result;
			if (!this._datetime.HasValue)
			{
				result = "";
			}
			else if (this.Type == SapDateTime.DataType.Time)
			{
				result = this._datetime.Value.ToString("hh:mm:ss");
			}
			else
			{
				result = this._datetime.Value.ToString("dd.MM.yyyy");
			}
			return result;
		}

		public DateTime? ToDateTime()
		{
			return this._datetime;
		}

		public DateTime ToSapObject()
		{
			return this._datetime.Value;
		}
	}
}
