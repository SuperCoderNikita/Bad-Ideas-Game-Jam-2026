using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public int laneIndex;
    public SongManager songManager;

    private List<NoteObject> notesInLane = new List<NoteObject>();

    public void Register(NoteObject note)
    {
        notesInLane.Add(note);
    }

    public void OnLanePressed()
    {
        notesInLane.RemoveAll(n => n == null || n.WasHit);

        if (notesInLane.Count == 0)
        {
            Debug.Log("Miss");
            return;
        }

        NoteObject target = notesInLane[0];
        float inputTime = songManager.SongTime;
        float noteTime = target.GetNoteData().hitTime;

        float error = Mathf.Abs(noteTime - inputTime);

        if (error <= 0.045f)
        {
            Debug.Log("Sick");
            target.Hit();
            notesInLane.RemoveAt(0);
        }
        else if (error <= 0.090f)
        {
            Debug.Log("Good");
            target.Hit();
            notesInLane.RemoveAt(0);
        }
        else if (error <= 0.135f)
        {
            Debug.Log("Bad");
            target.Hit();
            notesInLane.RemoveAt(0);
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}