using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerScript : MonoBehaviour
{

    [Header("Toggler Settings")]
    public GameObject ConnectedDoor;

    public GameObject Lighter;

    public GameObject RotatedPart;

    public Light Light;

    public Material Green;

    public Material Red;

    private bool _wasToggled;

    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (!_wasToggled)
        {
            if (!other.CompareTag("Player")) return;
            ConnectedDoor.GetComponent<DoorScript>().SwapSwitcheability();
            Light.color = Color.green;
            Lighter.GetComponent<MeshRenderer>().material = Green;
            RotatedPart.GetComponent<Animation>().Play();
        }
        _wasToggled = true;
    }

    void Start()
    {
        Lighter.GetComponent<MeshRenderer>().material = Red;
        _wasToggled = false;
    }
}
