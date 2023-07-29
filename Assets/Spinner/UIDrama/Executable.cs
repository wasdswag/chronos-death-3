using UnityEngine;
using System;

namespace UIDrama
{
    public class Executable : RbColliderDragger
    {
        
       // public Action OnDoubleClick;
        [SerializeField] private GameObject program;
        private IProgram iProgram;
        
        
        private int clickCount;
        private bool isExecuting;

        protected override void Start()
        {
            base.Start();
            iProgram = program.GetComponent<IProgram>();
        }

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            clickCount++;
            if (clickCount == 2 && iProgram.IsRunning == false)
            {
                iProgram.Run();
                Debug.Log("program running");
            }
        }

        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            clickCount = 0;
        }
        
    }
}