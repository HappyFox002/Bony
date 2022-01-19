using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool IsPaused { get { return _IsPaused; } }
    private bool _IsPaused = false;

    public void SetPause(bool value) { 
        _IsPaused = value;
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public static GameState GS() { return GameObject.Find("Services").GetComponent<GameState>(); }
}
