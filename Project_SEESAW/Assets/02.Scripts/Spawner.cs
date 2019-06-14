using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public int startwait;
    public bool stop;

    [Header("Spawn Points")]
    public GameObject LT;
    public GameObject RB;

    int randEnemy;


    //
    void Start()
    {
        StartCoroutine(waitGet_Key());
        spawnWait = 3.0f;
    }

    //
    void Update()
    {
        //spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        
    }

    IEnumerator waitGet_Key()
    {
        yield return new WaitForSeconds(startwait);

        float left = LT.transform.position.x;
        float right = RB.transform.position.x;
        float top = LT.transform.position.z;
        float bottom = RB.transform.position.z;

        while (!stop)
        {
            randEnemy = Random.Range(0, 1);

            Vector3 spawnPosition = new Vector3(Random.Range(left, right), 1, Random.Range(top, bottom));

            GameObject tmp = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);

            Destroy(tmp);
        }
    }

}