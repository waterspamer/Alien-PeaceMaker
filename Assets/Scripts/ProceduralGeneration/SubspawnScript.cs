using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubspawnScript : MonoBehaviour
{

    [SerializeField] private GameObject _barrel;
    [SerializeField] private GameObject _case;
    [SerializeField] private GameObject _bar;
    [SerializeField] private GameObject _enemyPrefab1;

    void GenerateSubObjects()
    {
        for (int i = 0; i < 6; i++)
        {
            if (Random.Range(0, 1f) > 0.6f)
            {
                var a = Random.Range(0, 3);
                Instantiate(a == 0 ? _barrel : a == 1 ? _case : _bar, new Vector3(Random.Range(gameObject.transform.position.x - 3f, gameObject.transform.position.x + 3f), 1, Random.Range(gameObject.transform.position.z-3f, gameObject.transform.position.z + 3f)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
        }
    }

    void InstantiateObject(GameObject obj)
    {
        if (obj == null) return;
        Instantiate(obj, new Vector3(Random.Range(gameObject.transform.position.x - 3f, gameObject.transform.position.x + 3f), 1, Random.Range(gameObject.transform.position.z - 3f, gameObject.transform.position.z + 3f)), Quaternion.identity);
    }


    void GenerateRanEnemies()
    {
        InstantiateObject(Random.Range(0, 1f) > 0.7f ? _enemyPrefab1 : null);
    }
    
    void Start()
    {
        GenerateSubObjects();
        GenerateRanEnemies();
    }
}
