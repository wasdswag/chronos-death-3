using UnityEngine;

public class Crowd : AudioPlayer
{
    [SerializeField] private AudioClip applause, boo, bravo;



    private void PlayBravo() => PlayClip(bravo);
    private void PlayApplause() => PlayClip(applause);
    private void PlayBoo() => PlayClip(boo);

    public override void PlayClip(AudioClip clip) => sound.PlayOneShot(clip);


    public override void SubscribeEvents()
    {
        Debug.Log("Crowd Subscribe events");
        if (boo == null) Debug.Log("No clip");
        Events.OnApplyBonus     += PlayBravo;
        Events.OnCompleteLoop   += PlayApplause;
        Events.OnFail           += PlayBoo;
    }

    public override void UnSubscribeEvents()
    {
        Events.OnApplyBonus -= PlayBravo;
        Events.OnCompleteLoop   -= PlayApplause;
        Events.OnFail           -= PlayBoo;
    }
}
