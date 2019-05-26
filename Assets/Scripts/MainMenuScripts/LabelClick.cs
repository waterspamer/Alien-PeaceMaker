using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabelClick : MonoBehaviour
{
    private float _scale = 1.1f;
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
        SceneManager.LoadScene("MainScene");
    }
}
