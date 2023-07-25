using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (menuName = "Graphics Storage")]
public class Graphics : ScriptableObject
{
    [FormerlySerializedAs("Dice")] public Sprite[] dice;
    [FormerlySerializedAs("Emoji")] public Sprite[] emoji;
}
