using System.Collections.Generic;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class City
    {
        public int ID;
        public int X;
        public int Y;
        public int ContinentID;
        public int Type;
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

}