using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{


    [Header("Move Settings")]
    public GameObject BodyToRotate;
    private Rigidbody _rb;
    private CharacterController _cc;

    public float WheelRotSpeed = .2f;
    public float SelfRotSpeed = .4f;

    [Range(0, 10f)] public float MoveSpeed = 3f;

    public GameObject ForceFieldPref;

    private bool _ffIsActive;

    public delegate void OnWin();
    public static event OnWin WinSender;

    // Start is called before the first frame update
    void Awake()
    {
        _ffIsActive = false;
        ForceFieldPref.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        _cc = GetComponent<CharacterController>();
        
        
    }

    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position + new Vector3(2, 1, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
            WinSender();
    }

    IEnumerator FieldSet()
    {
        for (int i = 0; i < 3; i++)
        {
            _ffIsActive = !_ffIsActive;
            ForceFieldPref.SetActive(_ffIsActive);
            PlayerStatsController.instance.SetForceField(_ffIsActive);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(10f);

        for (int i = 0; i < 3; i++)
        {
            _ffIsActive = !_ffIsActive;
            ForceFieldPref.SetActive(_ffIsActive);
            PlayerStatsController.instance.SetForceField(_ffIsActive);
            yield return new WaitForSeconds(.3f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
            {
            var inst = PlayerStatsController.instance;
            if (inst.IsEnoughEnergy(inst.FieldCost)) StartCoroutine(FieldSet());
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddRelativeForce(Vector3.left  * MoveSpeed * 1000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddRelativeForce(Vector3.right * MoveSpeed * 1000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 1f, 0), -SelfRotSpeed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1f, 0), SelfRotSpeed, Space.Self);
        }

    }
}
