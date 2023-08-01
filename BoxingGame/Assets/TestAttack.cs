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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            if (controller.AttackCheck())
            {
                Debug.Log("collide!!");
                defaultMaterial.material = test2;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            defaultMaterial.material = test1;
        }
    }
}
