using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmInput : MonoBehaviour
{
    public LaneManager lane0;
    public LaneManager lane1;
    public LaneManager lane2;
    public LaneManager lane3;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.aKey.wasPressedThisFrame)
            lane0.OnLanePressed();

        if (keyboard.sKey.wasPressedThisFrame)
            lane1.OnLanePressed();

        if (keyboard.wKey.wasPressedThisFrame)
            lane2.OnLanePressed();

        if (keyboard.dKey.wasPressedThisFrame)
            lane3.OnLanePressed();
    }
}