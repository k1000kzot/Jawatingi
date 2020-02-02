using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private static int STATE_NORMAL = 0;
    private static int STATE_LOCKED = 1;

    public Transform _target;
    public Vector3 _targetOffset;
    public Vector3 _targetOffset2;

    public CCheckTrigger _startTrigger1;
    public CCheckTrigger _startTrigger2;

    private Vector3 _desiredPos;

    public float _moveTime;

    public GameObject cameraNormal;
    public GameObject cameraFantasma;
    bool camerasBool = true;
    bool cambioCameras = true;
    public GameObject interfaceFantasmal;

    public int _state;

    public void Awake()
    {
        SetState(STATE_LOCKED);
    }

    public void LateUpdate()
    {
        transform.position = _desiredPos;
    }

    public void SetState(int aState)
    {
        _startTrigger1.UnCheked();
        _startTrigger2.UnCheked();

        _state = aState;
    }

    public void ManagingTriggers()
    {

    }

    public void Update()
    {
        if (_state == STATE_NORMAL)
            BasicMovment();

        else if (_state == STATE_LOCKED)
            LockedMovment();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(cambioCameras == true)
            {
                CambiarCamara(camerasBool = !camerasBool);
                cambioCameras = false;
                interfaceFantasmal.SetActive(!interfaceFantasmal.activeInHierarchy);
                StartCoroutine(ReinicioCambio(0.5f));
            }
            
        }
    }


    public void BasicMovment()
    {
        _desiredPos = GetTargetPos();
        _desiredPos = Vector3.Lerp(this.transform.position, _desiredPos, _moveTime * Time.deltaTime);
    }

    public void LockedMovment()
    {
        _desiredPos = GetTargetPos2();
        _desiredPos = Vector3.Lerp(this.transform.position, _desiredPos, _moveTime * Time.deltaTime);
    }

    private Vector3 GetTargetPos()
    {
        return new Vector3(_target.position.x, 0, 0) + _targetOffset;
    }

    private Vector3 GetTargetPos2()
    {
        return new Vector3(_target.position.x, 0, _target.position.z) + _targetOffset2;
    }

    void CambiarCamara(bool normal)
    {
        
            cameraNormal.SetActive(normal);
            cameraFantasma.SetActive(!normal);
        GameController.modoFantasmalActivo = !GameController.modoFantasmalActivo;
    }


    IEnumerator ReinicioCambio(float time)
    {
        yield return new WaitForSeconds(time);

        cambioCameras = true;
    }
}
