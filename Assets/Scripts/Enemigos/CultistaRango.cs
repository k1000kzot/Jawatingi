using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistaRango : MonoBehaviour
{
    public SpriteRenderer _sr;

    GameObject player;
    float distanciaPlayerX;
    bool activarse = false;
    bool atacar = true;
    bool moverse = true;
    bool activarMuerte = true;
    bool escapando;

    float distanciaPlayerY;
    public float distanciaParaActivarse;
    public float _offsetY;

    public float distanciaAtaque;

    public float velocidadMovimientoX;
    public float velocidadMovimientoY;

    public GameObject bala;
    public BoxCollider2D collAtaque;
    public AtaqueEnemigo ataqueScript;
    public UnidadEnemigo unidad;

    AudioSource audioS;
    public AudioClip Muerte;
    float escalaX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        escalaX = gameObject.transform.localScale.x;

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
                    if(escapando == false)
                    {
                        if (distanciaPlayerX > distanciaAtaque)
                        {

                            transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x, gameObject.transform.position.y), velocidadMovimientoX * Time.deltaTime);

                        }
                        else
                        {
                            if (distanciaPlayerX < distanciaAtaque / 2)
                            {
                                //transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x + distanciaAtaque, gameObject.transform.position.y), velocidadMovimientoX * Time.deltaTime);
                                escapando = true;
                            }

                        }
                        if (distanciaPlayerY > 0.3f)
                        {

                            transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(transform.position.x, player.transform.position.y), velocidadMovimientoY * Time.deltaTime);

                        }

                    }
                    else
                    {
                        if (distanciaPlayerX < distanciaAtaque)
                        {
                            transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x + distanciaAtaque, gameObject.transform.position.y), velocidadMovimientoX * Time.deltaTime);
                        }
                        if(distanciaPlayerX >= distanciaAtaque)
                        {
                            escapando = false;
                        }
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

               // ataque
               if(escapando == false)
                {
                    if(distanciaPlayerX > distanciaAtaque / 2 && distanciaPlayerX <= distanciaAtaque && distanciaPlayerY <= 0.3f)
                    {
                        if(atacar == true)
                        {
                            Ataque();
                        }
                    }
                }


               if(unidad.golpeRecibido == true)
                {
                    StartCoroutine(DañoRecibido(0.5f));
                    moverse = false;
                    unidad.golpeRecibido = false;
                }

            }
            else
            {
                if (activarMuerte == true)
                {
                    audioS.PlayOneShot(Muerte);
                    Destroy(gameObject,1);
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
         print("Ataque Rango");
        Instantiate(bala, transform.position, Quaternion.identity);
        atacar = false;
        moverse = false;
        StartCoroutine(ReiniciarAtaque(1, 1.5f));
    }

    IEnumerator ReiniciarAtaque(float timeMov, float timeAtaq)
    {
        yield return new WaitForSeconds(timeMov);        
        moverse = true;

        yield return new WaitForSeconds(timeAtaq);
        atacar = true;
    }

    IEnumerator DañoRecibido(float time)
    {
        yield return new WaitForSeconds(time);
        moverse = true;
    }
}
