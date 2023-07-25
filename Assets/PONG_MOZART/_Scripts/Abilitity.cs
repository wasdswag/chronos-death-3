using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.Serialization;

public abstract class Abilitity : MonoBehaviour
{

    [FormerlySerializedAs("ActiveTime")] public float activeTime;
    [FormerlySerializedAs("Show")] public SpriteRenderer show;
    [FormerlySerializedAs("Continue")] public bool @continue;

    private CancellationTokenSource _cancellationToken;

    private void OnEnable()
    {
        show.color = Color.white;
        Life();
        _cancellationToken = new CancellationTokenSource();
    }

    private void Life()
    {

        show.color = Color.white;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float currentTime = activeTime;

            while (currentTime > 0f)
            {
                show.color = Color.Lerp(Color.clear, Color.white, currentTime / activeTime);
                currentTime -= Time.deltaTime;

                if (@continue)
                {
                    currentTime = activeTime;
                    @continue = false;
                }

                yield return null;
            }

           
            gameObject.SetActive(false);
            yield return null;
    }



}
