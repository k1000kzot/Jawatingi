using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
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
            print("Daño bala");
            //GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }

    IEnumerator Destruir(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}
