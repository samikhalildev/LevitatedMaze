using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyAI : NetworkBehaviour {

    [SerializeField]
    float speed = 3f;

    Transform player;
    NavMeshAgent navMeshAgent;

    GameObject[] players;
    Enemy _Enemy;

    void Awake() {
        _Enemy = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        SetEnemyDestination();
    }

    /* This method will find players and store them in an array
     * Then will get a random number from 0 to array.length and return the player tranform.
     */
    public Transform PlayerToFollow(){

        players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length >= 1){
            int random = Random.Range(0, players.Length);
            return players[random].transform;
        }

        return null;
    }


    // This method will take player's tranform and set an enemy to follow.
    public void SetEnemyDestination(){

        // If enemy just spawned then set a player to follow
        if(_Enemy.justSpawned)
            player = PlayerToFollow();

        // If there is an agent and a player
        if(navMeshAgent != null && player != null){
            if(_Enemy.EnemyHealth > 0){
                navMeshAgent.SetDestination(player.position);
                _Enemy.justSpawned = false;

            } else {
                navMeshAgent.enabled = false;
            }
        }
    }
}
