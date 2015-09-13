using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Remote
{
	public class SynchronousSocketConnector
	{

		// Incoming data from the client.
		private static SynchronousSocketConnector _instance = null;

		public static SynchronousSocketConnector Instance {
			get { 
				return _instance; 
			}
		}

		IPEndPoint remoteEP;
		Socket sender;

		public string IPAdress { get { return remoteEP.Address.ToString (); } }

		public int Port { get { return remoteEP.Port; } }

		public static SynchronousSocketConnector InitializeSocket (string hostAdress, int port)
		{
			if (_instance == null) {
				_instance = new SynchronousSocketConnector (hostAdress, port);
			}

			return _instance;
		}

		private SynchronousSocketConnector (string hostAdress, int port)
		{
			IPHostEntry ipHostInfo = Dns.Resolve (hostAdress);
			IPAddress ipAddress = ipHostInfo.AddressList [0];

			// Establish the remote endpoint for the socket.
			// This example uses port 11000 on the local computer.
			remoteEP = new IPEndPoint (ipAddress, port);

			// Create a TCP/IP  socket.
			sender = new Socket (AddressFamily.InterNetwork, 
				SocketType.Stream, ProtocolType.IP);
		}

		public SmartObject TurnOn (SmartObject smartObject)
		{
			string result = SendCode (smartObject.SystemCode, smartObject.RegionCode, "1");
			if (string.IsNullOrEmpty (result) || string.IsNullOrWhiteSpace (result)) {
				result = "-1";
			}
			smartObject.State = Convert.ToInt32 (result);
			return smartObject;
		}


		public SmartObject TurnOff (SmartObject smartObject)
		{
			string result = SendCode (smartObject.SystemCode, smartObject.RegionCode, "0");
			if (string.IsNullOrEmpty (result) || string.IsNullOrWhiteSpace (result)) {
				result = "-1";
			}
			smartObject.State = Convert.ToInt32 (result);
			return smartObject;
		}

		public SmartObject Toogle (SmartObject smartObject)
		{
			if (smartObject.State == 0) {
				smartObject = TurnOn (smartObject);
			} else {
				smartObject = TurnOff (smartObject);
			}
			return smartObject;
		}

		public string SendCode (string systemCode, string regionCode, string state)
		{
			// Data buffer for incoming data.
			byte[] bytes = new byte[1024];
			string result = string.Empty;
			try {
				// Connect the socket to the remote endpoint. Catch any errors.
				try {
					sender = new Socket (AddressFamily.InterNetwork, 
						SocketType.Stream, ProtocolType.IP);
					sender.Connect (remoteEP);
					Console.WriteLine ("Socket connected to {0}",
						sender.RemoteEndPoint.ToString ());
					
						
					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes (String.Format ("{0}{1}{2}", systemCode, regionCode, state));

					// Send the data through the socket.
					int bytesSent = sender.Send (msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive (bytes);
					result = Encoding.ASCII.GetString (bytes, 0, bytesRec);

					Console.WriteLine ("Received = {0}", result);

					// Release the socket.
					sender.Shutdown (SocketShutdown.Both);
					sender.Close ();

				} catch (ArgumentNullException ane) {
					Console.WriteLine ("ArgumentNullException : {0}", ane.ToString ());
				} catch (SocketException se) {
					Console.WriteLine ("SocketException : {0}", se.ToString ());
				} catch (Exception e) {
					Console.WriteLine ("Unexpected exception : {0}", e.ToString ());
				}

			} catch (Exception e) {
				Console.WriteLine (e.ToString ());
			}
			return result;
		}
	}
}