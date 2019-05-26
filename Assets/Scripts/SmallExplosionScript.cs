using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosionScript : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update

    IEnumerator FXLogic()
    {
        yield return new WaitForSeconds(1f);
        ObjectPooler.instance.ReturnToPool(gameObject, "SmallExplosionEffect");
    }

    public void OnObjectSpawn()
    {
        
        StartCoroutine(FXLogic());
        
    }
}
