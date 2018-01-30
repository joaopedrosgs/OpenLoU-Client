using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public static class Client {

	
	//ip/address of the server, 127.0.0.1 is for your own computer
	public static string ConHost = "localhost";
	
	//port for the server, make sure to unblock this in your router firewall if you want to allow external connections
	public static int ConPort = 8080;
	
	//a true/false variable for connection status
	public static bool SocketReady;
	
	static TcpClient _mySocket;
	static NetworkStream _theStream;
	static StreamWriter _theWriter;
	static StreamReader _theReader;
	
	//try to initiate connection
	public static void SetupSocket() {
		try {
			_mySocket = new TcpClient(ConHost, ConPort);
			_theStream = _mySocket.GetStream();
			_theWriter = new StreamWriter(_theStream);
			_theReader = new StreamReader(_theStream);
			SocketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error:" + e);
		}
	}
	
	//send message to server
	public static void WriteSocket(AnswerTypes type, Dictionary<string,int> data)
	{
		while (!_theStream.CanWrite) ;
			
		Request request = new Request(type, data);
		string jsonRequest = JsonConvert.SerializeObject(request);
		Debug.Log("Enviado: " + jsonRequest);
		_theWriter.Write(jsonRequest);
		_theWriter.Flush();
	}
	
	//read message from server
	public static string ReadSocket() {
		
		while (!_theStream.CanRead);
		
		var inStream = new char[_mySocket.ReceiveBufferSize];
		_theReader.Read(inStream, 0, inStream.Length);	
		return new string(inStream);
	}
	
	//disconnect from the socket
	public static void CloseSocket() {
		if (!SocketReady)
			return;
		_theWriter.Close();
		_theReader.Close();
		_mySocket.Close();
		SocketReady = false;
	}
	
	//keep connection alive, reconnect if connection lost
	static  void MaintainConnection(){
		if(!_theStream.CanRead) {
			SetupSocket();
		}
	}

	public static string WriteToServer(AnswerTypes type, Dictionary<string, int> map)
	{
		WriteSocket(type, map);
		return ReadSocket();
	}

	

}
