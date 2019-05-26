using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMoving : MonoBehaviour
{
    private Vector3 _pos;
    // Start is called before the first frame update
    void Start()
    {
        _scale = 1.1f;
        _pos = transform.position;
        StartCoroutine(ChangeDir());
    }

    private float _scale;

    void OnMouseEnter()
    {
        transform.localScale *= _scale;
    }

    void OnMouseExit()
    {
        transform.localScale /= _scale;
    }

    void OnMouseDown()
    {
        Application.Quit();
    }

    IEnumerator ChangeDir()
    {
        _movingDirection = new Vector3(
            Random.Range(_pos.x - .3f, _pos.x + .3f),
            Random.Range(_pos.y - .3f, _pos.y + .3f), 
            Random.Range(_pos.z - .3f, _pos.z + .3f));    
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeDir());
    }

    private Vector3 _movingDirection;

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector3.Lerp(transform.position,_movingDirection, .6f * Time.deltaTime));
    }
}
