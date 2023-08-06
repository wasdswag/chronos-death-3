using System;
using UnityEngine;

namespace UIDrama
{
    public class InstallerHeader : MonoBehaviour
    {
        [SerializeField] private GameObject field;
        [SerializeField] private GameObject headerFloor;
        private Rigidbody2D body;


     
        public void OnInstallationSuccess()
        {
            body = GetComponent<Rigidbody2D>();
            var rbs = FindObjectsOfType<RbColliderDragger>();
            foreach (var rb in rbs) 
                rb.Release();
                
            field.SetActive(false);
            headerFloor.SetActive(true);
            body.isKinematic = false;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            var collider = other.collider;
            if (collider.TryGetComponent(out Rigidbody2D otherbody))
                otherbody.isKinematic = false;
              
        }

      
    }
}