using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRenderer : MonoBehaviour
{
    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _cam.transform.rotation;
    }
}
