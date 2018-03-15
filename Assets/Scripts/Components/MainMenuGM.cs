using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newGame(){
		SceneManager.LoadScene("game-session");
	}

	public void exit(){
		Application.Quit();
	}
}
