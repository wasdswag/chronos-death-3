using UnityEngine;

namespace UIDrama
{
    public static class Extentions 
    {
        public static bool IsOnTheScreen(this Vector3 position)
        {
            if (position.x < Field.ScreenBounds.x || position.y < Field.ScreenBounds.y
                                                  || position.x > Field.ScreenBounds.z
                                                  || position.y > Field.ScreenBounds.w)
                return false;

            return true;
        }

        public static bool RectContains(this Vector2 first, Vector2 second, Vector2 toCheck, float threshold, bool isDebugging)
        {
            
            var xBounds = first.x >= second.x ? 
                new Vector2 (second.x, first.x) :
                new Vector2(first.x, second.x);

            xBounds = new Vector2(xBounds.x - threshold, xBounds.y + threshold);
        
            var yBounds = first.y >= second.y ?  
                new Vector2 (second.y, first.y) :
                new Vector2(first.y, second.y);
        
            yBounds = new Vector2(yBounds.x - threshold, yBounds.y + threshold);
            

            if (isDebugging)
            {
                var a = new Vector3(xBounds.x, yBounds.x, 0f);
                var b = new Vector3(xBounds.y, yBounds.x, 0f);
                var c = new Vector3(xBounds.x, yBounds.y, 0f);
                var d = new Vector3(xBounds.y, yBounds.y, 0f);

                Debug.DrawLine(a, b, Color.magenta);
                Debug.DrawLine(b, d, Color.magenta);
                Debug.DrawLine(d, c, Color.magenta);
                Debug.DrawLine(c, a, Color.magenta);
            }

            if (toCheck.x < xBounds.x || toCheck.x > xBounds.y || toCheck.y < yBounds.x || toCheck.y > yBounds.y)
                return false;
            
            return true;

        }
    }
}