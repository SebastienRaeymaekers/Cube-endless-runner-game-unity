using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 endPos;
    public bool repeatable = false;
    public float speed = 1.0f;

    //TODO: calculate perfect duration en forwardspeed so that it matches other obstacles' speed.
    // we can alter this value which will change the duration of the trip van start to end when end pos is reached. (repeatable needs be enabled for it to work)
    public float duration = 3.0f;
    int forwardSpeed = 15;  // acts as its forwardspeed, but is just how many block down it wil move in a duration.


    float startTime, totalDistance;

    // Use this for initialization
    IEnumerator Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos, endPos);

        endPos -= new Vector3(0, 0, forwardSpeed/2);
        while (repeatable)
        {
            yield return RepeatLerp(startPos, endPos, duration);

            startPos -= new Vector3(0, 0, forwardSpeed);

            yield return RepeatLerp(endPos, startPos, duration);

            endPos -= new Vector3(0, 0, forwardSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!repeatable)
        {
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            this.transform.position = Vector3.Lerp(startPos, endPos, journeyFraction);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}