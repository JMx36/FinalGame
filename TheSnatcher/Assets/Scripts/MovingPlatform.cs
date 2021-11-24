using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float moveUnits;
    [SerializeField] private bool moveSideWays;
    private Vector3 targetPos;
    private Vector3 startPosition;
    private float time;
    public void Start()
    {
        if (!moveSideWays)
        {
            float y = transform.position.y + moveUnits;
            targetPos = new Vector3(transform.position.x, y, transform.position.z);
            startPosition = transform.position;
            StartCoroutine(MovePlatform());
        }
        else
        {
            float x = transform.position.y + moveUnits;
            targetPos = new Vector3(x, transform.position.y, transform.position.z);
            startPosition = transform.position;
            StartCoroutine(MovePlatform());
        }
    }
    private IEnumerator MovePlatform()
    {
        time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        targetPos = startPosition;
        startPosition = transform.position;
        StartCoroutine(MovePlatform());
    }
}
