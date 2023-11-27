using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
public class PlayerNameInput : NetworkBehaviour
{
    public InputField nameInput;

    private void Start()
    {
        if (isLocalPlayer)
        {
            nameInput.onEndEdit.AddListener(SubmitName);
        }
    }

    private void SubmitName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("¬ведите им€ игрока!");
            return;
        }

        CmdSetPlayerName(name);
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        var player = GetComponent<Player>();
        player.playerName = name;

        var server = FindObjectOfType<Server>();
        server.RegisterPlayerName(connectionToClient.connectionId, name);
    }
}
*/