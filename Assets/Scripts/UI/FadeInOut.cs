using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FadeInOut
{

    public static IEnumerator FadeImage(Image target, float duration, Color color)
    {
        if (target == null)
            yield break;

        float alpha = target.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            if (target == null)
                yield break;
            Color targetColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.color = targetColor;
            yield return null;
        }
        target.color = color;
    }

    public static IEnumerator FadeSprite(SpriteRenderer target, float duration, Color color)
    {
        if (target == null)
            yield break;

        float alpha = target.material.color.a;

        float t = 0f;
        while (t < 1.0f)
        {
            if (target == null)
                yield break;

            Color targetColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
            target.material.color = targetColor;

            t += Time.deltaTime / duration;

            yield return null;
        }
        target.material.color = color;
    }
    //推荐用第二种
    //public static IEnumerator FadeSound(AudioSource oldAudio, AudioSource newAudio, float duration)
    //{

    //    float volume = oldAudio.volume;

    //    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (duration / 2))
    //    {

    //        float targetVolume = Mathf.Lerp(volume, 0, t);
    //        oldAudio.volume = targetVolume;
    //        yield return null;
    //    }
    //    oldAudio.volume = 0;
    //    oldAudio.Stop();
    //    newAudio.Play();
    //    newAudio.volume = 0;
    //    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (duration / 2))
    //    {

    //        float targetVolume = Mathf.Lerp(volume, 1, t);
    //        newAudio.volume = targetVolume;
    //        yield return null;
    //    }
    //    newAudio.volume = 1;
    //}

    public static IEnumerator FadeSoundByCilp(AudioSource audioSource, AudioClip newAudio, float duration)
    {

        float volume = audioSource.volume;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (duration / 2))
        {

            float targetVolume = Mathf.Lerp(volume, 0, t);
            audioSource.volume = targetVolume;
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = newAudio;
        audioSource.Play();
        audioSource.volume = 0;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (duration / 2))
        {

            float targetVolume = Mathf.Lerp(volume, 1, t);
            audioSource.volume = targetVolume;
            yield return null;
        }
        audioSource.volume = 1;
    }
}
