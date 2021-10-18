using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField]
    KeyCode keyRestart;
    
    [SerializeField]
    KeyCode MenuKey;


    private GameManager GM;
    private GameObject MMVars; // Destruimos el MMVars para que no se creen problemas cuando se crea uno nuevo.

    // Update is called once per frame

    void Start()
    {
        GM = GetComponent<GameManager>();
        MMVars = GameObject.Find("KeepMenuVariables");
    }
    void Update()
    {
        if(Input.GetKey(keyRestart) && GM.DevMode)
        {
            ScoreCounter.scoreValue = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKey(MenuKey) && GM.DevMode) // Para volver al menu
        {
            Destroy(MMVars);
            ScoreCounter.scoreValue = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLv()
    {
        ScoreCounter.scoreValue = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Destroy(MMVars);
        ScoreCounter.scoreValue = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
