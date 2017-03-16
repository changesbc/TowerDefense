using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int Damage = 50;
    public float speed = 10;
    private Transform target;
    public GameObject explosionEffectPrefab;
    private float distanceArriveTarget = 1.0f;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 vel = target.position - transform.position;
        if (vel.magnitude < distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(Damage);
            Die();
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect.gameObject, 1);
        Destroy(this.gameObject);
    }

}
