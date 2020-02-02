using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelManager : MonoBehaviour
{
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
        
    }
}
