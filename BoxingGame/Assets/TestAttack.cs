using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    MeshRenderer defaultMaterial;
    public Material test1;
    public Material test2;

    public float HP;
    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
