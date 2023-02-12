using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject spawn;

    void Start(){
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls(){//make spawn as children
        yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        Instantiate(ballPrefab, new Vector2(-3.0f, 2.55f), Quaternion.identity, spawn.transform);
        StartCoroutine(SpawnBalls()); //stop spawning is the game is sucessfully finished
    }
}
