using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    [SerializeField]
    private bool isOneUse = true;
    [SerializeField]
    private float TimeReset = 5f;
    [SerializeField]
    private float TimeFadingStep = 0.1f;
    [SerializeField]
    private float TimeFadingValue = 0.1f;

    private bool isRestart = false;
    private bool isFading = false;
    private float timeReset = 0;
    private float timeFading = 0;

    private MeshRenderer render;
    private Collider col;
    private Color StandartColor;
    private Color EditColor;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        StandartColor = render.material.color;
        EditColor = StandartColor;
    }

    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        if (isRestart)
        {
            timeReset += Time.deltaTime;
        }

        if (isFading) {
            timeFading += Time.deltaTime;

            if (timeFading > TimeFadingStep) {
                timeFading = 0;
                EditColor.a = EditColor.a - TimeFadingValue;
                GetComponent<MeshRenderer>().material.color = EditColor;
            }

            if (EditColor.a <= 0) {
                render.enabled = false;
                col.enabled = false;
                isFading = false;
                EditColor = StandartColor;

                if (!isOneUse)
                {
                    isRestart = true;
                }
            }
        }

        if (timeReset >= TimeReset && isRestart)
        {
            render.enabled = true;
            col.enabled = true;
            GetComponent<MeshRenderer>().material.color = StandartColor;

            isRestart = false;
            timeReset = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFading = true;
        }
    }
}
