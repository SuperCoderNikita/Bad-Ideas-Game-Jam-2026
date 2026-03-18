using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private NoteData noteData;
    private Vector3 spawnPos;
    private Vector3 hitPos;
    private float approachTime;
    private SongManager songManager;

    public bool WasHit { get; private set; }

    public void Initialize(NoteData data, Vector3 spawn, Vector3 hit, float approach, SongManager manager)
    {
        noteData = data;
        spawnPos = spawn;
        hitPos = hit;
        approachTime = approach;
        songManager = manager;
    }

    void Update()
    {
        if (WasHit) return;

        float timeUntilHit = noteData.hitTime - songManager.SongTime;
        float t = 1f - (timeUntilHit / approachTime);
        t = Mathf.Clamp01(t);

        transform.position = Vector3.Lerp(spawnPos, hitPos, t);

        if (songManager.SongTime > noteData.hitTime + 0.15f)
        {
            Miss();
        }
    }

    public NoteData GetNoteData()
    {
        return noteData;
    }

    public void Hit()
    {
        WasHit = true;
        Destroy(gameObject);
    }

    void Miss()
    {
        WasHit = true;
        Destroy(gameObject);
    }
}