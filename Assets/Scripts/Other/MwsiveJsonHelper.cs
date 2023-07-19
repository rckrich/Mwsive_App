using System;
using System.Collections.Generic;

#region Mwsive Entities

public class MwsiveUser
{
    public string id { get; set; }
    public string email { get; set; }
    public string genre { get; set; }
    public int age { get; set; }
}

public class TrackAction
{
    public int user_id { get; set; }
    public int track_id { get; set; }
    public string action { get; set; }
    public float duration { get; set; }
}

#endregion

#region Json Convert Classes

public class MwsiveUserRoot
{
    public string MwsiveUser { get; set; }
}

public class MwsiveLoginRoot
{
    public MwsiveUser MwsiveUser { get; set; }
    public string mwsive_token { get; set; }
}

public class MwsiveCuratorsRoot
{
    public List<MwsiveUser> curators { get; set; } 
}

#endregion