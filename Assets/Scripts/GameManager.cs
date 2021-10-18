using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CherriesOnLv ;
    public int NumberOfLevels;

    private Coin[] _Cherries;

    private Player _player;
    private RestartButton _RestartButton;

    [SerializeField]
    KeyCode keyUndo;

    // Para el timer
    [SerializeField] Timer timer;
    public GameObject TimerHolder; // Para que no se vea.
    public bool StopClock = false;
    bool clockwasStopped = false;

    // Para el Gamemode
    public MMVariables MMVariables;
    public GameObject NumOfJumps;

    // Para guardar un nivel al entrar como desbloqueado.
    private int _currentMax;

    // Para Que funcione el contador de puzzle jumping.
    int[] MaxNumOfJumpsPerLv = {2, 1, 1, 2, 3, 3, 4, 4, 4, 5, 1, 2, 2, 3, 5, 5, 1, 4, 3, 3, 5, 3, 3, 3, 3, 4, 1 }; // 27

    public int CurrentJumpsDone = 0;
    public int currentLvIndex;
    public int currentLvMaxJumps;

    // You Lost screen
    public GameObject YouLostMenu;
    public string currentGamemode = "";

    // Para Activar los Atajos de Desarrollador.
    public bool DevMode = false;

    // Para que los spike no te hagan perder si agarraste la ultima cereza
    public bool finishedLv = false;



    void Start()
    {
        NumberOfLevels = 27;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _RestartButton = GetComponent<RestartButton>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.playing = true;
        currentGamemode = PrepareGamemodeSettings();
        SaveLevel(SceneManager.GetActiveScene().buildIndex);
        currentLvIndex = SceneManager.GetActiveScene().buildIndex;
        currentLvMaxJumps = MaxNumOfJumpsPerLv[currentLvIndex-1];    // COLOCAR A MANO EN EL EDITOR JUNTO A N of LEVELS.
        //Debug.Log(" MaxNumOfJumpsPerLv" + " " + currentLvMaxJumps);
    }


    private void OnEnable()
    {
        _Cherries = FindObjectsOfType<Coin>(); // Guarda todos los pickups en un array
        CherriesOnLv = _Cherries.Length; // Obtenemos cuantos hay.
    }

    
    void Update()
    {
        if(_player.NeedARestart == true)
        {
            _RestartButton.RestartLv();
        }

        if(Input.GetKeyUp(keyUndo) && DevMode) // Uso keyUp xq sino me deletea saltos continuamente y me lo deja en 0 al counter.
        {
            _player.transform.position = _player.PreJumpPosition;
            if(CurrentJumpsDone > 0)
            {
                CurrentJumpsDone += -1;
            }
        }

        if(StopClock && !clockwasStopped)
        {
            timer.StopTimer();
            clockwasStopped = true;
        }

    }

    public int GetCurrentLvIndex()
    {
        int _holder = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("LEVEL INDEX: " + _holder);
        return _holder;
    }


    string PrepareGamemodeSettings() // Obtenemos el estado en el que se quiere jugar y luego mostamos o ocultamos componentes basado en eso.
    {
        MMVariables = GameObject.Find("KeepMenuVariables").GetComponent<MMVariables>();
        string _gamemode = MMVariables.gamemode;

        switch (_gamemode)
        {
            default:
                Debug.Log(" ERROR GAMEMODE DOES NOT EXIST");
                break;
            case "normal":
                Debug.Log(_gamemode);
                TimerHolder.SetActive(false);
                NumOfJumps.SetActive(false);
                break;
            case "puzzle":
                Debug.Log(_gamemode);
                TimerHolder.SetActive(false);
                NumOfJumps.SetActive(true);
                break;
            case "speedrun":
                Debug.Log(_gamemode);
                NumOfJumps.SetActive(false);
                TimerHolder.SetActive(true);
                break;
        }
        return _gamemode;
    }

    public void SaveLevel(int id)
    {
        if(MMVariables.gamemode == "normal")
        {
            _currentMax = PlayerPrefs.GetInt("levelPlayed", 1);
        }

        if(MMVariables.gamemode == "puzzle")
        {
            _currentMax = PlayerPrefs.GetInt("levelPlayedP", 1);
        }

        if(MMVariables.gamemode == "speedrun")
        {
            _currentMax = PlayerPrefs.GetInt("levelPlayedS", 1);
        }

        Debug.Log("Current Last Unlocked Lv: " + _currentMax);
        if((id > _currentMax) && (MMVariables.gamemode == "normal") )
        {
            PlayerPrefs.SetInt("levelPlayed",id);
        }

        if((id > _currentMax) && (MMVariables.gamemode == "puzzle") )
        {
            PlayerPrefs.SetInt("levelPlayedP",id);
        }

        if((id > _currentMax) && (MMVariables.gamemode == "speedrun") )
        {
            PlayerPrefs.SetInt("levelPlayedS",id);
        }
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}        

