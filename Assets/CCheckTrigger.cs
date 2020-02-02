using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckTrigger : MonoBehaviour
{
    public bool _checked = false;

    public void OnTriggerEnter2D(Collider2D _playerIsHereTrigger)
    {
        Debug.Log("entrar entra");
        if (_playerIsHereTrigger.CompareTag("Player"))
        {
            Checked();
            Debug.Log("Cosa");
        }
    }

    public void Checked()
    {
        _checked = true;
    }

    public void UnCheked()
    {
        _checked = false;
    }
}
