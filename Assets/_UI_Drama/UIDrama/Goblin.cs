using System;
using UnityEngine;
using UnityEngine.AI;

namespace UIDrama
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Goblin : MonoBehaviour
    {

        [SerializeField] private RbColliderDragger target;
       
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera playerCamera;
        
        private NavMeshAgent _body;
        private Transform _targetTransform;

        [SerializeField] private GameObject tester;

        private void Awake()
        {
            _body = GetComponent<NavMeshAgent>();
            _targetTransform = target.transform;

        }

        private void Update()
        {
            var uiPosition = uiCamera.WorldToScreenPoint(_targetTransform.position);
            var targetPosition = playerCamera.ScreenToWorldPoint(uiPosition);
            //tester.transform.position = targetPosition;
            _body.SetDestination(targetPosition);
            Debug.Log(_body.remainingDistance);

            if (_body.remainingDistance < 0.02f)
            {
                _targetTransform.position = transform.position + Vector3.up * 1.2f;
            }

        }
    }
}