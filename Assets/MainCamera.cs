using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform _target;
    public Vector3 _targetOffset;

    private Vector3 _desiredPos;

    public float _moveTime;

    public GameObject cameraNormal;
    public GameObject cameraFantasma;
    bool camerasBool = true;
    bool cambioCameras = true;
    public GameObject interfaceFantasmal;

    public void FixedUpdate()
    {
        BasicMovment();

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

    public void LateUpdate()
    {
        transform.position = _desiredPos;
    }

    public void BasicMovment()
    {
        _desiredPos = GetTargetPos();
        _desiredPos = Vector3.Lerp(this.transform.position, _desiredPos, _moveTime * Time.deltaTime);
    }

    private Vector3 GetTargetPos()
    {
        return new Vector3(_target.position.x, 0, _target.position.z) + _targetOffset;
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
