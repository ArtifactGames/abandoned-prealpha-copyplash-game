
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{   
    public string id;
    public string nick;

    public static string ListToString(List<Player> p)
    {
        string toRet = "";
        foreach (Player player in p)
        {
            toRet += string.Format("{0} \n", player.nick);
        }
        return toRet;
    }
}

[Serializable]
public class PlayerList
{
    public Player[] players;
}