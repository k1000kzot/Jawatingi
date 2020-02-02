using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEndRoutine : MonoBehaviour
{
    public static int STATE_FADEIN = 0;
    public static int STATE_HOLD = 1;
    public static int STATE_FADEOUT = 2;
    public static int STATE_DONE = 3;

    public float _fadeInTime = .5f;
    public float _fadeOutTime = .3f;

    public bool _isDefault = false;
    public float _minHoldTime = 1;
    protected bool _fadeOutFlag = false;
    public CanvasGroup[] _group;
    public CanvasGroup _title;

    public int _state;

    private Coroutine _activeCoroutine;

    void Awake()
    {
        for (int i = 0; i <= _group.Length - 1; i++)
        {
            _group[i].alpha = 0;
        }
        _title.alpha = 0;

        SetState(STATE_FADEIN);
        CTransitionManager.Inst.SetFadeOutFlag();
    }

    public void SetState(int aState)
    {
        _state = aState;

        if (_state == STATE_FADEIN)
        {
            StopAllCoroutines();
            _activeCoroutine = StartCoroutine(FadeRoutine());
        }

        if (_state == STATE_DONE)
        {
            Destroy(this.gameObject);
        }
    }

    public bool GetFadeOutFlag()
    {
        return _fadeOutFlag;
    }

    public void SetFadeOutFlag(bool flag)
    {
        _fadeOutFlag = flag;
    }


    public virtual int GetState()
    {
        return _state;
    }

    public virtual bool IsDone()
    {
        return _state == STATE_DONE;
    }

    protected IEnumerator FadeRoutine()
    {

        yield return new WaitForSeconds(2);

        float elapsedTime = 0;

        while (elapsedTime < _fadeInTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / _fadeInTime;

            _title.alpha = t;
            //interpolar-animar

            yield return null;
        }

        Debug.Log("Start");

        yield return new WaitForSeconds(2);

        elapsedTime = 0;

        while (elapsedTime < _fadeOutTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / _fadeOutTime;

            //interpolar-animar

            _title.alpha = 1 - t;

            yield return null;
        }

        yield return new WaitForSeconds(2);

        elapsedTime = 0;

        for (int i = 0; i <= _group.Length - 1; i++)
        {
            elapsedTime = 0;

            while (elapsedTime < _fadeInTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = elapsedTime / _fadeInTime;

                _group[i].alpha = t;
                //interpolar-animar

                yield return null;
            }

            Debug.Log("Start2");
        }

        SetState(STATE_HOLD);

        //float holdTimeStamp = Time.time;
        //while (!(_fadeOutFlag && (Time.time - holdTimeStamp) > _minHoldTime))
        //yield return null;

        SetState(STATE_FADEOUT);

        for (int i = 0; i <= _group.Length - 1; i++)
        {
            elapsedTime = 0;

            while (elapsedTime < _fadeOutTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = elapsedTime / _fadeOutTime;

                //interpolar-animar

                _group[i].alpha = 1 - t;

                yield return null;
            }
        }

        CTransitionManager.Inst.CreateTransition("Fade");

        Application.Quit();

        while (CTransitionManager.Inst.IsScreenCovered() != true)
        {
            yield return null; //esperar 1 frame

            Debug.Log("Nigga");
        }

        //terminar game

        _activeCoroutine = null;
        SetState(STATE_DONE);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_state);
    }
}