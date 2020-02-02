using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static bool modoFantasmalActivo = false;
    public GameObject pausaUI;
    bool juegoPausado;
    
    // Start is called before the first frame update
    void Start()
    {
        modoFantasmalActivo = false;
        juegoPausado = true;
    }

    // Update is called once per frame
    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            juegoPausado = !juegoPausado;
            if(juegoPausado == true)
            {
                pausaUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pausaUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void BotonReturn()
    {
        pausaUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void BotonExit()
    {

    }
}
