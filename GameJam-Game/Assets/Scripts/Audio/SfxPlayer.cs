using System.Collections;
using System.Linq;
using Scriptables.Audio;
using Unity.VisualScripting;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// This Component can be referenced in any other component which might play any Sfx. Be it a looping sound or just playing a Sfx oneshot.
    /// </summary>
    public class SfxPlayer : MonoBehaviour
    {
        private AudioSource m_loopingAudioSource;
        
        public void PlayLoopingSfx(SfxData sfxData)
        {
            if (this.m_loopingAudioSource == null)
            {
                this.m_loopingAudioSource = this.AddComponent<AudioSource>();
                this.m_loopingAudioSource.loop = true;
            }

            this.PlayClipOnAudioSource(sfxData, this.m_loopingAudioSource);
        }

        public void StopLoopingCurrentSfx()
        {
            this.m_loopingAudioSource.Stop();
        }
        
        public void PlayOneShot(SfxData sfxData)
        {
            if (sfxData == null)
                return;

            var audioSources = this.GetComponents<AudioSource>();

            if (audioSources.Any(audioSource => audioSource.clip == sfxData.AudioClip))
                return;
            
            this.StartCoroutine(this.PlayClipAndDestroySource(sfxData));
        }

        private IEnumerator PlayClipAndDestroySource(SfxData data)
        {
            var audioSource = this.AddComponent<AudioSource>();
            this.PlayClipOnAudioSource(data, audioSource);
            yield return new WaitForSeconds(data.AudioClip.length);
            Destroy(audioSource);
        }

        private void PlayClipOnAudioSource(SfxData sfxData, AudioSource audioSource)
        {
            audioSource.clip = sfxData.AudioClip;
            audioSource.volume = sfxData.Volume * GlobalSettings.Instance.SfxVolume;
            audioSource.Play();
        }

        public void MuteAll()
        {
            foreach (var audioSource in this.GetComponents<AudioSource>())
            {
                audioSource.Stop();
            }
        }
    }
}