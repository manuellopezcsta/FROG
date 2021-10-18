using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    static bool AudioBegin = false;
    public AudioSource ASource;

    public static MusicPlayer instance;


    // Para el volumen dentro del juego
    public Slider VolSliderIGM;

    void Start()
    {
        ASource = GetComponent<AudioSource>();        
    }

    void Awake()
    { // Para evitar muchos audios a la vez.
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            LoadVolumePreference();
        }else
        {
            LoadVolumePreference();
            DestroyImmediate(this);
        }

        if (!AudioBegin)
        {
            ASource.Play();
            DontDestroyOnLoad (gameObject);
            AudioBegin = true;
        } 
    }

    void Update () {
        if(Application.loadedLevelName == "MainMenu")
        {
            ASource.Stop();
            AudioBegin = false;
            Destroy(gameObject);
        }
    }

    void LoadVolumePreference()
    {
        float _holder = PlayerPrefs.GetFloat("IGMVol", 0.5f);
        VolSliderIGM.value =  _holder; // Tratamos de obtenerla y si no existe , le asignamos uno de 0.5
        //Debug.Log(VolSliderIGM.value);        
    }

    public void SetVolumePreferenceIGM()
    {
        float _sliderValue = VolSliderIGM.value;
        PlayerPrefs.SetFloat("IGMVol", VolSliderIGM.value);
    }

    public void ChangeVolumeIGM()
    {
        ASource.volume = VolSliderIGM.value;
    }
}