using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float speed = 10;
    private Transform[] position;
    private int Index = 0;
    public float hp = 150;
    private float totalHp;
    public GameObject explosionEffectPrefab;
    private Slider sliderHp;

    void Start()
    {
        position = WayPoint.positions;
        totalHp = hp;
        sliderHp = GetComponentInChildren<Slider>();
    }

    void Update () {
        Move();
	}

    public void Move()
    {
        if (Index > position.Length - 1) return;
        transform.Translate((position[Index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(position[Index].position, transform.position) < 0.2f)
        {
            Index++;
        }

        if (Index > position.Length - 1)
        {
            GameManager._instance.Failed();
            ReachDistination();
        }
    }

    void ReachDistination()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.enemyAliveCount--;
    }


    //敌人血量
    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        sliderHp.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect.gameObject,1.5f);
        Destroy(this.gameObject);
    }
}
