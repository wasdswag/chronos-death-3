using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{
    [SerializeField] private Texture2D [] frames;
    [SerializeField] private float speed;
    private MeshRenderer look;
    private int currentFrameIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        look = GetComponent<MeshRenderer>();
        Invoke(nameof(AnimateMaterial), 0f);
    }

    private void AnimateMaterial()
    {
        look.material.SetTexture("_MainTex", frames[currentFrameIndex]);

        if (currentFrameIndex < frames.Length - 1)
            currentFrameIndex++;
        else
            currentFrameIndex = 0;

        float fps = 1f / speed;
        Invoke(nameof(AnimateMaterial), fps);
        
    }
    
}
