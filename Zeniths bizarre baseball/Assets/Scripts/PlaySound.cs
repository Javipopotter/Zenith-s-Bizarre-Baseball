using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void Play(string name)
    {
        AudioManager.instance.Play(name);
    }
}
