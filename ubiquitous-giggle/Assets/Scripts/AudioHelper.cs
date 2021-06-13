using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper
{
    /// <summary>
    /// Plays a clip on the provided audio source
    /// </summary>
    /// <param name="source">Audio source to play through</param>
    /// <param name="clip">Clip to play through source</param>
    /// <param name="pitchVariance">amount of variance in pitch</param>
    public static void PlayClipAtSource(AudioSource source, AudioClip clip, float pitchVariance = 0.2f)
    {
        if (source && clip)
        {
            source.clip = clip;
            source.pitch = Random.Range(1 - (pitchVariance / 2), 1 + (pitchVariance / 2));
            source.Play();
        }
    }
}
