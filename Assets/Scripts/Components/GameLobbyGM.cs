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
        ServerManager.ws.OnMessage += this.OnRecieveResponse;
    }

    private void OnRecieveResponse(object sender, WebSocketSharp.MessageEventArgs e)
    {
        // TODO: Handle players joining the lobby.
        CommandResponse c = JsonUtility.FromJson<CommandResponse>(e.Data);
        Debug.Log("Recieved a CommandResponse:");
        Debug.Log(c.ToString());
    }

    public void buttonToSendThings(){
        // TODO: Please remove me, this is hideous.
        CommandRequest c = new CommandRequest();
        c.action = CommandAction.RETRIEVE;
        ServerManager.SendRequest(c);
    }
}
