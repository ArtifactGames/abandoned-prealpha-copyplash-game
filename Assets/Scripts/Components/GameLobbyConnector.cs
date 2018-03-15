using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLobbyConnector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(ServerManager.CreateLobby(onCreateLobby));
	}

	private void onCreateLobby(RequestStatus status, string response){
		if(status == RequestStatus.ERROR){
			// TODO: Display error text and a button to go back to the menu.
			Debug.Log("Error");
		}else if(status == RequestStatus.OK){
			GameInfo.lobby = JsonUtility.FromJson<Lobby>(response);
            SceneManager.LoadScene("game-lobby");
        }
	}
}
