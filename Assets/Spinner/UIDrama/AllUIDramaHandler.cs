using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public class AllUIDramaHandler : MonoBehaviour
    {
        public List<RbColliderDragger> Interactables = new List<RbColliderDragger>();
        public RbColliderDragger Overlapped;
        private void Update()
        {
            var activeInteractables = new List<RbColliderDragger>(Interactables);

            foreach (var interactable in activeInteractables)
                interactable.TryMove();

        }
    }
}
