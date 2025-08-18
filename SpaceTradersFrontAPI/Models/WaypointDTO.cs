namespace SpaceTradersFrontAPI.Models;

public class WaypointDTO
{
    public string symbol { get; set; }
    public string type { get; set; }
    public string systemSymbol { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Orbitals[] orbitals { get; set; }
    public string orbits { get; set; }
/*    public Faction faction { get; set; }
    public Traits[] traits { get; set; }
    public Modifiers[] modifiers { get; set; }
    public Chart chart { get; set; } */
    public bool isUnderConstruction { get; set; }
}

public class Orbitals
{
    public string symbol { get; set; }
}

public class Faction
{
    public string symbol { get; set; }
}

public class Traits
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Modifiers
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Chart
{
    public string waypointSymbol { get; set; }
    public string submittedBy { get; set; }
    public string submittedOn { get; set; }
}

