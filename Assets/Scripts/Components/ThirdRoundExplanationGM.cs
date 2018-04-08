using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdRoundExplanationGM : MonoBehaviour {

	public void GoToThirdRoundWait(){
		SceneManager.LoadScene("third-round-wait");
	}
}
