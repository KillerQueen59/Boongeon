using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBook : MonoBehaviour
{
    public GameObject asta;
    public Enemy enemy;
    public Enemy enemy1;
    public Enemy enemy2;
    public bool enemyDead;
    public bool enemy1Dead;
    public bool enemy2Dead;
    public bool getAsta;

    void Start()
    {
        enemyDead = false;
        enemy1Dead = false;
        enemy2Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 200f); ;


        if (enemy.gameObject.tag == "Dead")
            enemyDead = true;
        if (enemy1.gameObject.tag == "Dead")
            enemy1Dead = true;
        if (enemy2.gameObject.tag == "Dead")
            enemy2Dead = true;

        if(enemyDead && enemy1Dead && enemy2Dead)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 10, gameObject.transform.position.z);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            getAsta = true;
            Destroy(gameObject);
            Destroy(enemy);
            Destroy(enemy1);
            Destroy(enemy2);
        }
    }
}
