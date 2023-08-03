using UnityEngine;

namespace UIDrama
{
    public class File : Executable
    {
        protected override void OnMouseDown()
        {
            SelfCollider.isTrigger = true;
            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            SelfCollider.isTrigger = false;
            base.OnMouseUp();
        }
    }
}