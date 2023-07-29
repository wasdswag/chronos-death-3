using UnityEngine;
using NaughtyAttributes;

namespace UIDrama
{
    public abstract class SpriteDragger : MonoBehaviour, IMouseInteractable
    {
        protected Rigidbody2D Body;
        protected float Radius;
        protected bool CursorIsOutCollider; 
        protected CursorHandler CursorHandler;
        // { get; private set; }

        [Header("Move and Spin settings")]
        [SerializeField] protected float moveSpeed = 3000f;
        [SerializeField] [Range(0f, 2f)] private float spinDeadZone = 0.33f;
        [SerializeField] private bool allowRotation;

        [Space(10)] [Header("Physics settings")]
        [SerializeField] protected bool usePhysics;
        [ShowIf(nameof(usePhysics))] [SerializeField] protected float releaseGravity;

        [SerializeField] private bool avoidObstaclesOnDrag;
        [ShowIf(nameof(usePhysics))] [SerializeField] private LayerMask collisionLayers;

      
        protected bool mouseIsPressed { get; private set; }
        
        private Vector3 _cursorOffset;
    
        private Collider2D _collider;
        private float _shapeWidth;

        protected bool IsMoving { get; private set; }

        private bool _isCollide, _isCursorOverlapped;
        private Vector3 _obstacleDirection;
        
        private static SpriteDragger _hovered;


        public Vector3 Position => transform.position;
        public Vector3 CursorPosition { get; set; }
        
        private float _angle;
        private float _delta;
        private float _currentAngle;
        private float _previousAngle;

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
            CursorHandler = FindObjectOfType<CursorHandler>();
            Radius = _shapeWidth * .5f;
        }


        public virtual void OnEnter()
        {
            Debug.Log("Enter");
            if (IsMoving == false && _collider.isTrigger == false)
                _hovered = this;
            
            CursorHandler.SetCursor(Cursors.Over);
            
            if (mouseIsPressed) DisablePhysics();
            else CursorIsOutCollider = false;
        }

        public virtual void OnOver()
        {
            var cursor = CursorIsInsideObjectDragZone(DistanceToCursor()) ? Cursors.MoveOver : Cursors.SpinOver;
            CursorHandler.SetCursor(cursor);
        }

        public virtual void OnDown()
        {
            DisablePhysics();
            _cursorOffset = CursorPosition - Position;
            mouseIsPressed = true;
            CursorIsOutCollider = false;
        }

        public virtual void OnDrag()
        {
            Body.Sleep();
            if(CanMove()) TryMove();
            else if(allowRotation) Spin();
        }

        protected bool CanMove() => CursorIsOutCollider || CursorIsInsideObjectDragZone(DistanceToCursor());

        public virtual void OnUp()
        {
            Body.WakeUp();
            if (usePhysics)
            {
                Body.isKinematic = false;
                Body.gravityScale = releaseGravity;
            }

            mouseIsPressed = false;
            IsMoving = false;
            CursorHandler.SetCursor(Cursors.Regular);
        
        }

        public virtual void OnExit()
        {
            if (IsMoving == false) _hovered = null;
            
            CursorIsOutCollider = true;
            CursorHandler.SetCursor(Cursors.Regular);

            if (!mouseIsPressed)
            //      _cursorOffset = CursorPosition - Position;
            // else
                Body.gravityScale = releaseGravity;
        }

        
        public virtual void TryMove()
        {
            if (mouseIsPressed == false) return;
            Move();
        }

        protected virtual void Move()
        {
            CursorHandler.SetCursor(Cursors.Move);
            IsMoving = true;
            var desiredPosition = CursorPosition - _cursorOffset;
            var dir = (Position - CursorPosition).normalized;

            bool unblocked = !avoidObstaclesOnDrag || HasNoObstacleOnTheWay(Position, dir, CursorPosition);

            if (desiredPosition.IsOnTheScreen() && unblocked)
            {
                Body.MovePosition(Vector3.MoveTowards(Position, desiredPosition, moveSpeed * Time.deltaTime));
            }
        }
        
        protected virtual void Spin()
        {
            GetAngleDelta();
            transform.Rotate(Vector3.back, _delta);
            _angle += _delta;
            CursorHandler.SetCursor(Cursors.Spin);
        }
        
        private void GetAngleDelta()
        {
            _currentAngle = GetAngle();
            _delta = Mathf.DeltaAngle(_previousAngle, _currentAngle);
            _previousAngle = _currentAngle;
        }
        
        private float GetAngle()
        {
            Vector3 relatedPosition = Position - CursorPosition;
            return Mathf.Atan2(relatedPosition.x, relatedPosition.y) * Mathf.Rad2Deg;
        }

        protected float DistanceToCursor()
        {
            var distanceToCursor = Vector3.Distance(Position, CursorPosition);
            return distanceToCursor;
        }

        protected bool CursorIsInsideObjectDragZone(float cursorDistance)
        {
            bool isInside = cursorDistance < Radius * spinDeadZone;
            if (isInside) CursorHandler.SetCursor(Cursors.Move);
            return isInside;
        }

        private void DisablePhysics()
        {
            Body.angularVelocity = 0f;
            Body.velocity = Vector2.zero;
            Body.Sleep();
        }

        private bool HasNoObstacleOnTheWay(Vector2 origin, Vector2 direction, Vector3 cursorPos)
        {
            if (_collider.isTrigger || usePhysics == false) return true;
            if (CursorIsOverObstacle()) return false;
            var obstacle = Physics2D.Raycast(cursorPos, direction, 100f, collisionLayers).collider;
            if (obstacle && obstacle.isTrigger == false && obstacle != _collider)
                return false;
            
            return true;
        }
        private bool CursorIsOverObstacle() => _hovered && _hovered != this;
    }
}