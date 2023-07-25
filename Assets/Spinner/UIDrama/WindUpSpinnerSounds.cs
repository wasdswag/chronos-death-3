using UnityEngine;

namespace UIDrama
{
    public class WindUpSpinnerSounds : ProgressReactable
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] [Range(0, 5)] private int soundIndex;
        private AudioSource _audioSource;

        private void Start() => _audioSource = GetComponent<AudioSource>();
        protected override void SetProgress(int percent) => PlaySound(percent);


        private void PlaySound(int value)
        {
            AudioClip clip = value < 100 ? clips[soundIndex] : clips[3];
            if (value < 100)
                _audioSource.pitch = Mathf.Lerp(0.7f, 1.7f, 0.01f * value); 
        
            _audioSource.PlayOneShot(clip);
        }
    }
}
