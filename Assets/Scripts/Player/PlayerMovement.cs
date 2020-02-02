using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IPlayerDamagable
{
    private static int STATE_NORMAL = 0;
    private static int STATE_INMOVIL = 1;

    [HideInInspector]
    public Rigidbody2D rb;
    [Space]
    public Animator _anim;
    public SpriteRenderer sp;

    public BoxCollider2D _meleeAtackR;
    public BoxCollider2D _meleeAtackL;
    public BoxCollider2D _highAtackR;
    public BoxCollider2D _highAtackL;

    public CHealthManager hpmg;

    [HideInInspector]
    public int _state;

    [Space]

    [HeaderAttribute("Variables")]

    public float _xSpeed;
    public float _ySpeed;
    public bool _canMove = true;

    public float _collisionTimer;
    public float _highCollisionTimer;
    public float _attackTimer;
    public float _highAttackTimer;
    public float _highAttackCooldown;
    private float _currentCooldownTimer;
    public float _rangeAttackTimer;
    public float _rangeAttackCooldown;
    private float _currentCooldownTimerRange;

    private bool _lastFrameFlip = false;

    public bool _atacking = false;

    public Coroutine _activeCoroutine;

    public GameObject botella;
    public Transform posicionBotella;
    public Transform posicionBotella_negativo;

    public AudioSource audioPlayer;
    public AudioSource audioCaminar;
    public AudioClip caminarPlayer;
    public AudioClip golpeClip;
    bool activarCaminarSonido = true;

    public Image fadeHabilidadL;
    public Image fadeHabilidadBotella;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hpmg = GetComponent<CHealthManager>();
        _meleeAtackR.enabled = false;
        _meleeAtackL.enabled = false;
        _highAtackR.enabled = false;
        _highAtackL.enabled = false;

        _currentCooldownTimer = 0;
        _currentCooldownTimerRange = 0;
    }

    public void SetState(int aState)
    {
        _state = aState;
    }

    public void OnHit(int dmg)
    {
        hpmg.LessHP(dmg);
    }

    public void Dead()
    {
        Debug.Log("Tas muerto mu;a");
    }
    void Update()
    {
        //Debug.Log(_currentCooldownTimerRange);

        if (hpmg.HasHP() == false)
            Dead();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(xRaw, yRaw);

        _anim.SetFloat("Vel", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));


        //Coldown Habilidades
        _currentCooldownTimer -= Time.deltaTime;
        _currentCooldownTimerRange -= Time.deltaTime;
        fadeHabilidadL.fillAmount = _currentCooldownTimer / _highAttackCooldown;
        fadeHabilidadBotella.fillAmount = _currentCooldownTimerRange / _rangeAttackCooldown;

        if (x > 0 || x < 0 || y > 0 || y < 0)
        {
            if (activarCaminarSonido == true)
            {
                audioCaminar.Play();
                activarCaminarSonido = false;
            }
        }
        else
        {
            audioCaminar.Stop();
            activarCaminarSonido = true;
        }
    }

    public void FixedUpdate()
    {
        if (_state == STATE_INMOVIL)
            return;

        else if (_state == STATE_NORMAL)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float xRaw = Input.GetAxisRaw("Horizontal");
            float yRaw = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(xRaw, yRaw);

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

            if (Input.GetKeyDown(KeyCode.J))
            {
                AttackLow();
                audioPlayer.PlayOneShot(golpeClip);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                AttackHigh();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                AttackRange();
            }
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!_canMove)
            return;

        rb.velocity = new Vector2(dir.x * _xSpeed, dir.y * _ySpeed);
    }

    private void AttackLow()
    {
        if (_atacking == false)
            _activeCoroutine = StartCoroutine(AtackRoutine());
    }

    private void AttackRange()
    {
        if (_atacking == false && _currentCooldownTimerRange <= 0)
        {
            _activeCoroutine = StartCoroutine(RangeAtackRoutine());
            StartCoroutine(InstanciarBotella(0.7f));
        }
            
            
    }

    private IEnumerator InstanciarBotella(float delay)
    {
        yield return new WaitForSeconds(delay);

       
        if(sp.flipX == true)
        {
            GameObject bot = Instantiate(botella, posicionBotella_negativo.position, Quaternion.identity);
            bot.GetComponent<BotellaAtaque>().velocidad *= -1;
        }
        else
        {
            Instantiate(botella, posicionBotella.position, Quaternion.identity);
        }
        yield return null;
    }
    private IEnumerator RangeAtackRoutine()
    {
        _anim.SetTrigger("HitShoot");
        _atacking = true;
        rb.velocity = Vector2.zero;
        SetState(STATE_INMOVIL);
        
        yield return new WaitForSeconds(_rangeAttackTimer);

        SetState(STATE_NORMAL);

        _atacking = false;
        _currentCooldownTimerRange = _rangeAttackCooldown;

        yield return null;
    }
    private void AttackHigh()
    {
        if (_atacking == false && _currentCooldownTimer <= 0)
            _activeCoroutine = StartCoroutine(AtackHighRoutine());
    }
    private IEnumerator AtackHighRoutine()
    {
        _anim.SetTrigger("HitHigh");
        _atacking = true;
        rb.velocity = Vector2.zero;
        SetState(STATE_INMOVIL);

        yield return new WaitForSeconds(_highCollisionTimer);

        if (_lastFrameFlip == false)
            _highAtackR.enabled = true;
        else
        {
            _highAtackL.enabled = true;
        }

        yield return new WaitForSeconds(_highAttackTimer);

        _highAtackL.enabled = false;
        _highAtackR.enabled = false;

        SetState(STATE_NORMAL);

        _atacking = false;
        _currentCooldownTimer = _highAttackCooldown;

        yield return null;
    }
    private IEnumerator AtackRoutine()
    {
        _anim.SetTrigger("Hit");
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
