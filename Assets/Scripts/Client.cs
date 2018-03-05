using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public static class Client
{

    public static Queue<Request> RequestList;
    public static Queue<string> AnswerList;
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
            RequestList = new Queue<Request>();
            AnswerList = new Queue<string>();

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

    internal static void GetConstructionsFrom(City selectedCity)
    {
        var map = new Dictionary<string, int>();
        map["CityX"] = selectedCity.X;
        map["CityY"] = selectedCity.Y;
        RequestList.Enqueue(new Request(RequestType.GetConstructions, map));
    }

    //send message to server

    public static IEnumerator PopAndWrite()
    {
        yield return new WaitUntil(() => _socket != null);

        while (true)
        {
            if (!_socket.Connected) break;
            yield return new WaitUntil(() => RequestList.Any());
            Request request;
            lock (RequestList)
            {
                request = RequestList.Dequeue();
            }
            string jsonRequest = JsonConvert.SerializeObject(request);
            Debug.Log("Enviado: " + jsonRequest);
            theWriter.Write(jsonRequest);
            theWriter.Flush();
        }
        yield return null;
    }

    //read message from server
    public static IEnumerator ReadFromServer()
    {
        yield return new WaitUntil(() => _socket != null);
        while (true)
        {
            if (_stream == null) break;
            yield return new WaitUntil(() => _stream.DataAvailable);
            var inStream = new char[_socket.ReceiveBufferSize];
            var read = theReader.Read(inStream, 0, inStream.Length);
            if (read > 0)
            {
                lock (AnswerList)
                {
                    AnswerList.Enqueue(new string(inStream));
                }
            }
            else
            {
                _socket.Close();
            }
        }
        yield return null;
    }

    public static void GetCityConstructions(City city)
    {
        var map = new Dictionary<string, int>();
        map["CityX"] = city.X;
        map["CityY"] = city.Y;
        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.GetConstructions, map));
        }
    }
    public static void CreateNewConstruction(int x, int y, int type)
    {

        var map = new Dictionary<string, int>();
        map["CityX"] = DataHolder.SelectedCity.X;
        map["CityY"] = DataHolder.SelectedCity.Y;
        map["X"] = x;
        map["Y"] = y;
        map["Type"] = type;
        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.NewConstruction, map));
        }
    }
    public static bool IsAlive()
    {
        return _socket != null && _socket.Connected;
    }
    public static void GetUpdatesFromCity(City city)
    {
        var map = new Dictionary<string, int>();
        map["CityX"] = city.X;
        map["CityY"] = city.Y;
        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.GetUpgrades, map));
        }
    }
    public static void GetCitiesFromUser()
    {

        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.GetCitiesFromUser, null));
        }
    }
    public static void GetCitiesFromRegion(City city)
    {
        var map = new Dictionary<string, int>();
        map["X"] = city.X;
        map["Y"] = city.Y;
        map["Range"] = 10;
        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.GetCities, map));
        }
    }
    public static void UpgradeConstruction(Construction construction)
    {
        var dic = new Dictionary<string, int>();
        dic["X"] = construction.X;
        dic["Y"] = construction.Y;
        dic["CityX"] = construction.CityX;
        dic["CityY"] = construction.CityY;
        lock (RequestList)
        {
            RequestList.Enqueue(new Request(RequestType.UpgradeConstruction, dic));
        }

    }

}
