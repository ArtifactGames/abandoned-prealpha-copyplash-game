using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyGM : MonoBehaviour
{

    public Text passwordText;
    public Text playerListText;

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
        if (c.action == CommandAction.UPDATE_PLAYERS)
        {
            UpdatePlayerList(JsonUtility.FromJson<List<Player>>(c.data));
        }
        Debug.Log("Recieved a CommandResponse:");
        Debug.Log(c.ToString());
    }

    private void UpdatePlayerList(List<Player> p)
    {
        // Update internal lobby player list
        GameInfo.lobby.players = p;

        // Update player list text
        playerListText.text = Player.ListToString(GameInfo.lobby.players);
    }

    public void buttonToSendThings()
    {
        // TODO: Please remove me, this is hideous.
        CommandRequest c = new CommandRequest();
        c.action = CommandAction.RETRIEVE;
        ServerManager.SendRequest(c);
    }
}
