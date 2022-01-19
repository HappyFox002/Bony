using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    protected Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        DataInit();
    }

    protected void SliderUpdate(float data) {
        slider.value = data;
    }

    public virtual void DataInit() { }
}
