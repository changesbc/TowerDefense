using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Wave[] waves;
    public Transform START;
    public float waveRoate = 0.2f;
    public static int enemyAliveCount = 0;  //存活的敌人的数量
    private Coroutine coroutine;

	void Start () {
        coroutine=StartCoroutine(SpawnEnemy());
	}
	
    //停止生成敌人
    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                enemyAliveCount++;
                yield return new WaitForSeconds(wave.rate);
            }
            while (enemyAliveCount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRoate);
        }

        while (enemyAliveCount > 0)
        {
            yield return 0;
        }
        GameManager._instance.Win();
    }
}
