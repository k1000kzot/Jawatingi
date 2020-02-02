using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelManager : MonoBehaviour
{
    public CCheckTrigger _gameFinished;

    public bool _finished = false;

    public static CLevelManager Inst
    {
        get
        {
            if (_inst == null)
                return new GameObject("LevelManager").AddComponent<CLevelManager>();
            return _inst;
        }
    }
    private static CLevelManager _inst;
    void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _inst = this;
    }

    private void Start()
    {
        CTransitionManager.Inst.SetFadeOutFlag();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameFinished._checked == true && _finished == false)
        {
            Debug.Log("termino el game");
            _finished = true;
            StartCoroutine(EndingRoutine());
        }
    }

    public IEnumerator EndingRoutine()
    {
        yield return new WaitForSeconds(0.7f);

        CTransitionManager.Inst.CreateTransition("Fade");

        while (CTransitionManager.Inst.IsScreenCovered() != true)
        {
            yield return null; //esperar 1 frame

            Debug.Log("Nigga");
        }
        CSceneManager.Inst.LoadScreen("Final");

        yield return null;
    }
}
