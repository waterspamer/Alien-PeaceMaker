using UnityEngine;

public class PointInTime
{

    public readonly Vector3 position;
    public readonly Quaternion rotation;
    public readonly Vector3 velocity;
    public readonly Vector3 angularVelocity;
    public readonly int health;

    public PointInTime(Transform t, Vector3 v, Vector3 aV, int h)
    {
        position = t.position;
        rotation = t.rotation;
        velocity = v;
        angularVelocity = aV;
        health = h;
    }

}