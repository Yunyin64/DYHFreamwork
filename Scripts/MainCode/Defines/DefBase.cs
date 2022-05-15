using System;
using System.Xml.Serialization;
using ShuShan;
namespace ShuShan
{
	public class DefBase
	{
		public bool CanFillLanText(string flag)
		{
			return this.FromMod == null || this.FromMod == flag;
		}

		[XmlIgnore]
		public string FromMod;
	}
}