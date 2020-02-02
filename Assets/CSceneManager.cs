using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour
{
    public static CSceneManager Inst

    {
        get
        {
            if (_inst == null)
                return new GameObject("SceneManager").AddComponent<CSceneManager>();
            return _inst;
        }
    }

    private static CSceneManager _inst;

    // Start is called before the first frame update
    void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _inst = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadScreen(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void LoadSceneAdditive(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
}
