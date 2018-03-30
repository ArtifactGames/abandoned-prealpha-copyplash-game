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
	private String newPlayerList;

	// Use this for initialization
	void Start ()
	{
		newPlayerList = "";
		try {
			playerListText.text = "None";
			passwordText.text = GameInfo.lobby.password.ToString ("D4");
		} catch (NullReferenceException) {
			passwordText.text = "ERROR";
		}

		ServerManager.EstablishConnection ();
		ServerManager.ws.OnMessage += this.OnReceiveResponse;

	}

	private void OnReceiveResponse (object sender, WebSocketSharp.MessageEventArgs e)
	{
        

		CommandResponse c;

		try {
			c = JsonUtility.FromJson<CommandResponse> (e.Data);
		} catch (NullReferenceException) {
			Debug.LogWarning ("Wrong Command Response serialization");
			c = null;
		}

		if (c == null) {
			return;
		}

		switch (c.action) {
		case CommandAction.UPDATE_PLAYERS:
			this.OnUpdatePlayerCommandAction (c);
			break;
		case CommandAction.START_GAME:
			this.OnStartGameCommandAction (c);
			break;
		default:
			Debug.LogError ("NOT IMPLEMENTED: CommandAction not implemented");
			break;
		}

        
	}

	private void OnStartGameCommandAction (CommandResponse c)
	{
		SceneManager.LoadScene ("first-round-explanation");
	}

	private void OnUpdatePlayerCommandAction (CommandResponse c)
	{
		try {
			PlayerList playerList = JsonUtility.FromJson<PlayerList> (c.payload);
		
			UpdatePlayerList (PlayerList.AsList (playerList));

		} catch (NullReferenceException err) {
			newPlayerList = "";
			Debug.LogWarning ("Wrong Player payload serialization or other related nullpoint @ UpdatePlayerList:" + err);
		} catch (UnityException err) {
			Debug.LogError (err);
		}
	}

	private void UpdatePlayerList (List<Player> p)
	{

		// Update internal lobby player list
		if (GameInfo.lobby.players == null) {
			GameInfo.lobby.players = new List<Player> ();
		}
		if (p != null) {
			GameInfo.lobby.players = p;
			newPlayerList = Player.ListToString (GameInfo.lobby.players);
		}
		
	}

	void Update ()
	{
		// Update player list text
		if (newPlayerList != "") {
			Debug.Log (newPlayerList);
			playerListText.text = newPlayerList;
			newPlayerList = "";
		}
	}
}
