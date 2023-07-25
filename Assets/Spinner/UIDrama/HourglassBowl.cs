using System;
using UnityEngine;

namespace UIDrama
{
    public class HourglassBowl : MonoBehaviour
    {
        [SerializeField] private bool isCollector;
        [SerializeField] private HourglassBowl otherBowl;
        private Hourglass _hourglass;
        private Camera _camera;
        [SerializeField]  private int _grainsPerRound;
        

        private void Start()
        {
            _hourglass = GetComponentInParent<Hourglass>();
            _camera = Camera.main;
            isCollector = IsDownSide();
            _grainsPerRound = _hourglass.GrainCount();
        }
        private bool IsDownSide() => Vector3.Dot(_camera.transform.up, transform.up) > 0f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Grain grain))
            {
                if (isCollector && IsDownSide())
                {
                    _hourglass.SetProgress();
                    _grainsPerRound--;
                    if (_grainsPerRound == 0)
                    {
                        isCollector = false;
                        otherBowl.isCollector = true;
                        _grainsPerRound = _hourglass.GrainCount();
                    }
                }
            }
        }
        
    }
}