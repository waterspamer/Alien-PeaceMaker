using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    [SerializeField] private Transform _spawnPos;
    // Start is called before the first frame update
    
    IEnumerator ContiniousSpawn()
    {
        Instantiate(EnemyPrefab, _spawnPos.position, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        StartCoroutine(ContiniousSpawn());
    }

    void Start()
    {
        StartCoroutine(ContiniousSpawn());
    }
}
