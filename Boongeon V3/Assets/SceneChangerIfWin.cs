using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerIfWin : MonoBehaviour
{
    public GameObject bossHealth;
    Boss bossDead;

    public float hpZero;

    private void Start()
    {
        bossDead = bossHealth.GetComponentInChildren<Boss>();
        hpZero = bossDead.currentHp;
    }

    private void Update()
    {
        hpZero = bossDead.currentHp;
        if (hpZero <= 0)
            SceneManager.LoadScene("GameWinner");
    }
}
