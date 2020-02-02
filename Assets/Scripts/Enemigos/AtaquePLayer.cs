using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePLayer : MonoBehaviour
{
    public int _dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameController.modoFantasmalActivo == true)
        {
            if(collision.gameObject.layer == 8)
            {
                if (collision != null)
                {
                    object objPlayer = collision.gameObject.GetComponent(typeof(IEnemyDamagable));

                    if (objPlayer != null)
                    {
                        (objPlayer as IEnemyDamagable).OnHit(_dmg, 1);
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
                    }
                }
            }
        }
        
    }
}
