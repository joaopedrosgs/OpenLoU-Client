using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public static class Client
{

    public static readonly string Host = "localhost";

    public static readonly int Port = 8080;

    static TcpClient _socket;
    static NetworkStream _stream;
    static StreamWriter theWriter;

    static StreamReader theReader;

    //try to initiate connection
    public static void SetupSocket()
    {
        try
        {
            _socket = new TcpClient(Host, Port);
            _stream = _socket.GetStream();
            theWriter = new StreamWriter(_stream);
            theReader = new StreamReader(_stream);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);
        }
    }

    //send message to server
    public static void WriteToServer(AnswerTypes type, Dictionary<string, int> data)
    {
        if (!_socket.Connected) return;
        Request request = new Request(type, data);
        string jsonRequest = JsonConvert.SerializeObject(request);
        Debug.Log("Enviado: " + jsonRequest);

        theWriter.Write(jsonRequest);
        theWriter.Flush();
    }

    //read message from server
    public static string ReadFromServer()
    {
        if (_stream==null || !_stream.DataAvailable) return null;
        var inStream = new char[_socket.ReceiveBufferSize];
        var read = theReader.Read(inStream, 0, inStream.Length);
        if (read == 0)
        {
            _socket.Close();
        }
        return new string(inStream);
    }



}
