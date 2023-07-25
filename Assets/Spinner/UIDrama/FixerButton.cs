using UnityEngine;

namespace UIDrama
{
    public class FixerButton : RbColliderDragger
    {
        [Space(25)]
        [SerializeField] private GameObject fixKnob;

        protected override void Start()
        {
            base.Start();
            SetFix();
        }

        protected override void OnMouseDown()
        {
            SetFix();
            base.OnMouseDown();
        }
        
        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            Body.isKinematic = !usePhysics;
        }

        private void SetFix()
        {
            usePhysics = !usePhysics;
            Body.isKinematic = !usePhysics;
            fixKnob.SetActive(!usePhysics);
        }
        
        
        
        
    }
}