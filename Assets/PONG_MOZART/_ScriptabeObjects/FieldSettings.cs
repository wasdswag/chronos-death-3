using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


[CreateAssetMenu (menuName = "Pong Field Settings")]
public class FieldSettings : ScriptableObject
{

    public enum Level {  Mozart, Stadler }

    [System.Serializable] public struct Boundaries
    {
        [FormerlySerializedAs("LeftSide")] public float leftSide;
        [FormerlySerializedAs("RightSide")] public float rightSide;
        [FormerlySerializedAs("UpSide")] public float upSide;
        [FormerlySerializedAs("DownSide")] public float downSide;

        public bool PositionWithinABoundaries(Vector2 position)
        {
            if( position.y > upSide   || position.y < downSide  || 
                position.x < leftSide || position.x > rightSide )
                return true;

            return false;
        }

        public float ClampByHeight(float y) => Mathf.Clamp(y, downSide, upSide);
        public float GetHeightLenght() => upSide - downSide;
    }

    [FormerlySerializedAs("Bounds")] public Boundaries bounds;
    [FormerlySerializedAs("DiceLevel")] public Level diceLevel;



}
