using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;

public class Timer : Singleton<Timer>
{
    TextMeshProUGUI _timerTMP;

    void Start() => _timerTMP = GetComponentInChildren<TextMeshProUGUI>();

    public void StartTimer(float _timerTime = 60f)
    {
        DOVirtual.Float(_timerTime, 0f, _timerTime, t => DisplayTime(t)).SetEase(Ease.Linear).SetDelay(1f)
            .OnComplete(() => GameManager.Instance.ChangeState(GameManager.GameState.Lose));
    }
    public void StopTimer()
    {
        DisplayTime(0f);
        DOTween.KillAll();
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timerTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
