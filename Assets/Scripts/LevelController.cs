using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLvIndex = 1; // Le ponemos la static tag, para que sin importar que instancia del lv controller sea, todos tienen el mismo index.(Queda guardado).
    private Coin[] _Cherries;

    public GameManager _GM;


    private void Start()
    {
        _GM = GetComponent<GameManager>();
        _nextLvIndex = _GM.GetCurrentLvIndex();
    }
    private void OnEnable()
    {
        _Cherries = FindObjectsOfType<Coin>(); // Guarda todos los enemigos en un array.
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_GM.NumberOfLevels);
        foreach (Coin cherry in _Cherries)
        {
            if(cherry != null) // Si existe todavia volve, hay alguno vivo.
            {
                return;
            }
        }

        Debug.Log("Level " +_nextLvIndex + " Completed.");
        _GM.finishedLv = true;
        
        _nextLvIndex++;

        if(_nextLvIndex >= _GM.NumberOfLevels + 1 ) // Si se llego al ultimo nivel
        {
            Debug.Log("You won.");
            Destroy(gameObject);
            SceneManager.LoadScene("PostGame");
            return;
        }
        string _nextLvname = "Level" + _nextLvIndex;
        SceneManager.LoadScene(_nextLvname);
        
    }
}

