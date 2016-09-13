using System;
using System.Threading.Tasks;

namespace Remote
{
	public class SmartObject
	{
		private string _smartID;

		public string SmartID { get { return _smartID; } }

		private string _systemCode = string.Empty;
		public string SystemCode { get{ return _systemCode; } }

		private string _regionCode = string.Empty;
		public string RegionCode { get{ return _regionCode; } }

		public string Name { get; set; }

		public string Image {
			get{
				if (Type == SmartObjectType.Fan) {
					if (State == 0) {
						return "fan_off.png";
					} else if(State == 1){
						return "fan_on.png";
					}
				} else if (Type == SmartObjectType.Light) {
					if (State == 0) {
						return "lamp_off.png";
					} else if(State == 1){
						return "lamp_on.png";
					}
				}
				return string.Empty;
			}
		}

		public SmartObjectType Type { get; set; }

		public int State = 0;

		public SmartObject (string SystemCode, string RegionCode)
		{
			this._systemCode = SystemCode;
			this._regionCode = RegionCode;
			this._smartID = Guid.NewGuid().ToString();
			Type = SmartObjectType.Light;
		}


	}
}

