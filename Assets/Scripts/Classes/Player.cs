using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
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
