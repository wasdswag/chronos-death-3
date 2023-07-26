using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UIDrama
{
    public class AllUIDramaHandler : MonoBehaviour
    {
       // public List<RbColliderDragger> Interactables = new List<RbColliderDragger>();
        public RbColliderDragger OverlappedByCursor;
        public RbColliderDragger CurrentDraggable;
        private void Update()
        {
          
            //if(CurrentDraggable) CurrentDraggable.TryMove();
            
            // var activeInteractables = new List<RbColliderDragger>(Interactables);
            //
            // foreach (var interactable in activeInteractables)
            //     interactable.TryMove();

        }
    }
}
