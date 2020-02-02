using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{

    public bool atacando;
    bool jugadorEnRango;
    
    public int _dmg;
    // Start is called before the first frame update
    void Start()
    {
        atacando = false;
        jugadorEnRango = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Bala")
        {

            if (collision.gameObject.tag == "Player")
            {

                
                if (collision != null)
                {
                    object objPlayer = collision.gameObject.GetComponent(typeof(IPlayerDamagable));

                    if (objPlayer != null)
                    {
                        (objPlayer as IPlayerDamagable).OnHit(_dmg);
                    }
                }
                print("balaaaaaaaa");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        jugadorEnRango = false;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(atacando == true)
        {
            
            if (collision != null)
            {
                object objPlayer = collision.gameObject.GetComponent(typeof(IPlayerDamagable));

                if (objPlayer != null)
                {
                    (objPlayer as IPlayerDamagable).OnHit(_dmg);
                }
            }

            atacando = false;
        }
    }



}
