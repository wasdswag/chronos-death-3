using UnityEngine;

namespace UIDrama
{
    public class NewSpinner : SpriteDragger
    {
 
        
        public override void OnEnter()
        {
            base.OnEnter();
            CursorIsOutCollider = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            if (mouseIsPressed) Radius = DistanceToCursor();
        }

        public override void OnDown()
        {
            base.OnDown();
        }

        
        public override void OnDrag()
        {
            Debug.Log("Dragging");
            if(CanMove()) base.OnDrag();
            else Spin();
        }
        
        public override void TryMove()
        {
            if (mouseIsPressed)
            {
                if (CursorIsOutCollider && DistanceToCursor() >= Radius)
                    Move();
            }
        }

    }
}