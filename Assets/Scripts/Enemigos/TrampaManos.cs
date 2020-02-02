using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaManos : MonoBehaviour
{
    public int _dmg = 1;
    public GameObject spriteManos;
    BoxCollider2D bxColl;

    bool dañarAPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        print("trampa");
        bxColl = gameObject.GetComponent<BoxCollider2D>();
        StartCoroutine(ActivarTrampa(Random.Range(1, 4)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivarTrampa(float activar)
    {
        yield return new WaitForSeconds(activar);
        print("trampa");
        dañarAPlayer = true;
        bxColl.enabled = true;
        spriteManos.SetActive(true);
        yield return new WaitForSeconds(Random.Range(2.5f,5));
        bxColl.enabled = false;
        spriteManos.SetActive(false);

        StartCoroutine(ActivarTrampa(Random.Range(2f, 5)));
        
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("player");
            if(dañarAPlayer == true)
            {
                if (collision != null)
                {
                    object objPlayer = collision.gameObject.GetComponent(typeof(IPlayerDamagable));

                    if (objPlayer != null)
                    {
                        (objPlayer as IPlayerDamagable).OnHit(_dmg);
                        dañarAPlayer = false;
                    }
                }
            }
            
        }
    }

    
}
