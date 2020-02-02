using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUI : MonoBehaviour
{
    public PlayerMovement _player;

    public Canvas _canvas;

    public Image[] _hearths;

    public Coroutine _activeCoroutine;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void TurnOff()
    {
        //_canvas.enabled = false;
        for (int i = 0; i <= _hearths.Length - 1; i++)
        {
            _hearths[i].enabled = false;
        }
    }

    public void TurnOn()
    {
        //_canvas.enabled = true;
        for (int i = 0; i <= _hearths.Length - 1; i++)
        {
            _hearths[i].enabled = true;
        }
    }
}
