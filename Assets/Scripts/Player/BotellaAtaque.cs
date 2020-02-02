using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotellaAtaque : MonoBehaviour
{
    public int _dmg;
    public float velocidad;

    AudioSource audioS;
    public AudioClip botellaRompe;
    public AudioClip lanzarAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(lanzarAudio);
        Destroy(gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.modoFantasmalActivo == true)
        {
            if (collision.gameObject.layer == 8)
            {
                if (collision != null)
                {
                    object objPlayer = collision.gameObject.GetComponent(typeof(IEnemyDamagable));

                    if (objPlayer != null)
                    {
                        (objPlayer as IEnemyDamagable).OnHit(_dmg, 1);
                        audioS.PlayOneShot(botellaRompe);
                        //Animacion Botella
                        this.enabled = false;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        Destroy(gameObject, 1);
                    }

                   
                }
            }


        }
        else
        {
            if (collision.gameObject.layer == 9)
            {
                if (collision != null)
                {
                    object objPlayer = collision.gameObject.GetComponent(typeof(IEnemyDamagable));

                    if (objPlayer != null)
                    {
                        (objPlayer as IEnemyDamagable).OnHit(_dmg, 1);
                        audioS.PlayOneShot(botellaRompe);
                        this.enabled = false;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        Destroy(gameObject, 1);
                    }
                }
            }
        }
    }
}
