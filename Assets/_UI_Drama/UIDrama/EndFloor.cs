using System;
using UnityEngine;

namespace UIDrama
{
    public class EndFloor : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.collider.TryGetComponent(out InstallerHeader installerHeader))
                installerHeader.StartChronOS();
        }
    }
}