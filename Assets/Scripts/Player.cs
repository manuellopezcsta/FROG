using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private LineRenderer lr;    
    private Animator animator;

    public GameManager GM;

    [SerializeField] private float Launch_Force = 200;
    private Vector2 newPos;
    private Vector2 OldPos;
    private bool _isDragging;

    // Para la barra de fuerza
    public Image BarraFuerza;

    // Valores de limites del Line renderer
    [SerializeField] float _distance;
    float range1Min = 0f;
    public float range1Max = 3.70f;

    // Para controlar que este en el piso.
    [SerializeField] private bool grounded = true;

    // Para ver adonde mira
    private bool IsLookingRight = false;

    // Para forzar un restart al tocar un pincho
    public bool NeedARestart = false;

    public Vector2 PreJumpPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();

        lr.enabled = false;

        PreJumpPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDragging)
        {
            CalculateLineRendererPosition();
        }        

        if(rb.velocity.y == 0  && isGrounded())
            {
                grounded = true;
                animator.SetBool("isJumping", false);
                animator.SetFloat("velocity", 0);
                animator.Play("Frog1");
            } else {
                grounded = false;
                animator.SetBool("isJumping", true);
                animator.SetFloat("velocity", rb.velocity.y);                
            }
        
    
    }

    private void OnMouseDrag(){
        _isDragging = true;
        if(grounded)
        {
            _distance = Vector2.Distance(lr.GetPosition(0), lr.GetPosition(1)); // 3.68 limitar.
            if(_distance >= range1Max)
            {
                _distance = range1Max;
            }
            lr.enabled = true;
            BarraFuerza.fillAmount = TransformToRange();

            LookAtTheMouse(); // Que Mire hacia el Mouse antes del salto.
        }
    } 

    private void OnMouseUp(){
        if(grounded)
        {
            // Guardamos su posicion para usarla en la funcion de deshacer movimientos.
            PreJumpPosition = transform.position;
            // Calculos para lanzarlo
            OldPos = new Vector2(transform.position.x , transform.position.y);
            Vector2 dirToNewPos = newPos - OldPos; // Calculamos el vector para lanzarlo.
            //Debug.Log(dirToNewPos);
            rb.AddForce(dirToNewPos * Launch_Force); // Multiplicamos para que sea mas rapido.
            rb.gravityScale = 1;
            //_BirdWasLaunched = true;
            lr.enabled = false;
            BarraFuerza.fillAmount = 0f;
            // Agregamos el salto al jump counter.
            AddToJumpCounter();
        }
        _isDragging = false;
    }

    private bool isGrounded()
    {
        return transform.Find("Ground Check").GetComponent<GroundCheck>().isGrounded;
    }

    float TransformToRange()
    {
        float range2Min = 0f;
        float range2Max = 1f;
        
        float normalizedValue = Mathf.InverseLerp(range1Min, range1Max, _distance);
        float result = Mathf.Lerp(range2Min, range2Max, normalizedValue);
        return result;
    }

    void CalculateLineRendererPosition()
    {
            Vector2 mousePos = Input.mousePosition;
            newPos = Camera.main.ScreenToWorldPoint(mousePos);

            //Vector2 _desiredPosition = mousePos;
            if(_distance >= range1Max)
            {
                Vector2 _startingPos = new Vector2(transform.position.x , transform.position.y); // Tomo la posicion del jugador.
                Vector2 _direction = newPos - _startingPos; // Obtengo una direccion restando la posicion del mouse - la del jugador
                _direction.Normalize(); // La normalizo para poder multiplicarlo.
                newPos = _startingPos + (_direction * range1Max); // Cambio la posicion del mouse para que sea dentro del limite.
            }
            
            // De donde esta el pajaro a la linea.
            lr.SetPosition(1, newPos);
            lr.SetPosition(0, transform.position); // Si la linea se dibuja alrevez, cambiar el 0 y el 1.
    }

    void LookAtTheMouse(){
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 lTemp = transform.localScale;
        lTemp.x *= -1;

        if(transform.position.x < mousePos.x && !IsLookingRight)
        {
            transform.localScale = lTemp;
            IsLookingRight = true;
        }
        if(transform.position.x > mousePos.x && IsLookingRight)
        {
            transform.localScale = lTemp;
            IsLookingRight = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 _velocity = rb.velocity; // x si colisiona con una pared q rebota.
        if(other.gameObject.tag == "Spike" && GM.finishedLv == false)
        {
            //Debug.Log("Toco Spike");
            NeedARestart = true;
        }

        if(other.gameObject.tag == "BouncyWall")
        {
            //Debug.Log("Toco Pared Bouncy");
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;

            if(IsLookingRight)
            {
                IsLookingRight = false;
            } else
            {
                IsLookingRight = true;
            }

            //rb.velocity = Vector2.Reflect(_velocity, other.contacts[0].normal); // Usaria este metodo , pero no me anda q-q.
        }

    }

    void AddToJumpCounter()
    {
        GM.CurrentJumpsDone += 1;

        if(GM.CurrentJumpsDone > GM.currentLvMaxJumps && GM.currentGamemode == "puzzle")
        {
            Debug.Log(" Se realizaron demaciados saltos, reintenta el nivel.  " + GM.CurrentJumpsDone + "/" + GM.currentLvMaxJumps );
            GM.YouLostMenu.SetActive(true);
            GM.PauseGame(); // Pausamos para que no intenten saltar.
        }
    }
}
