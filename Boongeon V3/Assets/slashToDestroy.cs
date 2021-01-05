using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashToDestroy : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
