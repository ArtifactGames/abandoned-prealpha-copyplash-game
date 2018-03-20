using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLobbyConnector : MonoBehaviour {

	public GameObject connectingText;
	public GameObject loadingIcon;
	public GameObject errorConnectingText;

	// Use this for initialization
	void Start () {
		StartCoroutine(ServerManager.CreateLobby(onCreateLobby));
	}

	private void onCreateLobby(RequestStatus status, string response){
		if(status == RequestStatus.ERROR){
			connectingText.SetActive(false);
			loadingIcon.SetActive(false);
			errorConnectingText.SetActive(true);
		}else if(status == RequestStatus.OK){
			GameInfo.lobby = JsonUtility.FromJson<Lobby>(response);
            SceneManager.LoadScene("game-lobby");
        }
	}
}
