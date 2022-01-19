using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    [SerializeField]
    private bool isOneUse = true;
    [SerializeField]
    private float TimeReset = 5f;

    private bool isRestart = false;
    private float time = 0;
    private MeshRenderer render;
    private Collider col;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        if (isRestart) {
            time += Time.deltaTime;
        }

        if (time >= TimeReset && isRestart) {
            render.enabled = true;
            col.enabled = true;
            isRestart = false;
            time = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            render.enabled = false;
            col.enabled = false;

            if (!isOneUse) {
                isRestart = true;
            }
        }
    }
}
