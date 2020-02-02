using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reinicio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BotonRestart();
           
        }
            
    }

    public void BotonRestart()
    {
        SceneManager.LoadScene(1);
    }
    public void BotonSalir()
    {
        Application.Quit();
    }
}
