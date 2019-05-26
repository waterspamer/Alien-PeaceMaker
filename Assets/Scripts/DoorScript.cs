using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [Header ("Door Components")]

    public GameObject LeftDoor;

    public GameObject RightDoor;

    public Transform LeftDoorPoint;

    public Transform RightDoorPoint;

    public GameObject Lighter;

    public Light Light;

    public Material Green;

    public Material Red;

    private Vector3 _leftDoorStartPos;

    private Vector3 _rightDoorStartPos;

    public bool canBeSwitched = false;



    [Header("Tech Params")]

    [Range(0, 5f)] public float LockSpeed = 3f;

    private bool _toOpen = false; 

    public void SwitchDoor()
    {
        _toOpen = !_toOpen;
    }

    // Start is called before the first frame update
    void Start()
    {
        Light.color = canBeSwitched ? Color.green : Color.red;
        Lighter.GetComponent<MeshRenderer>().material = canBeSwitched ? Green : Red;
        _leftDoorStartPos = LeftDoor.transform.position;
        _rightDoorStartPos = RightDoor.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !canBeSwitched) return;
        _toOpen = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || !canBeSwitched) return;
        _toOpen = false;
       
    }

    public void SwapSwitcheability()
    {
        canBeSwitched = !canBeSwitched;
        Light.color = canBeSwitched ? Color.green : Color.red;
        Lighter.GetComponent<MeshRenderer>().material = canBeSwitched ? Green : Red;
    }


    // Update is called once per frame
    void Update()
    {
        if (!canBeSwitched) return;

        if (_toOpen)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, LeftDoorPoint.position, Time.deltaTime * LockSpeed);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, RightDoorPoint.position, Time.deltaTime * LockSpeed);
        }
        if (!_toOpen)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, _leftDoorStartPos, Time.deltaTime * LockSpeed);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, _rightDoorStartPos, Time.deltaTime * LockSpeed);
        }

    }
}
