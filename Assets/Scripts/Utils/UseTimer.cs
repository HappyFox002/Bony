using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTimer : MonoBehaviour
{
    public delegate void Action();

    private float time = 0;
    private float TimeWork = 0;
    private bool isWork = false;
    private Action timerAction;

    public static void CreateTimer(float TimeUse, Action action, string name = "NoNameTimer") {
        new GameObject().AddComponent<UseTimer>().StartTimer(TimeUse,action,name);
    }

    public void StartTimer(float TimeUse, Action action, string name) {
        TimeWork = TimeUse;
        timerAction = action;
        this.gameObject.name = name;
        isWork = true;
    }

    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        if (isWork) {
            time += Time.deltaTime;

            if (time >= TimeWork) {
                timerAction();
                isWork = false;
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
