using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public List<GameObject> enemys = new List<GameObject>();
    public float attackRoateTime = 1; //多少秒攻击一次
    private float timer=0;
    public GameObject bulletPrefab;  //子弹
    public Transform firePosition;
    public Transform headPosition;//炮塔头部方向

    public bool useLaser=false;  //使用激光攻击
    public float rateDamage = 70;
    public LineRenderer lineRenderer;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    void Start()
    {
        timer = attackRoateTime;
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = headPosition.position.y;
            headPosition.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRoateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            //使用激光攻击
            if (lineRenderer.enabled == false)
                lineRenderer.enabled = true;
            if (enemys[0] == null)
            {
                UpdateEnemy();
            }
            if (enemys.Count > 0)
            {
                lineRenderer.SetPositions(new Vector3[] { firePosition.position,enemys[0].transform.position});
                enemys[0].GetComponent<Enemy>().TakeDamage(rateDamage * Time.deltaTime);
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemy();
        }else if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            
        }
        else
        {
            timer = attackRoateTime;
        }
    }

    void UpdateEnemy()
    {
        List<int> enemyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                enemyIndex.Add(index);
            }
        }

        for (int i = 0; i < enemyIndex.Count; i++)
        {
            enemys.RemoveAt(enemyIndex[i] - i);
        }

    }
}
