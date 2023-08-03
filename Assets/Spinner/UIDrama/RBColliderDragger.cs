using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;


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

        protected Collider2D SelfCollider { get; private set; }

        [Header("Move and Spin settings")]
        [SerializeField] protected float moveSpeed = 3000f;
        [SerializeField] protected bool isRotatable;
        [ShowIf(nameof(isRotatable))] [SerializeField] [Range(0f, 2f)] private float spinDeadZone = 0.33f;

        [Space(10)] [Header("Physics settings")]
        [SerializeField] protected bool usePhysics;
        [ShowIf(nameof(usePhysics))] [SerializeField] protected float releaseGravity;

        [SerializeField] private bool avoidObstaclesOnDrag;
        [ShowIf(nameof(avoidObstaclesOnDrag))] [SerializeField] private bool avoidOnlyKinematics;
        [ShowIf(nameof(usePhysics))] [SerializeField] private LayerMask collisionLayers;

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
            SelfCollider = _collider;
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
            
            if (isRotatable == false)
                Body.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            else if(isRotatable)
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

        protected void Move()
        {
            // if (!_isSpinning)
            // {
                CursorHandler.SetCursor(Cursors.Move);
                _isMoving = true;
                var cursorPosition = GetCursorPosition();
                var desiredPosition = cursorPosition - _cursorOffset;
                var position = transform.position;
                var dir = (position - cursorPosition).normalized;

                bool unblocked = !avoidObstaclesOnDrag || HasNoObstacleOnTheWay(position, dir, cursorPosition);

                if (desiredPosition.IsOnTheScreen() && unblocked) 
                    Body.MovePosition(Vector3.MoveTowards(position, desiredPosition,
                        moveSpeed * Time.deltaTime));
          //  }
        }
        private bool HasNoObstacleOnTheWay(Vector2 origin, Vector2 direction, Vector3 cursorPos)
        {
            if (_collider.isTrigger || usePhysics == false) return true;
            if (CursorIsOverObstacle()) return false;

            var obstacle = Physics2D.Raycast(cursorPos, direction, 100f, collisionLayers).collider;
            if (obstacle != null && avoidOnlyKinematics && obstacle.TryGetComponent(out Rigidbody2D body) && body.isKinematic)
                return false;
            
            else if (obstacle && obstacle.isTrigger == false && obstacle != _collider)
                return false;
            
            return true;
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
