using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugManager : MonoBehaviour
{
    public event EventHandler<bool> eve_playerInvul;
    public GameObject debugPanel;
    public bool dP;
    int cursorLockstate;
    bool cursorVisible;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            dP = !dP;
            if (dP)
                DebugPanelOpen();
            else
                DebugPanelClose();
        }
    }

    public void OnClick_PlayerInvulnerable(bool b)
    {
        eve_playerInvul?.Invoke(this, b);
    }

    public void DebugPanelClose()
    {
        dP = false;
        debugPanel.SetActive(false);
        Cursor.lockState = (CursorLockMode)cursorLockstate;
        Cursor.visible = cursorVisible;
    }

    public void DebugPanelOpen()
    {
        dP = true;
        debugPanel.SetActive(true);
        cursorLockstate = (int)Cursor.lockState;
        cursorVisible = Cursor.visible;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
