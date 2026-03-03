using UnityEngine;
using System.Collections;

public class WorldInverter : MonoBehaviour
{
    public Transform worldRoot;

    public void FlipWorld()
    {
        Physics2D.gravity *= -1;
        StartCoroutine(RotateWorld());
    }

    IEnumerator RotateWorld()
    {
        Quaternion startRot = worldRoot.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, 180);

        float time = 0f;
        float duration = 1.5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            worldRoot.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        worldRoot.rotation = endRot;
    }
}