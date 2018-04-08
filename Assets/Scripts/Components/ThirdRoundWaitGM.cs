using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoundWaitGM : MonoBehaviour
{

    void Start()
    {
        ServerManager.ws.OnMessage += this.OnRecieveResponse;
    }

    private void OnRecieveResponse(object sender, WebSocketSharp.MessageEventArgs e)
    {
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

		// TODO: Manage server response
		Debug.Log(c);
    }
}
