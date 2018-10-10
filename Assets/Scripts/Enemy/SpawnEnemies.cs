using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnEnemies : NetworkBehaviour {

    public GameObject enemy;
    public Transform[] spawnPoints;

    GameObject player;
    EnemyAI enemyAI;

    public float spawnTime = 3f;
    public int maximumNumberOfEnemies;

    int randomSpawnPoint;

    public static int enemiesOnTheMap;

    void Awake() {
    }

    void Start () {
        // Call this method every few seconds depending on the spawntime.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    void Update() {

        if(Enemy.EnemyIsDead){
            enemiesOnTheMap--;
            Enemy.EnemyIsDead = false;
;        }

        GameManager.enemiesLeft = enemiesOnTheMap;
    }


    /* This method will get a random spawn point from the listed points in the array
     * It will then spawn an enemy to that location using the enemy prefab attached.
     */
    void Spawn(){
        
        if(enemiesOnTheMap < maximumNumberOfEnemies && GameObject.FindGameObjectWithTag("Player") != null){
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject EnemyGameObject = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
            NetworkServer.Spawn(EnemyGameObject);
            enemiesOnTheMap++;
        } 
    }
}