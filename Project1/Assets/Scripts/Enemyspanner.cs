﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspanner : MonoBehaviour {

    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    private Coroutine coroutine;
    public Transform START;
    public float waveRate = 6;

    void Start()
    {
        coroutine= StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
	
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i =0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager.instance.Win();
    }
}
