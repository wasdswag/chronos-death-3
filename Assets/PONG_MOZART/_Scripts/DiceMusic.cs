using UnityEngine;


public class DiceMusic : AudioPlayer
{

    [SerializeField] private FieldSettings settings;
    [SerializeField] private AudioClip [] stadler;   
    [SerializeField] private AudioClip [] mozart;
                     private AudioClip [] _soundClips;

    [SerializeField] private int maxBeats = 16;
    [SerializeField] private int maxVariations = 11;

    private AudioClip[,] _tacts;


    public override void SubscribeEvents() 
    {
        Events.OnPassBall += SetClip;
    }

    public override void UnSubscribeEvents()
    {
        Events.OnPassBall -= SetClip;
    }


    private void Start()
    {
        _soundClips = settings.diceLevel == FieldSettings.Level.Mozart ? mozart : stadler;
        _tacts = SetClipsArray(maxBeats, maxVariations);
    }

    private AudioClip[,] SetClipsArray(int a, int b)
    {
        var clips = new AudioClip[a, b];

        int tact = 0;
        int part = 0;

        for (int i = 0; i < _soundClips.Length; i++)
        {
            if (i % 11 == 0)
            {
                tact++;
                part = 0;
            }

            clips[tact - 1, part] = _soundClips[i];
            part++;
        }

        return clips;
    }


    private void SetClip(Paddle paddle, Color color)
    {
        int variation = paddle.HeightIndex;
        PlayClip(_tacts[Events.beatCounter, variation]);
        Events.SetBeat(maxBeats - 1);
    }

    public override void PlayClip(AudioClip clip) => sound.PlayOneShot(clip);

}
