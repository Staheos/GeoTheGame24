using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

public class PacketHandler
{
	private Socket _server;
	private int _clientID;
	public PacketHandler()
	{
		this._clientID = -12;
	}
	public void Connect()
	{
		IPHostEntry ipHostInfo = Dns.GetHostEntry("192.168.1.121");
		IPAddress ipAddress = ipHostInfo.AddressList[0];

		IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8989);
		this._server = new Socket(
			ipEndPoint.AddressFamily,
			SocketType.Stream,
			ProtocolType.Tcp
			);

		DateTime now = DateTime.Now;
		DateTime last = DateTime.Now;

		//await client.ConnectAsync(ipEndPoint);
		//client.Connect(ipEndPoint);
		this._server.Connect(ipEndPoint);

		//this._server.Send(Encoding.UTF8.GetBytes("camera"));
		this.Send(Encoding.UTF8.GetBytes("start"));

		byte[] buff = new byte[1024];
		int recv;
		recv = this._server.Receive(buff);
		Console.WriteLine(Encoding.UTF7.GetString(buff));
	}

	/// <summary>
	/// Zakodowuje wiadomość do bajtów i ją wysyła
	/// </summary>
	/// <param name="data">Bajty pakietu</param>
	/// <param name="packetType">Typ pakietu</param>
	/// <returns>Ilość wysłanych bajtow</returns>
	public int Send(byte[] data, PacketType packetType = PacketType.CUSTOM)
	{
		byte[] buffer = new byte[data.Length + 12];
		BitConverter.GetBytes((int)packetType).CopyTo(buffer, 0);
        BitConverter.GetBytes(data.Length).CopyTo(buffer, 4);
		// aby było wiadomo od kogo jest ten pakiet
        BitConverter.GetBytes(this._clientID).CopyTo(buffer, 8);
		data.CopyTo(buffer, 12);
		return this._server.Send(buffer);
	}
	public int Receive(ref byte[] data, int count, SocketFlags socketFlags = SocketFlags.None)
	{
		return this._server.Receive(data, count, socketFlags);
	}

	public int SendPosition(Vector3 position)
	{
		byte[] bytes = new byte[12];
		BitConverter.GetBytes(position.x).CopyTo(bytes, 0);
		BitConverter.GetBytes(position.y).CopyTo(bytes, 4);
		BitConverter.GetBytes(position.z).CopyTo(bytes, 8);
		return this.Send(bytes, PacketType.POSITION);
	}
	public int SendPosition(Vector2 position)
	{
		return this.SendPosition(new Vector3(position.x, position.y, 0));
	}
}
