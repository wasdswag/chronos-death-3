using UnityEngine;
using TMPro;

namespace UIDrama
{
    public class Executable : RbColliderDragger, IFile
    {
        [SerializeField] private TextMeshPro filename;
        [SerializeField] private Program program;

        public string Filename => filename.text;
        public Vector2 Position => transform.position;
       
        private IProgram iProgram;
        private int clickCount;
        
        protected override void Start()
        {
            base.Start();
            if(program != null)
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

    
    }

   
}