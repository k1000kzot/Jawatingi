using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerDamagable
{
    private static int STATE_NORMAL = 0;
    private static int STATE_INMOVIL = 1;

    [HideInInspector]
    public Rigidbody2D rb;
    [Space]
    public SpriteRenderer sp;

    public BoxCollider2D _meleeAtackR;
    public BoxCollider2D _meleeAtackL;

    public CHealthManager hpmg;

    [HideInInspector]
    public int _state;

    [Space]

    [HeaderAttribute("Variables")]

    public float _xSpeed;
    public float _ySpeed;
    public bool _canMove = true;

    public float _collisionTimer;
    public float _attackTimer;

    private bool _lastFrameFlip = false;

    public bool _atacking = false;

    public Coroutine _activeCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hpmg = GetComponent<CHealthManager>();
        _meleeAtackR.enabled = false;
        _meleeAtackL.enabled = false;
    }

    public void SetState(int aState)
    {
        _state = aState;
    }

    public void OnHit(int dmg)
    {
        hpmg.LessHP(dmg);
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");    
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(xRaw, yRaw);

        if (_state == STATE_INMOVIL)
            return;

        else if (_state == STATE_NORMAL)
        {
            Walk(dir);

            if (dir.x > 0)
            {
                sp.flipX = false;
                _lastFrameFlip = false;
            }
            else if (dir.x < 0)
            {
                sp.flipX = true;
                _lastFrameFlip = true;
            }
            else
            {
                sp.flipX = _lastFrameFlip;
            }

            if (Input.GetKeyDown(KeyCode.I))
                Attack();
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!_canMove)
            return;

        rb.velocity = new Vector2(dir.x * _xSpeed, dir.y * _ySpeed);
    }

    private void Attack()
    {
        if (_atacking == false)
            _activeCoroutine = StartCoroutine(AtackRoutine());
    }

    private IEnumerator AtackRoutine()
    {
        _atacking = true;
        rb.velocity = Vector2.zero;
        SetState(STATE_INMOVIL);

        yield return new WaitForSeconds(_collisionTimer);

        if (_lastFrameFlip == false)
            _meleeAtackR.enabled = true;
        else
        {
            _meleeAtackL.enabled = true;
        }

        yield return new WaitForSeconds(_attackTimer);

        _meleeAtackL.enabled = false;
        _meleeAtackR.enabled = false;

        SetState(STATE_NORMAL);

        _atacking = false;

        yield return null;
    }
}
