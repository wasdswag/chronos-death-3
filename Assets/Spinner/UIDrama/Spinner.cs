using System;
using System.Collections;
using UnityEngine;


namespace UIDrama
{
    public class Spinner : RbColliderDragger, ILoadable
    {
        public Action<int> OnProgressChange { get; set; }

        [Space (25)]
        [SerializeField] private float minResistance = 0.02f;
        [SerializeField] private float maxResistance = 0.2f;
        
        [SerializeField] private AnimationCurve resistanceCurve;
        [SerializeField] private float percentPerAngle = 45f;
        
        private int _rotationCounter;
        private Coroutine _regress;
        private Coroutine _auto;



        public void StartAuto()
        {
            _auto = StartCoroutine(AutoSpinTutorial());
        }

        private IEnumerator AutoSpinTutorial()
        {
            int autoRotationCount = 50;
            while (_rotationCounter < autoRotationCount)
            {
                var delta = 1000f * Time.deltaTime;

                angle += delta;
                transform.Rotate(Vector3.back * delta);
                Progress();
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            _regress = StartCoroutine(OnSpinnerRelease());
            
        }
        

        protected override void Start()
        {
            isRotatable = true;
            base.Start();
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            CursorIsOutCollider = false;
        }

        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            if (mouseIsPressed) Radius = DistanceToCursor();
        }

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            if (_regress != null)
                StopCoroutine(_regress);
        }

        protected override void Spin()
        {
            if (_rotationCounter < 102) base.Spin();
            Progress();
        }

        public override void TryMove()
        {
            if (mouseIsPressed)
            {
                if (CursorIsOutCollider && DistanceToCursor() >= Radius)
                    Move();
            }

        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            _regress = StartCoroutine(OnSpinnerRelease());
        }


        private void Progress()
        {
            if (angle >= percentPerAngle)
            {
                _rotationCounter++;
                angle = 0;
                OnProgressChange?.Invoke(_rotationCounter);
            }
            else if (angle <= -percentPerAngle)
            {
                _rotationCounter--;
                angle = 0;
                OnProgressChange?.Invoke(_rotationCounter);
            }
        }
        
        private IEnumerator OnSpinnerRelease()
        {
           
            Quaternion pAngle = transform.rotation;
            Body.gravityScale = releaseGravity ;
            while (_rotationCounter > 0)
            {
                Body.angularVelocity = Mathf.Lerp(minResistance, maxResistance,
                    resistanceCurve.Evaluate((float)_rotationCounter / 100f));

                var rotation = transform.rotation;
                var cAngle = rotation;
                var aDelta = Quaternion.Angle(pAngle, cAngle);
                pAngle = rotation;
                angle -= aDelta;
                
                Progress();
                yield return null;
            }
            Body.angularVelocity = 0f;
        }


    }
}
