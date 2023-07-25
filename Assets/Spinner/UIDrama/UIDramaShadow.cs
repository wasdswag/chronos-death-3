using UnityEngine;

namespace UIDrama
{
    public class UIDramaShadow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 _offset;
        private void Start() => _offset = target.position - transform.position;
        private void Update() => transform.position = target.position - _offset;
    }
}
