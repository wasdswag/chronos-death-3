using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDrama;

[ExecuteAlways]
public class CursorTester : MonoBehaviour
{
    [SerializeField] private CursorHandler cursorHandler;
    [SerializeField] private Cursors current;
    

    private void OnMouseOver()
    {
        cursorHandler.TestCursor(current);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 

    }
}
