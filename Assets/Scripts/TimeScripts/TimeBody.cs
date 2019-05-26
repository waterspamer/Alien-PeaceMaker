using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;

    public float recordTime = 5f;

    public float RewindCost = .1f;

    private PlayerStatsController inst;

    LinkedList<PointInTime> pointsInTime;

    Rigidbody rb;

    public bool IsRewinding()
    {
        return isRewinding;
    }

    // Use this for initialization
    void Start()
    {
        inst =  PlayerStatsController.instance;
        pointsInTime = new LinkedList<PointInTime>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.T))
            StopRewind();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 1 )
        {       
            PointInTime pointInTime = pointsInTime.First.Value;
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            inst.ChangeHP(pointInTime.health - inst._currentHP);
            pointsInTime.RemoveFirst();
            
            inst.ChangeEnergy(-RewindCost);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveLast();
        }
        pointsInTime.AddFirst(new PointInTime(this.transform, (rb != null) ? rb.velocity : new Vector3(0,0,0),
            (rb != null) ? rb.angularVelocity : new Vector3(0,0,0), (int)inst._currentHP));
    }

    public void StartRewind()
    {
        isRewinding = true;
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        
        if (rb != null)
        {
            rb.isKinematic = false;
            ReapplyForces();
        } 
    }
    void ReapplyForces()
    {
   
        var piT = pointsInTime.First.Value;
        rb.position = piT.position;
        rb.rotation = piT.rotation;
        rb.velocity = piT.velocity;
        rb.angularVelocity = piT.angularVelocity;

    }
}