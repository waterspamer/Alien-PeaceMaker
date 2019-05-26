using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform PosToLookAt;
    private Vector3 _offset;


    private float _lerpSetting = 1.5f;

    private Transform _defaultTransform;

    private Vector3 _bulletTimePos;

    private bool _bulletTime = false;

    private bool _rocketControlling = false;

    private Transform _tmp;






    public static CameraController instance;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        _bulletTimePos = new Vector3();
    } 

    public void MoveToBulletTime(Transform btTransform)
    {
        _bulletTimePos = btTransform.position;
        transform.LookAt(btTransform);
        StartCoroutine(ToggleCameraBulletTime());


    }

    public void RocketObserve(GameObject rocket)
    {
        _tmp = PosToLookAt;
        PosToLookAt = rocket.transform;
    }

    IEnumerator SetDefault()
    {
        Transform onEnd = PosToLookAt;
        PosToLookAt = onEnd;
        yield return new WaitForSeconds(.4f);
        PosToLookAt = _tmp;
    }

    public void DefaultObserve()
    {
        StartCoroutine(SetDefault());
    }


    IEnumerator ToggleCameraBulletTime()
    {
        _defaultTransform = gameObject.transform;
        _bulletTime = true;
        yield return new WaitForSeconds(.5f);
        _bulletTime = false;
        transform.position = _defaultTransform.position;
        transform.rotation = _defaultTransform.rotation;

    } 

    // Start is called before the first frame update
    void Start()
    {
        _offset = PosToLookAt.position - transform.position;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y)) SceneManager.LoadScene("MainScene");
        if (_bulletTime)
        {
            transform.position = Vector3.Lerp(transform.position, _bulletTimePos, Time.fixedDeltaTime * .5f);
            return;
        }


        transform.LookAt(PosToLookAt);
        transform.position = Vector3.Lerp(transform.position, PosToLookAt.position - _offset, Time.deltaTime * .5f );
    }
}
