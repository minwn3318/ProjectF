using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    MeshRenderer defaultMaterial;
    public Material test1;
    public Material test2;

    public float HP = 100;
    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>();
        HP = 100;
    }
    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCollider"))
        {
            Debug.Log("AttackCollider");
            defaultMaterial.material = test2;
            HP -= 5;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AttackCollider"))
        {
            defaultMaterial.material = test1;
        }
    }

}
