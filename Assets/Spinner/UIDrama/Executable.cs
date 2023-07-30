using UnityEngine;
using TMPro;

namespace UIDrama
{
    public class Executable : RbColliderDragger, IFile
    {

        [SerializeField] private TextMeshPro filename;
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
            if (iProgram == null) return;
            
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

        public string Filename => filename.text;
    }

   
}