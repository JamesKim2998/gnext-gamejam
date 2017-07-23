using UnityEngine;

class SoundManager
{
    public static void Play(string path)
    {
        var clip = Resources.Load<AudioClip>("Sound/" + path);
        if (clip == null)
        {
            Debug.LogError("Clip not found: " + path);
            return;
        }

        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}