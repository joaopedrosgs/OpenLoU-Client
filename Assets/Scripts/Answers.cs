using System.Collections.Generic;
using Assets.Scripts;

public enum RequestType
{
    GetUserInfo = 101,
    CreateCity = 201,
    GetCities = 202,
    GetCitiesFromUser = 203,
    UpgradeConstruction = 301,
    NewConstruction = 302,
    GetConstructions = 303,
    GetUpgrades = 304
}

public class AnswerGeneric
{
    public bool Ok;
    public RequestType Type;
}

public class Cities
{
    public List<City> Data;
}
public class Constructions
{
    public List<Construction> Data;
}
public class ConstructionUpdates
{
    public List<ConstructionUpdate> Data;
}
public class NewConstruction
{
    public int Type;
    public int X;
    public int Y;
    public int CityID;
}