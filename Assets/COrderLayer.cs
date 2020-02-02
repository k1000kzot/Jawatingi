using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COrderLayer : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer _rend;
    public int _offset = 0;

    void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rend.sortingOrder = Mathf.RoundToInt(-this.transform.position.y + _offset);
    }
}
