using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public enum CityType
    {
        Normal,
        Castle,
        Palace
    }
    public class City
    {
        public int ID;
        public int X;
        public int Y;
        public int ContinentID;
        public CityType Type;
        public string Name;
        public int Points;
        public CityData Data;



    }


    public class CityData
    {

        public List<Construction> Constructions;
    }

    public class Construction
    {
        public int CityID;
        public int Type;
        public int X;
        public int Y;
        public int Level;
    }

    public struct ConstructionType
    {
        public int ID;
        public string Name;

        public string Info;

        public UnityEngine.Tilemaps.TileBase Tile;

    }
    public struct ConstructionUpdate
    {
        public int ConstructionID;
        public int CityID;
        public int Index;
        public TimeSpan Duration;
        public DateTime Start;
    }


}