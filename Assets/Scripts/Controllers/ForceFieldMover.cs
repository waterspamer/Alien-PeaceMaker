using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldMover : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.fixedDeltaTime);
    }
}
