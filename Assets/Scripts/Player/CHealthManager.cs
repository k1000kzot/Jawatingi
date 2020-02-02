using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHealthManager : MonoBehaviour
{
    public int _maxHP = 1;
    public int _currentHP;

    public Image[] _hearths;
    public Sprite _fullHearth;
    public Sprite _hollowHearth;

    public bool _firstCoroutineEnded = false;

    public void Awake()
    {
        MaxHP();
    }

    public void Update()
    {
        for (int i = 0; i < _hearths.Length; i++)
        {
            if(i < _currentHP)
            {
                _hearths[i].sprite = _fullHearth;
            }

            else
            {
                _hearths[i].sprite = _hollowHearth;
            }

            if (i < _maxHP)
            {
                _hearths[i].enabled = true;
            }
            else
            {
                _hearths[i].enabled = false;
            }
        }
    }

    public void NowStart()
    {
        _firstCoroutineEnded = true;
    }

    public void MaxHP()
    {
        _currentHP = _maxHP;
    }

    public int GetHP()
    {
        return _currentHP;
    }

    public int GetMaxHP()
    {
        return _maxHP;
    }

    public float GetPercent()
    {
        return _currentHP / (float)_maxHP;
    }

    public void AddHP(int add)
    {
        _currentHP = Mathf.Clamp(_currentHP + add,0,_maxHP);
    }

    public void LessHP(int less)
    {
        _currentHP = Mathf.Clamp(_currentHP -less, 0, _maxHP);
    }

    public void AddPercent(float percent)
    {
        percent = Mathf.Clamp(percent,0, 100);
        AddHP(Mathf.FloorToInt(_maxHP * (percent / 100f)));
    }

    public void LessPercent(float percent)
    {
        percent = Mathf.Clamp(percent, 0, 100);
        LessHP(Mathf.FloorToInt(_maxHP * (percent / 100f)));
    }

    public bool HasHP()
    {
        return _currentHP > 0;
    }

    public void ZeroHP()
    {
        _currentHP = 0;
    }
}
