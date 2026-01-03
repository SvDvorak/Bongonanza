using System;
using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] SpriteRenderer TimerBar;
    [SerializeField] AnimationCurve ElapseCurve;
    Coroutine timerRoutine;
    float startWidth;

    private void Start()
    {
        startWidth = TimerBar.size.x;
        StartTimer(20f);
    }

    private void StartTimer(float time)
    {
        timerRoutine = StartCoroutine(TimerRoutine(time));
    }

    private IEnumerator TimerRoutine(float time)
    {
        var endTime = Time.time + time;

        while (Time.time < endTime)
        {
            var elapsed = 1 - Mathf.Clamp01((endTime - Time.time) / time);
            elapsed = ElapseCurve.Evaluate(elapsed);
            TimerBar.size = new Vector2(Mathf.Max(0, startWidth * (1 - elapsed)), TimerBar.size.y);
            yield return null;
        }
        
        Debug.Log("GAME OVER!");
    }
}
