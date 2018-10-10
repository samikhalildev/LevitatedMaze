using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    [SerializeField]
    float damage = 5;
    [SerializeField]
    float range = 50f;
    [SerializeField]
    float hitForce = 30f;
    [SerializeField]
    float fireRate = 15f;

    [SerializeField]
    Camera cam;

    [SerializeField]
    ParticleSystem gunParticles;

    [SerializeField]
    GameObject hitEffect;

    [SerializeField]
    GameObject dieEffect;

    public float nextTimeToFire = 0.5f;

    Enemy enemy;

    RaycastHit hitInfo;

    // Update is called once per frame
    void Update () {

        // IF the player shoots
        if(Input.GetButton("Fire1") && Time.time > nextTimeToFire){
            nextTimeToFire = Time.time + 1.5f/fireRate;
            CmdShoot();
        }
		
	}

    [Client]
    void CmdShoot(){

        if(!isLocalPlayer){
            return;
        }

        // Shoot a ray starting at the position of the player's camera
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range)){

            CmdOnShoot(hitInfo.point, hitInfo.normal);

            // If we hit a player, return
            if(hitInfo.collider.tag == "Player"){
                return;
            }

            // If we have hit an enemy, call a command method to take damage.
            if(hitInfo.collider.tag == "Enemy"){
                enemy = hitInfo.transform.GetComponent<Enemy>();

                if(enemy != null)
                    enemy.CmdTakeDamage(damage);
            }

            if(hitInfo.rigidbody != null){
                hitInfo.rigidbody.AddForce(hitInfo.normal * hitForce);
            }
        }
    }

    // this method gets called on the server when a player shoots
    [Command]
    void CmdOnShoot(Vector3 point, Vector3 normal){
        RpcGunParticulars(point, normal);
    }

    // Called on all clients (players)
    [ClientRpc]
    void RpcGunParticulars(Vector3 point, Vector3 normal){

        //fire the gun
        gunParticles.Play();  

        // Instatiate hit effects
        GameObject explositions = Instantiate(dieEffect, point, Quaternion.LookRotation(normal));

        // Destroy the hit effect after 2 seconds
        Destroy(explositions, 2f);
    }

}
