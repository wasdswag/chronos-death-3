using System;
using UnityEngine;

namespace UIDrama
{
    [RequireComponent(typeof(Sand))]
    public class Hourglass : RbColliderDragger, ILoadable
    {
        [Space(25), SerializeField] private Sand sand;
        public Action<int> OnProgressChange { get; set; }
        private int _percentage;
        
        public void SetProgress()
        {
            _percentage++;
            OnProgressChange?.Invoke(_percentage);
        }
        public int GrainCount() => sand.Grains.Count;
        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            CursorIsOutCollider = false;
        }
        protected override void Spin()
        {
            base.Spin();
            Body.isKinematic = true;
        }
        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            if (mouseIsPressed) Width = DistanceToCursor();
        }
        public override void TryMove()
        {
            sand.GrainsInsideTheBowl();
            if (mouseIsPressed)
            {
                if (usePhysics && Body.isKinematic && !IsSpinning()) Body.isKinematic = false;
                if (CursorIsOutCollider && DistanceToCursor() >= Width)
                    Move();
            }
        }
    }
}