using System.Collections.Generic;
using Assets.Scripts;

public enum AnswerTypes
{
    CreateCity = 101,
    GetCities = 102,
    GetCitiesFromUser = 103,

    UpgradeConstruction = 201,
    NewConstruction = 202,
    GetConstructions = 203
}

public class AnswerGeneric
{
    public bool Ok;
    public AnswerTypes Type;
}

public class Cities
{
    public List<City> Data;
}
public class Constructions
{
    public List<Construction> Data;
}
public class NewConstruction
{
    public int Type;
    public int X;
    public int Y;
    public int CityID;
}