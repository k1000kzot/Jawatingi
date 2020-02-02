using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public GameObject sprite;
    AudioSource audioS;
    public AudioClip lanzarD;
    public AudioClip choque;
    // Start is called before the first frame update
    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.PlayOneShot(lanzarD);
        Destroy(gameObject, 8);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-velocidad * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioS.PlayOneShot(choque);
           
            //GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            sprite.SetActive(false);
            Destroy(gameObject,0.5f);
        }
    }

    IEnumerator Destruir(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}
