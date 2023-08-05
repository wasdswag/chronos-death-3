using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public class Field : MonoBehaviour
    {
        public static Vector4 ScreenBounds { get; private set; }
        
        [SerializeField] private Camera gameCamera;
        [SerializeField] private PhysicsMaterial2D edgePhysicsMaterial;
        [SerializeField] private bool makeFrameColliders = true;
        [SerializeField] private float edgeRadius = 0.8f;

        [SerializeField] private float zoomStep = 50f;
        private struct Edge
        {
            private EdgeCollider2D edgeCollider;

            public void Initialize(GameObject parent, float radius, PhysicsMaterial2D material)
            {
                edgeCollider = parent.AddComponent<EdgeCollider2D>();
                edgeCollider.edgeRadius = radius;
                edgeCollider.sharedMaterial = material;
            }

            public void SetPoints(List<Vector2> points)
            {
                if (edgeCollider != null)
                    edgeCollider.SetPoints(points);
            }
        }

        private Edge[] _edges = new Edge[4];
        private Vector3 _screenBoundsMin;
        private Vector3 _screenBoundsMax;


        private void Awake()
        {
            if(gameCamera == null) gameCamera = Camera.main;
            transform.position = Vector3.zero;

            if (gameCamera)
            {
                DefineScreenEdges();

                if (makeFrameColliders)
                {
                    for (int i = 0; i < _edges.Length; i++)
                        _edges[i].Initialize(gameObject, edgeRadius, edgePhysicsMaterial);
                    AdjustColliders(_screenBoundsMin, _screenBoundsMax);
                }
            }
            else
            {
                throw new Exception("There is no attached camera or main camera in a scene");
            }
        }

        // void Update()
        // {
        //     var mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        //     if (mouseScroll >= 0.1f || mouseScroll <= -0.1f)
        //     {
        //         gameCamera.orthographicSize += mouseScroll * zoomStep * Time.deltaTime;
        //         UpdateScreenFrame();
        //     }
        // }

        public void UpdateScreenFrame()
        {
            DefineScreenEdges();
            if(makeFrameColliders)
             AdjustColliders(_screenBoundsMin, _screenBoundsMax);
        }

        private void DefineScreenEdges()
        {
            _screenBoundsMin = gameCamera.ScreenToWorldPoint(Vector3.zero);
            _screenBoundsMax = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            ScreenBounds = new Vector4(_screenBoundsMin.x, _screenBoundsMin.y, _screenBoundsMax.x, _screenBoundsMax.y);

            if (makeFrameColliders)
            {
                _screenBoundsMin = new Vector3(_screenBoundsMin.x - edgeRadius, _screenBoundsMin.y - edgeRadius, 0f);
                _screenBoundsMax = new Vector3(_screenBoundsMax.x + edgeRadius, _screenBoundsMax.y + edgeRadius, 0f);
            }
        }

        private void AdjustColliders(Vector3 screenBoundsMin, Vector3 screenBoundsMax)
        {
            var p1 = new Vector2(screenBoundsMin.x, screenBoundsMin.y) ;
            var p2 = new Vector2(screenBoundsMin.x, screenBoundsMax.y);
            SetEdge(p1, p2, 0);

            var p3 = new Vector2(screenBoundsMin.x, screenBoundsMin.y);
            var p4 = new Vector2(screenBoundsMax.x, screenBoundsMin.y);
            SetEdge(p3, p4, 1);

            var p5 = new Vector2(screenBoundsMax.x, screenBoundsMin.y);
            var p6 = new Vector2(screenBoundsMax.x, screenBoundsMax.y);
            SetEdge(p5, p6, 2);

            var p7 = new Vector2(screenBoundsMin.x, screenBoundsMax.y);
            var p8 = new Vector2(screenBoundsMax.x, screenBoundsMax.y);
            SetEdge(p7, p8, 3);
        }
        private void SetEdge(Vector2 first, Vector2 second, int edgeIndex)
        {
            if (edgeIndex < _edges.Length)
            {
                var points = new List<Vector2>();
                points.Add(first);
                points.Add(second);
                _edges[edgeIndex].SetPoints(points);
            }
        }
    }
}