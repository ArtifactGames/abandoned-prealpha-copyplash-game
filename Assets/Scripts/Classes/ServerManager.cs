using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class ServerManager
{
    private static string serverAddress = "127.0.0.1";
    private static string serverPort = "8080";

    /// <summary>
    /// A WebSocket object with the current WebSocket connection.
    /// It is null if no connection was made.
    /// </summary>
    public static WebSocket ws = null;

    /// <summary>
    /// Sends a web request to the REST server to create a new lobby. Needs a
    /// callback function to execute when finished.
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public static IEnumerator CreateLobby(System.Action<RequestStatus, string> callBack)
    {
        UnityWebRequest request = UnityWebRequest.Get(string.Format("http://{0}:{1}/lobby-create", serverAddress, serverPort));
        // Wait for a second to get response

        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {
            callBack(RequestStatus.OK, request.downloadHandler.text);
        }
        else
        {
            callBack(RequestStatus.ERROR, request.downloadHandler.text);
        }

    }

    /// <summary>
    /// Establishes a WebSocket connection with the server, in the ws object.
    /// </summary>
    public static void EstablishConnection()
    {
        ws = new WebSocket(string.Format("ws://{0}:{1}", serverAddress, serverPort));
        ws.Connect();
    }


}
