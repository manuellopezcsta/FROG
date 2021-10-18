using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    Animator anim;
    AudioSource aSource;
    int banana = 0;

    public AudioClip RuidoGemaObtenida;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        aSource = GetComponent<AudioSource> ();        
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
    	anim.SetTrigger("obtenida");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (banana < 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Cherry2")){
            aSource.PlayOneShot(RuidoGemaObtenida,1);
            Destroy(gameObject, 1);
            ScoreCounter.scoreValue += 1;
            banana ++;
        }
    }
}
