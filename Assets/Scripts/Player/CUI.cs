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

    public float _timer = 2f;
    public float _currentTimer = 0;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    private void Update()
    {
        _currentTimer += Time.deltaTime;

        if (_currentTimer >= _timer && _canvas.enabled == false)
            _canvas.enabled = true;
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
