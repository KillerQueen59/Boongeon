using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial_Outer : MonoBehaviour
{
    
    public Renderer Rend;
    private int index = 1;

    public float switchTime = 3.0f;

    void Start()
    {
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
    }

    IEnumerator switchMaterial()
    {
       // GetComponent<MeshRenderer>().material = materials(1);

        yield return new WaitForSeconds(switchTime);

        //GetComponent<MeshRenderer>().material = materials(2);
    }
}