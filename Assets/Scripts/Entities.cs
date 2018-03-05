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
        public int X;
        public int Y;
        public int ContinentX;
        public int ContinentY;
        public CityType Type;
        public string Name;
        public string UserName;
        public int Points;

        public List<Construction> Constructions;
        public List<ConstructionUpdate> BuildingQueue;

        public bool Contains(Construction construction)
        {
            return X == construction.CityX && Y == construction.CityY;
        }
        public override int GetHashCode()
        {
            return (X << 10) + Y;
        }
        public bool Equals(City c)
        {
            if (Object.ReferenceEquals(c, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, c))
            {
                return true;
            }

            if (this.GetType() != c.GetType())
            {
                return false;
            }

            return (X == c.X) && (Y == c.Y);
        }

        public int ContinentID()
        {
            return (ContinentX + (ContinentY * 10));
        }
    }


    public struct User
    {
        public string Key;
        public string Name;
        public string Email;
        public string AllianceName;
        public string Gold;
        public string Diamonds;
        public string Darkwood;
        public string Runestone;
        public string Veritium;
        public string Trueseed;
        public string Rank;
    }
    public class Construction
    {
        public int CityX;
        public int CityY;
        public int X;
        public int Y;
        public int Type;

        public int Level;
        public bool Equals(Construction c)
        {
            if (Object.ReferenceEquals(c, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, c))
            {
                return true;
            }

            if (this.GetType() != c.GetType())
            {
                return false;
            }

            return (X == c.X) && (Y == c.Y);
        }

        public override int GetHashCode()
        {
            return (CityX << 20) + (CityY << 10) + (X << 5) + Y;
            //       10              10             5         5
            //Max construction coord = 21 = 10101 bin which has 5 digits
            //Max city coord = 600 = 1001011000 bin which has 10 digits
        }
        public bool BelongsTo(City city)
        {
            return CityX == city.X && CityY == city.Y;
        }
        public Construction(int cityX, int cityY, int x, int y, int type, int level)
        {
            CityX = cityX;
            CityY = cityY;
            X = x;
            Y = y;
            Type = type;
            Level = level;
        }

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
        public int X;
        public int Y;
        public int CityX;
        public int CityY;
        public int Index;
        public TimeSpan Duration;
        public DateTime Start;
        public bool BelongsTo(City city)
        {
            return CityX == city.X && CityY == city.Y;
        }

    }


}