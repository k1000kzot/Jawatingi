using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFantasma : MonoBehaviour
{
    public SpriteRenderer _sr;
    public Animator _anim;

    public float distanciaParaActivarse;
    GameObject player;
    float distanciaPlayerX;
    bool activarse = false;
    bool atacar = true;
    bool moverse = true;
    bool activarMuerte = true;

    public float _offsetY;


    float distanciaPlayerY;

    public float velocidadMovimientoX;
    public float velocidadMovimientoY;

    float velocidadInicialX;
    float velocidadInicialY;

    public BoxCollider2D collAtaque;
    public AtaqueEnemigo ataqueScript;
    public UnidadEnemigo unidad;

    AudioSource audioS;
    public AudioClip Muerte;
    public AudioClip ataque;

    float escalaX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        escalaX = gameObject.transform.localScale.x;

        velocidadInicialX = velocidadMovimientoX;
        velocidadInicialY = velocidadMovimientoY;

        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //distanciaPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        // print(Mathf.Abs(player.transform.position.x - transform.position.x));
        distanciaPlayerX = Mathf.Abs(player.transform.position.x - transform.position.x);
        distanciaPlayerY = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (player.transform.position.y - _offsetY <= this.transform.position.y)
            _sr.sortingOrder = -1;
        else
            _sr.sortingOrder = 1;

        if (activarse == true)
        {
            if (unidad.vivo == true)
            {

                if (moverse == true)
                {

                    //Movimiento
                    if (distanciaPlayerX > 1.5f)
                    {

                        transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x, gameObject.transform.position.y), velocidadMovimientoX * Time.deltaTime);

                    }

                    if (distanciaPlayerY > 0.3f)
                    {

                        transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(transform.position.x, player.transform.position.y), velocidadMovimientoY * Time.deltaTime);

                    }


                    if (transform.position.x < player.transform.position.x)
                    {
                        gameObject.transform.localScale = new Vector3(-escalaX, gameObject.transform.localScale.y, 1);
                    }
                    else
                    {
                        gameObject.transform.localScale = new Vector3(escalaX, gameObject.transform.localScale.y, 1);
                    }


                }

                // Ataque
                if (distanciaPlayerX <= 3f && distanciaPlayerY <= 0.3f)
                {
                    if (atacar == true)
                    {
                        
                        Ataque();

                    }
                }




                //recibir daño
                if (unidad.golpeRecibido == true)
                {
                    StartCoroutine(DañoRecibido(0.5f));

                    unidad.golpeRecibido = false;
                }


            }
            else
            {
                if (activarMuerte == true)
                {
                    print("muerto");
                    audioS.PlayOneShot(Muerte);
                    Destroy(gameObject, 1);
                    activarMuerte = false;
                }

            }


        }
        else
        {
            if(distanciaPlayerX <= distanciaParaActivarse)
            {
                activarse = true;
            }
        }
    }

    public void Ataque()
    {
        _anim.SetTrigger("Hit");
        print("Ataque enemigo");
        ataqueScript.atacando = true;
        atacar = false;
        moverse = false;
        audioS.PlayOneShot(ataque);
        StartCoroutine(ReiniciarAtaque(1, 1.5f));
    }

    IEnumerator ReiniciarAtaque(float timeMov, float timeAtaq)
    {
        yield return new WaitForSeconds(timeMov);
        ataqueScript.atacando = false;
        moverse = true;
        yield return new WaitForSeconds(timeAtaq);
        atacar = true;
    }

    IEnumerator DañoRecibido(float time)
    {
        moverse = false;
        yield return new WaitForSeconds(time);
        moverse = true;
    }
}