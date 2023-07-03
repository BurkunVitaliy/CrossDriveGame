
using System;
using UnityEngine;

public class Fading : MonoBehaviour
{
    public Texture2D fading;
    private float fadeSpeed = 0.8f,  alpha = 1f, fadeDirection = -1f;

    private int drawDepth = -1000;
    private void OnGUI()
    {
        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), fading);
    }

    public float Fade(float dir)
    {
        fadeDirection = dir;
        return fadeSpeed;
    }
}
