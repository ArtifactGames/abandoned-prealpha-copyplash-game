using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenButton : MonoBehaviour
{

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
		GameConfig.fullscreen = Screen.fullScreen;
    }
}
