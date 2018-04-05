using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This behaviour defines a countdown timer text on screen.
/// It holds values from 99.99f to 00.00f and counts down in seconds.
/// Supports value update from a websocket callback.
/// 
/// This counter only starts to count down at the first callback received
/// </summary>
public class ServerCounter : MonoBehaviour
{

    private const float DEFAULT_VALUE = 99.99f;
    private const float MIN_VALUE = 00.00f;

    private const CommandAction ACTION_TO_OBEY = CommandAction.UPDATE_COUNTER;
    private float currentValue;
    private bool counting;
    public Text textGO;

    // Use this for initialization
    void Start()
    {
        this.currentValue = ServerCounter.DEFAULT_VALUE;
        this.counting = false;
        if (ServerManager.ws == null)
        {
            Debug.LogError("Cannot initialize a ServerCounter before connecting with a server.");
            this.gameObject.SetActive(false);
        }
        else
        {
            ServerManager.ws.OnMessage += OnServerCountReceive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            this.UpdateCounter();
        }

    }

    private void UpdateCounter()
    {
        if (this.currentValue > ServerCounter.MIN_VALUE)
        {
            this.currentValue -= Time.deltaTime;
            textGO.text = ((int)this.currentValue).ToString();
        }
        else
        {
            this.currentValue = ServerCounter.MIN_VALUE;
        }
    }

    private void OnServerCountReceive(object sender, WebSocketSharp.MessageEventArgs e)
    {
        CommandResponse c;

        try
        {
            c = JsonUtility.FromJson<CommandResponse>(e.Data);
            if (c.action != ServerCounter.ACTION_TO_OBEY)
            {
                return;
            }
            else
            {
                // TODO: Check that this is working.
                this.counting = true;
                this.currentValue = float.Parse(c.payload);
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Wrong Command Response serialization");
            return;
        }

    }
}
