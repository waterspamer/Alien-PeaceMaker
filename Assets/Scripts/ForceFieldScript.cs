using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        StartCoroutine(FieldSwitch());
    }

    IEnumerator FieldSwitch()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("SADSA");
            gameObject.SetActive(!gameObject.activeInHierarchy);
            yield return new WaitForSeconds(.1f);
        }    
    }
}
