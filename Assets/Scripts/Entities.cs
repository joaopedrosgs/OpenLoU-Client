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
        public UnityEngine.Tilemaps.Tile Tile;


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
        public UnityEngine.Tilemaps.Tile Tile;
    }

    public class ConstructionType
    {
        public int ID;
        public string Name;

        public UnityEngine.Tilemaps.Tile Tile;

    }


}