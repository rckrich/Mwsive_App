using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserDataPersistance
{
    [SerializeField] public bool userTokenSetted = false;
    [SerializeField] public string access_token = "";
    [SerializeField] public string token_type = "";
    [SerializeField] public DateTime expires_at = new DateTime(1990, 01, 01);
    [SerializeField] public string raw_value = "";

    public UserDataPersistance(bool _userTokenSetted, string _access_token, string _token_type, DateTime _dateTime, string _raw_value)
    {
        this.userTokenSetted = _userTokenSetted;
        this.access_token = _access_token;
        this.token_type = _token_type;
        this.expires_at = _dateTime;
        this.raw_value = _raw_value;
    }
}

[System.Serializable]
public class AppProgress
{
    [SerializeField] public UserDataPersistance userDataPersistance;
}