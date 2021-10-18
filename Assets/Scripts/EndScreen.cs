using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    float TotalTime = 0;
    public TextMeshProUGUI TimeText;
    public GameObject TimerTextBox;

    MMVariables MMVariables;
    GameObject MMVarHolder;

    // Start is called before the first frame update
    void Start()
    {
        MMVarHolder = GameObject.Find("KeepMenuVariables");
        MMVariables = MMVarHolder.GetComponent<MMVariables>();

        string _gamemode = MMVariables.gamemode;
        CalculateTotalTime();

        if(_gamemode == "speedrun")
        {
            TimerTextBox.SetActive(true);
        } else
        {
            TimerTextBox.SetActive(false);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateTotalTime()
    {
        TotalTime = PlayerPrefs.GetFloat("TotalTime", 0f);
        Debug.Log("TOTAL TIME: " + TotalTime);

        string hours = Mathf.Floor((TotalTime % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((TotalTime % 3600) / 60).ToString("00");
        string seconds = (TotalTime % 60).ToString("00");

        TimeText.text = "Your time: " + hours + ":" + minutes + ":" + seconds;
    }

    public void BackToMenu()
    {
        Destroy(MMVarHolder);
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("TotalTime", 0);
        SceneManager.LoadScene(0);
    }
}
