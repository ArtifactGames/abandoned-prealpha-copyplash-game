using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommandResponse
{
	public CommandAction action;

    public override string ToString()
    {
        return string.Format("Action: {0}", action);
    }

}
