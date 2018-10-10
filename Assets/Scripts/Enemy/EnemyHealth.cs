using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealth : NetworkBehaviour {

    [SyncVar]
    public float health = 50;
    public static bool EnemyIsDead;

    // this function gets called from the player

    // This will allow the server to destory the enemy object on the entire network.
    [Command]
    public void CmdTakeDamage(float damage){
        
        if (health <= 0f){
            Debug.Log(transform.name + " has been destroyed!");
            RpcDestoryEnemy(gameObject);
            EnemyIsDead = true;

        } else {
            health -= damage;
            //Debug.Log(transform.name + " has " + EnemyHealth + " health");
        }
    }

    [ClientRpc]
    void RpcDestoryEnemy(GameObject enemy){
        Destroy(enemy);
        NetworkServer.Destroy(enemy);
        Debug.Log("confirmed enemy destroyed on all players");
    }
}
