using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public SongManager songManager;
    public GameObject notePrefab;
    public Transform[] laneSpawnPoints;
    public Transform[] laneHitPoints;
    public LaneManager[] laneManagers;

    public float approachTime = 1.5f;

    public List<NoteData> notes = new List<NoteData>();

    private int nextNoteIndex = 0;

    void Start()
    {
        notes.Add(new NoteData { lane = 0, hitTime = 2.0f, holdLength = 0f });
        notes.Add(new NoteData { lane = 1, hitTime = 2.5f, holdLength = 0f });
        notes.Add(new NoteData { lane = 2, hitTime = 3.0f, holdLength = 0f });
        notes.Add(new NoteData { lane = 3, hitTime = 3.5f, holdLength = 0f });
        notes.Add(new NoteData { lane = 0, hitTime = 4.0f, holdLength = 0f });
        notes.Add(new NoteData { lane = 2, hitTime = 4.5f, holdLength = 0f });
    }

    void Update()
    {
        while (nextNoteIndex < notes.Count &&
               notes[nextNoteIndex].hitTime <= songManager.SongTime + approachTime)
        {
            SpawnNote(notes[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    void SpawnNote(NoteData data)
    {
        GameObject note = Instantiate(notePrefab, laneSpawnPoints[data.lane].position, Quaternion.identity);

        NoteObject obj = note.GetComponent<NoteObject>();
        obj.Initialize(
            data,
            laneSpawnPoints[data.lane].position,
            laneHitPoints[data.lane].position,
            approachTime,
            songManager
        );

        laneManagers[data.lane].Register(obj);
    }
}
