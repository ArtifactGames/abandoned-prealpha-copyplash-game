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
	public GameObject startButtonGO;

	private String newPlayerList;
	private int playerCount;

	// Use this for initialization
	void Start ()
	{
		
		startButtonGO.SetActive (false);
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
		List<Player> players = GameInfo.lobby.players;

		// Update internal lobby player list
		if (players == null) {
			players = new List<Player> ();
		}
		if (p != null) {
			players = p;
			newPlayerList = Player.ListToString (players);
			playerCount = players.Count;

		}
		
	}

	public void loadFirstScene ()
	{
		SceneManager.LoadScene ("first-round-explanation");
	}

	void Update ()
	{
		// Update player list text
		if (newPlayerList != "") {
			playerListText.text = newPlayerList;
			newPlayerList = "";
			//manage start button
			if (playerCount > 1) {
				startButtonGO.SetActive (true);
			} else {
				startButtonGO.SetActive (false);
			}
		}

	}
}
