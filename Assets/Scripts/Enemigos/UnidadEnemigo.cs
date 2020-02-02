using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadEnemigo : MonoBehaviour, IEnemyDamagable
{
    public bool vivo;
    public float saludMaxima;
    public float saludActual;
    public bool golpeRecibido = false;
    
    // Start is called before the first frame update

    public void OnHit(int dmg, int type)
    {
        Debug.Log("me pegaron wey");
        saludActual = saludActual - dmg;
        golpeRecibido = true;
    }

    private void Awake()
    {
        saludActual = saludMaxima;
    }

    void Start()
    {
        vivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(saludActual <= 0)
        {
            vivo = false;
            
        }
    }

    
}
