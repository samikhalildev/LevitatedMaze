using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    void Start() {

        /* If a player is trying to access this script and is not the player on THIS machine, then this script will be removed and it wont run.
         * This is so other players wont be able to control other players such as movements, shooting etc..
         */
        if(!isLocalPlayer){
            DisablComponents();

        } else {
            sceneCamera = Camera.main;

            if(sceneCamera != null){
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    // This method gets called when a player joins the game
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _PlayerID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _PlayerID; 

        Player _Player = GetComponent<Player>();

        GameManager.AddPlayer(_PlayerID, _Player);
    }


    void DisablComponents() {
        foreach(Behaviour component in componentsToDisable){
            component.enabled = false;
        }
    }

    // This will assign a remote player layer for all other players
    void AssignRemoteLayer(){
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }

    void OnDisable() {
        
        if(sceneCamera != null){
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.RemovePlayer(transform.name);

    }
}
