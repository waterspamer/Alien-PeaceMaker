using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLightScript : MonoBehaviour
{
    public Light Light;
    private float _intensity;
    // Start is called before the first frame update
    void Start()
    {
        _intensity = Light.intensity;
    }

    IEnumerator LightBug()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Light.intensity = 0;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            Light.intensity = _intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var a = Random.Range(0, 100);
        if (a > 95)
        {
            StartCoroutine(LightBug());
        }
    }
}
