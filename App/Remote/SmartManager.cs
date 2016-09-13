using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Remote
{
	public enum ConnectionState
	{
		online,
		offline,
		unknow
	}

	public class SmartManager : GalaSoft.MvvmLight.ViewModelBase
	{
		private static SmartManager _instance = null;

		public bool IsOnline {
			get { 
				if (ConnectionState == ConnectionState.online) {
					return true;
				}
				return false;
			}
		}

		public bool IsOffline {
			get { 
				if (ConnectionState == ConnectionState.offline) {
					return true;
				}
				return false;
			}
		}

		public ConnectionState ConnectionState {
			get{ return _ConnectionState; }
			set{ _ConnectionState = value;
				RaisePropertyChanged ("ConnectionState");
			}
		}

		private ConnectionState _ConnectionState { get; set; }

		public static SmartManager Instance {
			get {
				if (_instance == null) {
					_instance = new SmartManager ();
				}
				return _instance;
			}
		}

		public ObservableCollection<SmartObject> SmartObjects{ get { return new ObservableCollection<SmartObject> (_smartObjects.OrderBy (x => x.Name)); } }

		ObservableCollection<SmartObject> _smartObjects;

		private SmartManager ()
		{
			_smartObjects = new ObservableCollection<SmartObject> ();
			//SmartObjects.CollectionChanged += HandleChange;
		}

		private void HandleChange (object sender, NotifyCollectionChangedEventArgs e)
		{
			foreach (var x in e.NewItems) {
				// do something
			}

			foreach (var y in e.OldItems) {
				//do something
			}
			if (e.Action == NotifyCollectionChangedAction.Move) {
				//do something
			}
		}

		public SmartObject Get (string guid)
		{
			SmartObject smartObjcet;
			lock (_smartObjects) {
				smartObjcet = _smartObjects.FirstOrDefault (x => x.SmartID == guid);
			}
			return smartObjcet;
		}

		public void Save (SmartObject smartObject)
		{
			lock (_smartObjects) {
				SmartObject smartObjcetTmp = Get (smartObject.SmartID);
				if (smartObjcetTmp != null) {
					_smartObjects.Remove (smartObjcetTmp);
				}
				_smartObjects.Add (smartObject);
				RaisePropertyChanged ("SmartObjects");
			}
		}

		public string CreateSmartObjcet (string systemCode, string regionCode, string name = null, SmartObjectType type = SmartObjectType.Light)
		{
			SmartObject smartObjcet = new SmartObject (systemCode, regionCode) {
				Name = name,
				Type = type
			};
			Save (smartObjcet);
			return smartObjcet.SmartID;
		}

		public async Task ToogleAsync (string guid)
		{
			SmartObject smartObjcet = Get (guid);
			if (smartObjcet != null) {
				smartObjcet = await ToogleAsync (smartObjcet);
				Save (smartObjcet);
			}
		}

		public async Task<SmartObject> ToogleAsync (SmartObject smartObjcet)
		{
			return SynchronousSocketConnector.Instance.Toogle (smartObjcet);
		}
	}
}

