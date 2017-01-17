using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAI : MonoBehaviour {
    public float speed = 10;
    public float hp =150;
    private float totalHp;
    public GameObject explosionEffect;
    public Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    private int reward;

	// Use this for initialization
	void Start () {
        positions = waypoints.positions;
        totalHp = hp;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position-transform.position).normalized*Time.deltaTime*speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            Reachbase();
        }
    }

    void Reachbase()
    {
        GameManager.instance.Lose();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        Enemyspanner.CountEnemyAlive--;
    }

    

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if(hp <= 0)
        {
            reward = BuildManager.buildManager.money += 50;
             
            BuildManager.buildManager.moneyText.text = "$" + reward;
            Die();
            
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
