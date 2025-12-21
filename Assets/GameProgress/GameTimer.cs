using System;
using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] SpriteRenderer TimerBar;
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
            TimerBar.size = new Vector2(Mathf.Lerp(startWidth, 0, elapsed), TimerBar.size.y);
            yield return null;
        }
        
        Debug.Log("GAME OVER!");
    }
}
