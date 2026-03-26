using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class handler : MonoBehaviour
{
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject rightArrow;
    public GameObject leftArrow;

    private Transform upArrowTransform;
    private Transform downArrowTransform;
    private Transform rightArrowTransform;
    private Transform leftArrowTransform;

    public GameObject leftScore;
    public GameObject rightScore;
    public GameObject upScore;
    public GameObject downScore;

    private Animator upArrowAnimator;
    private Animator downArrowAnimator;
    private Animator rightArrowAnimator;
    private Animator leftArrowAnimator;

    public TMP_Text timerText;
    private float timer;

    Keyboard keyboard;

    private List<float> upScoreTimes = new List<float> {
        26.823f, 30.005f, 33.114f, 36.294f, 38.279f, 39.097f,
        41.055f, 42.205f, 43.212f, 44.192f, 45.171f, 47.946f,
        48.190f, 48.913f, 49.692f, 50.322f, 50.751f, 51.892f,
        52.299f, 53.491f, 54.874f, 55.661f, 56.626f, 57.044f,
        58.209f, 58.628f, 59.803f, 60.201f, 60.399f, 60.776f,
        61.171f, 61.569f, 61.973f, 63.787f, 64.577f, 65.331f,
        66.085f, 66.867f, 67.643f, 68.463f, 69.242f, 70.005f,
        70.865f, 71.566f, 72.423f, 73.199f, 74.016f, 74.792f,
        75.570f, 76.366f, 77.140f, 77.965f, 78.762f, 79.711f
    };

    private List<float> downScoreTimes = new List<float> {
        28.426f, 31.577f, 34.652f, 37.853f, 38.698f, 40.234f,
        41.644f, 42.619f, 43.782f, 44.929f, 46.558f, 47.743f,
        48.544f, 49.309f, 50.102f, 50.533f, 51.689f, 52.109f,
        53.292f, 53.684f, 54.505f, 55.251f, 56.009f, 56.442f,
        56.847f, 58.016f, 58.431f, 58.826f, 59.230f, 59.612f,
        60.002f, 63.169f, 63.569f, 64.366f, 64.926f, 65.695f,
        66.435f, 67.248f, 68.035f, 68.845f, 69.627f, 70.442f,
        71.236f, 72.024f, 72.785f, 73.596f, 74.391f, 75.196f,
        75.945f, 76.735f, 77.544f, 78.349f, 79.122f, 80.518f
    };

    private List<float> leftScoreTimes = new List<float> {
        26.045f, 29.210f, 32.346f, 35.492f, 38.081f, 38.918f,
        40.669f, 41.835f, 43.034f, 43.986f, 45.367f, 46.970f,
        50.919f, 52.470f, 53.897f, 57.236f, 62.342f, 63.967f,
        64.720f, 65.524f, 66.248f, 67.041f, 67.852f, 68.639f,
        69.443f, 70.252f, 71.051f, 71.847f, 72.598f, 73.384f,
        74.187f, 74.980f, 75.771f, 76.556f, 77.346f, 78.169f,
        78.930f, 80.135f
    };

    private List<float> rightScoreTimes = new List<float> {
        27.658f, 30.789f, 33.926f, 37.107f, 38.492f, 39.832f,
        41.452f, 42.416f, 43.398f, 44.570f, 46.163f, 51.328f,
        52.833f, 54.095f, 57.612f, 62.752f, 64.166f, 65.100f,
        65.856f, 66.612f, 67.422f, 68.219f, 69.016f, 69.838f,
        70.623f, 71.415f, 72.198f, 72.969f, 73.781f, 74.561f,
        75.363f, 76.133f, 76.904f, 77.737f, 78.539f, 79.299f,
        80.950f
    };

    private int upNoteIndex = 0;
    private int downNoteIndex = 0;
    private int leftNoteIndex = 0;
    private int rightNoteIndex = 0;

    void Start()
    {
        upArrowAnimator = upArrow.GetComponent<Animator>();
        downArrowAnimator = downArrow.GetComponent<Animator>();
        rightArrowAnimator = rightArrow.GetComponent<Animator>();
        leftArrowAnimator = leftArrow.GetComponent<Animator>();

        upArrowTransform = upArrow.GetComponent<Transform>();
        downArrowTransform = downArrow.GetComponent<Transform>();
        rightArrowTransform = rightArrow.GetComponent<Transform>();
        leftArrowTransform = leftArrow.GetComponent<Transform>();

        upArrowAnimator.SetBool("isPressed", false);
        downArrowAnimator.SetBool("isPressed", false);
        rightArrowAnimator.SetBool("isPressed", false);
        leftArrowAnimator.SetBool("isPressed", false);

        timer = 2.19f;

        keyboard = Keyboard.current;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F3");

        if (upNoteIndex < upScoreTimes.Count && Mathf.Abs(timer - upScoreTimes[upNoteIndex]) <= 0.1f)
        {
            spawnUpScore();
            upNoteIndex++;
        }

        if (downNoteIndex < downScoreTimes.Count && Mathf.Abs(timer - downScoreTimes[downNoteIndex]) <= 0.1f)
        {
            spawnDownScore();
            downNoteIndex++;
        }

        if (leftNoteIndex < leftScoreTimes.Count && Mathf.Abs(timer - leftScoreTimes[leftNoteIndex]) <= 0.1f)
        {
            spawnLeftScore();
            leftNoteIndex++;
        }

        if (rightNoteIndex < rightScoreTimes.Count && Mathf.Abs(timer - rightScoreTimes[rightNoteIndex]) <= 0.1f)
        {
            spawnRightScore();
            rightNoteIndex++;
        }

        upArrowAnimator.SetBool("isPressed", keyboard.upArrowKey.isPressed);
        downArrowAnimator.SetBool("isPressed", keyboard.downArrowKey.isPressed);
        leftArrowAnimator.SetBool("isPressed", keyboard.leftArrowKey.isPressed);
        rightArrowAnimator.SetBool("isPressed", keyboard.rightArrowKey.isPressed);
    }

    void spawnLeftScore() 
    { 
        Instantiate(leftScore, new Vector3(leftArrowTransform.position.x - 6, leftArrowTransform.position.y), leftArrowTransform.rotation); 
    }

    void spawnRightScore() 
    { 
        Instantiate(rightScore, new Vector3(rightArrowTransform.position.x + 6, rightArrowTransform.position.y), rightArrowTransform.rotation); 
    }

    void spawnUpScore()
    { 
        Instantiate(upScore, new Vector3(upArrowTransform.position.x, upArrowTransform.position.y + 6), upArrowTransform.rotation); 
    }

    void spawnDownScore()
    { 
        Instantiate(downScore, new Vector3(downArrowTransform.position.x, downArrowTransform.position.y - 6), downArrowTransform.rotation); 
    }
}