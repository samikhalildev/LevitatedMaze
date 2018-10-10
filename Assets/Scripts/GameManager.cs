using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static Dictionary<string, Player> players = new Dictionary<string, Player>();
    static Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();

    public static int enemiesLeft;

    /*
     * PLAYER
     */

    public static void AddPlayer(string _PlayerID, Player _player){
        players.Add(_PlayerID, _player);
    }

    public static void RemovePlayer(string _PlayerID){
        players.Remove(_PlayerID);
    }

    public static Player GetPlayer(string _PlayerID){
        return players[_PlayerID];
    }



    void OnGUI() {
        GUI.color = Color.red;

        GUILayout.BeginArea(new Rect(50, 150, 500, 500));
        GUILayout.BeginVertical();

        // If there are players, show number of enemies and players connected
        if(players.Count >= 1){
            GUILayout.Label("Players connected: " + players.Count);
            GUILayout.Label("Enemies Left: " + enemiesLeft);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

}
