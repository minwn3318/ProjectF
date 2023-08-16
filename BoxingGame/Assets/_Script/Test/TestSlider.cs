using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    public TestAttack enemy;
    public Slider HPSlider;
    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GetComponent<Slider>();
        HPSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemy.HP);
        HPSlider.value = enemy.HP / 100;
    }
}
