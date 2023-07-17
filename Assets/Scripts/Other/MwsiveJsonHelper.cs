using System;
using System.Collections.Generic;

#region Mwsive Entities

public class MwsiveUser
{
    string id { get; set; }
    string email { get; set; }
    string genre { get; set; }
    int age { get; set; }
}

#endregion

#region Json Convert Classes

public class RootMwsiveUser
{
    string MwsiveUser { get; set; }
}

public class RootMwsiveLogin
{
    MwsiveUser MwsiveUser { get; set; }
    string mwsive_token { get; set; }
}

#endregion