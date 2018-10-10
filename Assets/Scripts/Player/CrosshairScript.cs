using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CrosshairScript : MonoBehaviour {

    public Texture2D crosshair;
    Rect position;

    void OnGUI(){
        float w = crosshair.width / 2;
        float h = crosshair.height / 2;

        position = new Rect((Screen.width - w)/2, (Screen.height - h)/2, w, h);

        GUI.DrawTexture(position, crosshair);
    }
}
