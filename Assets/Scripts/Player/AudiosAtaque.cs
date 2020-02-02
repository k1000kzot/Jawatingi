using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosAtaque : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip grito;
    public AudioClip ataque;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SonidoGrito()
    {
        audioS.PlayOneShot(grito);
    }

    public void SonidoAtaque()
    {
        audioS.PlayOneShot(ataque);
    }
}
