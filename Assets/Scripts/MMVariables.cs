using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MMVariables : MonoBehaviour
{

    public string gamemode = "normal";
    //public static MMVariables instance;

    // normal
    // puzzle
    // speedrun


    public TextMeshProUGUI Explanation; // Se llama asi el componente xq es de Ui, si no fuera Ui seria TextMeshPro solo.


    void Awake() 
    {
        /*
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }else
        {
            DestroyImmediate(this);
        }*/
        DontDestroyOnLoad(this);                
    }

    public void SetGameMode()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        if(ButtonName == "Normal Button")
        {
            gamemode = "normal";
        }
        if(ButtonName == "Puzzle Button")
        {
            gamemode = "puzzle";
        }
        if(ButtonName == "SpeedRun Button")
        {
            gamemode = "speedrun";
        }
        
        // Mostramos el Elegido

        Debug.Log("Current Gamemode: " + gamemode);

        switch (gamemode)
        {
            default:
                Explanation.text = "WHUUUUUUUUT? Error";
                break;
            case "normal":
                Explanation.text = "Normal Mode: You have no jumping or time limits.";
                break;
            case "puzzle":
                Explanation.text = "Puzzle Mode: You have a limited ammount of jumps to complete the level.";
                break;
            case "speedrun":
                Explanation.text = "Speedrun Mode: You have a timer that keeps track of your time.";
                break;
        }
    }
}
