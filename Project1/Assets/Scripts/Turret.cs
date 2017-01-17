using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<GameObject> enemys = new List<GameObject>();

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
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
    public float attackRate = 1;
    private float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;

    public bool useLaseer = false;
    public float damageRate = 77;
    public LineRenderer laserRender;
    public GameObject laserEffect;

    void Start()
    {
        timer = attackRate;
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (useLaseer == false) {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }
        }
        else if (enemys.Count>0)
        {
            if (laserRender.enabled == false)
                laserRender.enabled = true;
                laserEffect.SetActive(true);
            if (enemys[0] == null)
            {

                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                laserRender.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<enemyAI>().TakeDamage(damageRate*Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }

        }
        else
        {
            laserEffect.SetActive(false);
            laserRender.enabled = false;
        }


    }
    void Attack()
    {
        if (enemys[0] == null) {

            UpdateEnemys();
        }
        if (enemys.Count > 0) {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRate;
        }
    }
    void UpdateEnemys()
    {
        List<int> emptyindex = new List<int>();
        for (int index=0;index<enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyindex.Add(index);
            }
        }
        for (int i=0; i<emptyindex.Count; i++)
        {
            enemys.RemoveAt(emptyindex[i]-i);
        }
    }
}
