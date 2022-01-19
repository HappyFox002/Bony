using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{ 
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private GameObject PausePanel;

    public static UIController UI() { return GameObject.Find("Services").GetComponent<UIController>(); }

    public void DisableAll() {
        PausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        DisableAll();
        GameState.GS().SetPause(false);
    }

    public void PauseGame()
    {
        GameState.GS().SetPause(true);
        DisableAll();
        PausePanel.SetActive(true);
    }
}
