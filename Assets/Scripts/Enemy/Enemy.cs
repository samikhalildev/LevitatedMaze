using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

    // This will sync the enemyhealth across the network to all clients
    [SyncVar]
    public float EnemyHealth = 50f;
    public static bool EnemyIsDead;
    public bool justSpawned = true;

    // This method is called on the server then calls another method on all clients.
    [Command]
    public void CmdTakeDamage(float damage){
        EnemyHealth -= damage;
        //Debug.Log("current health: " + EnemyHealth);

        if (EnemyHealth <= 0f){
            RpcDestory(gameObject);
        }
    }

    // This will destory the enemy across the network.
    [ClientRpc]
    void RpcDestory(GameObject enemy){
        NetworkServer.Destroy(enemy);
        Destroy(enemy);

        //Debug.Log("confirmed enemy destroyed on all players");
        EnemyIsDead = true;
    }
}
