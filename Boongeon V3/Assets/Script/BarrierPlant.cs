using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPlant : MonoBehaviour
{
    public Enemy enemy;
    public Enemy enemy1;
    public Enemy enemy2;

    public bool enemyDead;
    public bool enemy1Dead;
    public bool enemy2Dead;

    void Start()
    {
        enemyDead = false;
        enemy1Dead = false;
        enemy2Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.gameObject.tag == "Dead")
            enemyDead = true;
        if (enemy1.gameObject.tag == "Dead")
            enemy1Dead = true;
        if (enemy2.gameObject.tag == "Dead")
            enemy2Dead = true;

        if (enemyDead && enemy1Dead && enemy2Dead)
        {
            Destroy(gameObject);
        }
    }
}
