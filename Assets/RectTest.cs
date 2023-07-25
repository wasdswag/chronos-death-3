using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RectTest : MonoBehaviour
{
    [SerializeField] private Transform first, second;

    [SerializeField] private SpriteRenderer cursor;

    [SerializeField] private float threshold;
    
    // Start is called before the first frame update
    void Update()
    {

        //var direction = (first.position - second.position).normalized;
        
        var xBounds = first.position.x > second.position.x ? 
            new Vector2 (second.position.x, first.position.x) :
            new Vector2(first.position.x, second.position.x);

        xBounds = new Vector2(xBounds.x - threshold, xBounds.y + threshold);
        
        
        var yBounds = first.position.y > second.position.y ?  
            new Vector2 (second.position.y, first.position.y) :
            new Vector2(first.position.y, second.position.y);
        
        yBounds = new Vector2(yBounds.x - threshold, yBounds.y + threshold);

        
 
        var cp = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
   
         var a = new Vector3(xBounds.x, yBounds.x, 0f);
         var b = new Vector3(xBounds.y, yBounds.x, 0f);
         var c = new Vector3(xBounds.x, yBounds.y, 0f);
         var d = new Vector3(xBounds.y, yBounds.y, 0f);
        
        Debug.DrawLine(a,b, Color.green);
        Debug.DrawLine(b,d, Color.green);
        Debug.DrawLine(d, c, Color.green);
        Debug.DrawLine(c, a, Color.green);

        if (cp.x < xBounds.x || cp.x > xBounds.y || cp.y < yBounds.x || cp.y > yBounds.y)
            cursor.color = Color.gray;
        else
            cursor.color = Color.yellow;
        
        // var width =  first.position.x - second.position.x;
        // var height = first.position.y - second.position.y;
        // var minW = width > 0f ? 1f : -1f;
        // var minH = height > 0f ? 1f : -1f;
        //
        // width += minW;
        // height += minH;
        // var pivot = new Vector2(second.position.x - width * 0.5f, second.position.y - height * .5f);
        //
        // rect = new Rect( pivot.x, pivot.y,  width, height);
        //
        //     
        // var a = new Vector3(rect.xMin, rect.yMin, 0f);
        // var b = new Vector3(rect.xMax, rect.yMin, 0f);
        // var c = new Vector3(rect.xMin, rect.yMax, 0f);
        // var d = new Vector3(rect.xMax, rect.yMax, 0f);
        //
        // Debug.DrawLine(a,b, Color.green);
        // Debug.DrawLine(b,d, Color.green);
        // Debug.DrawLine(d, c, Color.green);
        // Debug.DrawLine(c, a, Color.green);

      //  var cp = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
        // if (rect.Contains(cp, true))
        //     cursor.color = Color.yellow;
        // else cursor.color = Color.gray;
    }

}
