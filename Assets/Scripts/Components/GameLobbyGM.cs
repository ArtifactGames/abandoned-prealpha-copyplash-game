using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        CommandResponse c;

        try
        {
            c = JsonUtility.FromJson<CommandResponse>(e.Data);
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Wrong Command Response serialization");
            c = null;
        }

        if (c == null)
        {
            return;
        }

        switch (c.action)
        {
            case CommandAction.UPDATE_PLAYERS:
                this.OnUpdatePlayerCommandAction(c);
                break;
            case CommandAction.START_GAME:
                break;
            default:
                Debug.LogError("NOT IMPLEMENTED: CommandAction not implemented");
                break;
        }

        
    }

    private void OnStartGameCommandAction(CommandResponse c){
        SceneManager.LoadScene("first-round-explanation");
    }

    private void OnUpdatePlayerCommandAction(CommandResponse c){
        try
        {
            PlayerList playerList = JsonUtility.FromJson<PlayerList>(c.payload);
            UpdatePlayerList(PlayerList.AsList(playerList));
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Wrong Player payload serialization");
        }
        catch (UnityException err)
        {
            Debug.LogError(err);
        }
    }

    private void UpdatePlayerList(List<Player> p)
    {
        // Update internal lobby player list
        GameInfo.lobby.players = p;

        // Update player list text
        playerListText.text = Player.ListToString(GameInfo.lobby.players);
    }
}
