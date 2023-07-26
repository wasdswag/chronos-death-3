using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;

namespace UIDrama
{
    [RequireComponent(typeof (Rigidbody2D))]
    public abstract class RbColliderDragger : MonoBehaviour
    {
        protected Rigidbody2D Body;
        protected float Radius;
        protected float angle { get; set; }

        protected bool CursorIsOutCollider; 
        protected CursorHandler CursorHandler;
        protected bool mouseIsPressed { get; private set; }

        [Header("Move and Spin settings")]
        [SerializeField] protected float moveSpeed = 3000f;
        [SerializeField] [Range(0f, 1f)] private float spinDeadZone = 0.33f;

        [Space(10)] [Header("Physics settings")]
        [SerializeField] protected bool usePhysics;
        [ShowIf(nameof(usePhysics))] [SerializeField] protected float releaseGravity;
        
        [Serializable]
        private struct CollisionSettings
        {
            [Range(0.01f, 0.5f)] public float minDistance;
            [Range(0.01f, 3f)]   public float obstacleAvoidanceDistance;
            [Range(0.1f, 2f)]    public float obstacleDetectionRadius; 
            public bool isDebugging;
            public CollisionSettings(float min, float avoidance, float radius, bool isDebug)
            {
                minDistance = min;
                obstacleAvoidanceDistance = avoidance;
                obstacleDetectionRadius = radius;
                isDebugging = isDebug;
            }
            
        }
        [ShowIf(nameof(usePhysics))] [SerializeField] private LayerMask collisionLayers;
        [ShowIf(nameof(usePhysics))] [SerializeField] private CollisionSettings collisionSettings = new CollisionSettings(0.05f, 1.6f, 0.8f, false);
        
        private Camera _gameCamera;
        private float _previousAngle, _currentAngle, _delta; 

        private Vector3 _cursorPosition;
        private Vector3 _cursorOffset;
    
        private Collider2D _collider;
        private float _shapeWidth;

        private bool _isMoving;
        protected bool _isSpinning;

        private bool _isCollide, _isCursorOverlapped;
        private Vector3 _obstacleDirection;
        private static RbColliderDragger _hovered;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            var bounds = _collider.bounds;
            _shapeWidth = bounds.max.x - bounds.min.x;
        }
        protected virtual void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            if (usePhysics == false) Body.isKinematic = true;

            _gameCamera = Camera.main;
            CursorHandler = FindObjectOfType<CursorHandler>();
            Radius = _shapeWidth * .5f;
            
        }
        protected virtual void OnMouseEnter()
        {
            if (_isMoving == false && _collider.isTrigger == false)
                _hovered = this;
            
            CursorHandler.SetCursor(Cursors.Over);
            if (mouseIsPressed) DisablePhysics();
            else CursorIsOutCollider = false;
        }
        protected virtual void OnMouseDown()
        {
            DisablePhysics();
            _cursorOffset = GetCursorPosition() - transform.position;
            _previousAngle = GetAngle();
            mouseIsPressed = true;
            CursorIsOutCollider = false;
        }
        protected virtual void OnMouseDrag()
        {
            Body.Sleep();
            if (CursorIsOutCollider || CursorIsOverSpinDeadZone()) 
                TryMove();
            else 
                Spin();
            
        }
        private void OnMouseOver()
        {
            var cursor = CursorIsOverSpinDeadZone() ? Cursors.MoveOver : Cursors.SpinOver;
            CursorHandler.SetCursor(cursor);
        }
        protected virtual void OnMouseExit()
        {
            if (_isMoving == false)
                _hovered = null;
            
            _isSpinning = false;
            CursorIsOutCollider = true;
            CursorHandler.SetCursor(Cursors.Regular);

            if (mouseIsPressed)
                _cursorOffset = GetCursorPosition() - transform.position;
            else
                Body.gravityScale = releaseGravity;
        }
        protected virtual void OnMouseUp()
        {
            Body.WakeUp();
            if (usePhysics)
            {
                Body.isKinematic = false;
                Body.gravityScale = releaseGravity;
            }

            mouseIsPressed = false;
            _isMoving = false;
            _isSpinning = false;
            
            CursorHandler.SetCursor(Cursors.Regular);
        }
        public bool IsSpinning() => _isSpinning;
        
        private void DisablePhysics()
        {
            Body.angularVelocity = 0f;
            Body.velocity = Vector2.zero;
            Body.Sleep();
        }
        protected virtual void Spin()
        {
            GetAngleDelta();

            transform.Rotate(Vector3.back, _delta);
            angle += _delta;
            _isSpinning = true;
            CursorHandler.SetCursor(Cursors.Spin);
        }
        
        protected float DistanceToCursor() => Vector3.Distance(transform.position, GetCursorPosition());
        private bool CursorIsOverSpinDeadZone()
        {
            bool isInCenter = DistanceToCursor() < Radius * spinDeadZone;
            if (isInCenter)
                CursorHandler.SetCursor(Cursors.Move);
            
            return isInCenter;
        }
        public virtual void TryMove()
        {
            if (mouseIsPressed == false) return;
            if (CursorIsOutCollider || CursorIsOverSpinDeadZone()) Move();
        }

        protected virtual void Move()
        {
            if (!_isSpinning)
            {  
                CursorHandler.SetCursor(Cursors.Move);
                _isMoving = true;
                var desiredPosition = GetCursorPosition() - _cursorOffset;
                var dir = (desiredPosition - transform.position).normalized;
                
                if (desiredPosition.IsOnTheScreen() && !IsBlocked(transform.position, dir, desiredPosition)) 
                    Body.MovePosition(Vector3.MoveTowards(transform.position, desiredPosition,
                        moveSpeed * Time.deltaTime));
            }
        }
        private bool IsBlocked(Vector2 origin, Vector2 direction, Vector3 cursorPos)
        {
            if (_collider.isTrigger || usePhysics == false) return false;
            if (CursorIsOverObstacle()) return true;
            
            var outPosition = _collider.ClosestPoint(origin + direction * 100f);
            var hit = Physics2D.Raycast(outPosition, direction, 100f, collisionLayers);
            
            if (hit.collider && hit.collider.isTrigger == false && hit.collider != _collider)
            {
                var other = hit.collider;
                var closestPoint = other.ClosestPoint(outPosition);
                var distance = Vector2.Distance(closestPoint, outPosition);

                var cursorIsOutside = !outPosition.RectContains(closestPoint, _cursorPosition, 
                    collisionSettings.obstacleDetectionRadius, collisionSettings.isDebugging);
                if (distance < collisionSettings.minDistance || (distance < collisionSettings.obstacleAvoidanceDistance && cursorIsOutside))
                    return true;
            }
            return false;
        }

        private bool CursorIsOverObstacle() => _hovered && _hovered != this;

        private Vector3 GetCursorPosition()
        {
            var distanceToScreen = _gameCamera.WorldToScreenPoint(transform.position).z;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = distanceToScreen;//transform.position.z - gameCamera.transform.position.z;
            _cursorPosition = _gameCamera.ScreenToWorldPoint(mousePosition);
            return _cursorPosition;
        }
        
        private void GetAngleDelta()
        {
            _currentAngle = GetAngle();
            _delta = Mathf.DeltaAngle(_previousAngle, _currentAngle);
            _previousAngle = _currentAngle;
        }
        private float GetAngle()
        {
            Vector3 relatedPosition = transform.position - GetCursorPosition();
            return Mathf.Atan2(relatedPosition.x, relatedPosition.y) * Mathf.Rad2Deg;
        }
    }
}
