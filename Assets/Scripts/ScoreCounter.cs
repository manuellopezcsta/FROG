using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text score;
    public GameManager GM;
    public Text Jumps;

    // Start is called before the first frame update
    void Start()
    {
        score.GetComponent<Text> ();
        score.text = "Cherries :" + scoreValue;
        scoreValue = 0;

        // Para Saltos.

        Jumps.text = "JUMPS: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue <= GM.CherriesOnLv){
            score.text = "Cherries :" + scoreValue + "/" + GM.CherriesOnLv;
        }

        if(scoreValue == GM.CherriesOnLv) // Le decimos al GameManager que mande a detener el timer.
        {
            GM.StopClock = true;
        }

        // Para el Counter de Saltos.
        Jumps.text = "JUMPS: " + GM.CurrentJumpsDone + "/" + GM.currentLvMaxJumps;        
    }
}
