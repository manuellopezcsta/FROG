using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public Button[] Buttons;
    public MMVariables MMVariables;

    // Para cargar los botones en el menu.
    private int _currentMaxLv;

    // Para cargar el volumen de forma persistente.
    public Slider VolSlider;
    

    void Start()
    {
        LoadVolumePreference();
        FixTheTimerIfTheGameExitIncorrectly();
        //MMVariables = GameObject.Find("KeepMenuVariables").GetComponent<MMVariables>();
    }

    public void PlayGame() // Es publica para llamarla desde el Boton.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void SelectLevel()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene("Level" + ButtonName);
    }

    public void CheckButtonLvStatus()
    {
        if(MMVariables.gamemode == "normal")
        {
            _currentMaxLv = PlayerPrefs.GetInt("levelPlayed", 1);
        }

        if(MMVariables.gamemode == "puzzle")
        {
            _currentMaxLv = PlayerPrefs.GetInt("levelPlayedP", 1);
        }

        if(MMVariables.gamemode == "speedrun")
        {
            _currentMaxLv = PlayerPrefs.GetInt("levelPlayedS", 1);
        }

        
        //Debug.Log("Current Max: " + _currentMaxLv);
        //Debug.Log(Buttons.Length); // Si hay 2 niveles el lenght es 2 en unity..
        for (int x = 0; x <= Buttons.Length-1 ; x++)
        {
            //Debug.Log("Max: " + _currentMaxLv + " X: " + x);
            if(_currentMaxLv > x)
            {
                Buttons[x].interactable = true;
            } else
            {
                {
                    Buttons[x].interactable = false;
                    //Debug.Log("Disabled " + x + " Button");
                }
            }
        }
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("levelPlayed", 1);
        PlayerPrefs.SetInt("levelPlayedP", 1);
        PlayerPrefs.SetInt("levelPlayedS", 1);
        Debug.Log("Data Reseted");
    }

    void LoadVolumePreference()
    {
        float _holder = PlayerPrefs.GetFloat("MMVol", 0.5f);
        VolSlider.value =  _holder; // Tratamos de obtenerla y si no existe , le asignamos uno de 0.5
        //Debug.Log(VolSlider.value);        
    }

    public void SetVolumePreference()
    {
        float _sliderValue = VolSlider.value;
        PlayerPrefs.SetFloat("MMVol", VolSlider.value);
    }

    void FixTheTimerIfTheGameExitIncorrectly()
    {
        PlayerPrefs.SetFloat("TotalTime", 0);
    }
}
