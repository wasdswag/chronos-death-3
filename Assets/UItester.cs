
using UIDrama;
using UnityEngine;

public class UItester : MonoBehaviour, IMouseInteractable
{
    public Vector3 Position => transform.position;
    public Vector3 CursorPosition { get; set; }

    private RbColliderDragger _item;

    public void Init(RbColliderDragger item)
    {
        _item = item;
    }

    public void OnEnter()
    {
 
    }

    public void OnOver()
    {
        Debug.Log("Over");
    }

    public void OnDown()
    {
        Debug.Log("Down");
    }

    public void OnDrag()
    {
        Debug.Log("Drag");
    }

    public void OnUp()
    {
        Debug.Log("Up");
    }

    public void OnExit()
    {
        Debug.Log("Exit");
    }
    
    //
    //     protected virtual void OnMouseEnter()
    //         {
    //             if (_isMoving == false && _collider.isTrigger == false)
    //                 _hovered = this;
    //             
    //             CursorHandler.SetCursor(Cursors.Over);
    //             if (mouseIsPressed) DisablePhysics();
    //             else CursorIsOutCollider = false;
    //         }
    //         protected virtual void OnMouseDown()
    //         {
    //             DisablePhysics();
    //             _cursorOffset = GetCursorPosition() - transform.position;
    //             _previousAngle = GetAngle();
    //             mouseIsPressed = true;
    //             CursorIsOutCollider = false;
    //         }
    //         protected virtual void OnMouseDrag()
    //         {
    // //            Debug.Log("Drag");
    //             Body.Sleep();
    //             if (CursorIsOutCollider || CursorIsOverSpinDeadZone()) 
    //                 TryMove();
    //             else 
    //                 Spin();
    //             
    //         }
    //         private void OnMouseOver()
    //         {
    //             var cursor = CursorIsOverSpinDeadZone() ? Cursors.MoveOver : Cursors.SpinOver;
    //             CursorHandler.SetCursor(cursor);
    //         }
    //         protected virtual void OnMouseExit()
    //         {
    //             if (_isMoving == false)
    //                 _hovered = null;
    //             
    //             _isSpinning = false;
    //             CursorIsOutCollider = true;
    //             CursorHandler.SetCursor(Cursors.Regular);
    //
    //             if (mouseIsPressed)
    //                 _cursorOffset = GetCursorPosition() - transform.position;
    //             else
    //                 Body.gravityScale = releaseGravity;
    //         }
    //
    
}
