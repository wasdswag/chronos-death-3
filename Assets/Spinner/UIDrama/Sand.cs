using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public class Sand : MonoBehaviour
    {
        [SerializeField] private Grain grain;
        [SerializeField] private int rows;
        [SerializeField] private float firstLineHeight = -0.5f;
        [SerializeField] private float xGap = 0.6f;
        [SerializeField] private float yGap = 0.4f;

        [SerializeField] private float bowlRectSize;

        private int _columns = 1;
        public List<Grain> Grains { get; private set; } = new List<Grain>();
    
        private void Awake()
        {

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < _columns; x++)
                    {
                        var xPos = x * xGap - y * xGap * 0.5f;
                        var yPos = firstLineHeight - y * yGap;
                        var position = new Vector3(xPos , yPos, 0f);
                        var sand = Instantiate(grain, transform);
                        sand.transform.localPosition = position;
                        Grains.Add(sand);
                    }
                    _columns++;
                }
        }

        public void GrainsInsideTheBowl()
        {
            Debug.Log("checking..");
            Vector2 position = transform.position;
            
            foreach (var g in Grains)
            {
                if (!position.RectContains(position, g.transform.position, bowlRectSize, true))
                    g.ResetPosition(transform.position);
            }
            
        }
    }
}