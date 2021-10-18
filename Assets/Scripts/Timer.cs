using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text text;
    float theTime;
    public float speed = 1;
    public bool playing = false;
    public GameManager GM;

    // Para el tiempo total
    float TotalTime = 0;

    // Use this for initialization
    void Start () 
    {
        text = GetComponent<Text>();
    }
 
    // Update is called once per frame
    void Update () 
    {
        if (playing == true)
        {
            theTime += Time.deltaTime * speed;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        playing = false;
        text.color = Color.yellow;

        string _PlayerPrefName = "Level" + GM.GetCurrentLvIndex() + "time";
        ////string _PlayerPrefNameTime = "Level" + GM.GetCurrentLvIndex() + "timeFLOAT";
        //Debug.Log("PLAYERPREFNAME " + _PlayerPrefName);
        PlayerPrefs.SetString(_PlayerPrefName,text.text);
        ////PlayerPrefs.SetFloat(_PlayerPrefNameTime, Mathf.Floor(theTime));
        //Debug.Log(PlayerPrefs.GetString(_PlayerPrefName, "null"));
        ////Debug.Log(PlayerPrefs.GetFloat(_PlayerPrefNameTime, 0));
        // Sumamos al tiempo total
        //TotalTime += Mathf.Floor(theTime);
        TotalTime = PlayerPrefs.GetFloat("TotalTime", 0); // We get the previous time.
        TotalTime += theTime; // You add the new time.
        Debug.Log("Level took: " + Mathf.Floor(theTime) + " seconds");
        PlayerPrefs.SetFloat("TotalTime", TotalTime); // We save the new time.
    }
}