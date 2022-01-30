using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    GameObject barrel = null;

    System.Random randomGenerator = null;

    private void Start() {
        randomGenerator = new System.Random();
        StartCoroutine(spawnBarrel());
    }

    IEnumerator spawnBarrel() {
        while(true) {
            Instantiate(barrel, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(randomGenerator.Next(2, 5));
        }
    }
}
