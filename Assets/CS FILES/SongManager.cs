using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioSource musicSource;
    public float songOffset = 0f;

    private double songStartDspTime;
    private bool songStarted = false;

    public float SongTime { get; private set; }

    void Start()
    {
        double startTime = AudioSettings.dspTime + 1.0;
        songStartDspTime = startTime;
        musicSource.PlayScheduled(startTime);
        songStarted = true;
    }

    void Update()
    {
        if (!songStarted) return;

        SongTime = (float)(AudioSettings.dspTime - songStartDspTime) + songOffset;
    }
}