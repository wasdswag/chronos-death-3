using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public enum Cursors { Regular, Move, Spin, Over, SpinOver, MoveOver, Undefined }
    [ExecuteAlways]
    public class CursorHandler : MonoBehaviour
    {
        [System.Serializable]
        private struct CursorImage
        {
            public Texture2D icon;
            public Vector2 offset;
        }
        
        [SerializeField] private CursorImage regular;
        [SerializeField] private CursorImage over;
        [SerializeField] private CursorImage move;
        [SerializeField] private CursorImage spin;
        [SerializeField] private CursorImage spinOver;
        [SerializeField] private CursorImage moveOver;

        private readonly Dictionary<Cursors, CursorImage> _cursors = new Dictionary<Cursors, CursorImage>();
        private Cursors _current;

        [SerializeField] private bool isTesting;
        
        
        private void Awake()
        {
            _current = Cursors.Undefined;

            _cursors.Add(Cursors.Regular, regular);
            _cursors.Add(Cursors.Over, over);
            _cursors.Add(Cursors.Move, move);
            _cursors.Add(Cursors.Spin, spin);
            _cursors.Add(Cursors.SpinOver,spinOver);
            _cursors.Add(Cursors.MoveOver,moveOver);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
            SetCursor(Cursors.Regular);
        }

        public void SetCursor(Cursors current)
        {
            if (_current != current || isTesting)
            {   
                 Cursor.SetCursor(_cursors[current].icon, _cursors[current].offset, CursorMode.ForceSoftware);
                _current = current;
            }
        }
        public void TestCursor(Cursors test)
        {
            switch (test)
            {
                case Cursors.Regular:
                    _cursors[test] = regular;
                    break;
                case Cursors.Move:
                    _cursors[test] = move;
                    break;
                case Cursors.Over:
                    _cursors[test] = over;
                    break;
                case Cursors.Spin:
                    _cursors[test] = spin;
                    break;
                case Cursors.MoveOver:
                    _cursors[test] = moveOver;
                    break;
                case Cursors.SpinOver:
                    _cursors[test] = spinOver;
                    break;
                default:
                    return;
            }
            SetCursor(test);
        }
        
        
    }
}