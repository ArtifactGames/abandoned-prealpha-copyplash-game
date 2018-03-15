using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyGM : MonoBehaviour
{

    public Text passwordText;

    // Use this for initialization
    void Start()
    {
        try
        {
            passwordText.text = GameInfo.lobby.password.ToString("D4");
        }
        catch (NullReferenceException)
        {
            passwordText.text = "ERROR";
        }

        ServerManager.EstablishConnection();
        ServerManager.ws.OnMessage += (sender, e) =>
        {
            // TODO: Handle players joining the lobby.
            Debug.Log(e.Data);
        };
    }
}
