using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;
using static Question;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Starting = 0,
        Awake = 1,
        Bonus = 2,
        Win = 3,
        Lose = 4,
    }

    public GameState game_state { get; private set; }

    public delegate void OnStateChanged(GameState current_state);
    public static event OnStateChanged on_state_changed;

    public delegate void OnLevelChanged(int current_level);
    public static event OnLevelChanged on_level_changed;

    public delegate void OnMoneyChanged(int current_amount);
    public static event OnMoneyChanged on_money_changed;

    int TempCount = 0;
    ObjectType[] ObjectTypes;
    TextMeshPro[] TextMeshProS;

    protected override void Awake()
    {
        ChangeState(GameState.Awake);
        base.Awake();
    }
    public void StartGame()
    {
        ChangeState(GameState.Starting);
    }
    public void NextLevel()
    {
        UIManager.Instance.ShowPlayScene();
        SetLevel(1, true);
        StartGame();
    }
    public void Bonus()
    {
        if (PlayerPrefs.GetInt("Money") >= 5)
            ChangeState(GameState.Bonus);
    }
    public void NewQuestion()
    {
        if (Question.Instance.newQuestion())
            LevelManager.Instance.SetLoadLevel();
        else
            ChangeState(GameState.Win);
    }
    public void TempTrueCount(bool add)
    {
        if (add)
            TempCount++;
        else TempCount = 0;

        if (TempCount == Question.Instance.questionSlots.Count * Question.Instance.AmountValue)
        {
            NewQuestion(); TempCount = 0;
            UIManager.Instance.StartWinGold();
            SoundManager.Instance.PlaySound(0);
            VibrationManager.Instance.VibrationWithDelay(50, .1f);
        }
    }
    public void SetLevel(int level_index, bool add)
    {
        if (add)
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + level_index);
        else
            PlayerPrefs.SetInt("Level", level_index);

        on_level_changed?.Invoke(PlayerPrefs.GetInt("Level"));
    }
    public void SetMoney(int amount, bool add)
    {
        if (add)
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + amount);
        else
            PlayerPrefs.SetInt("Money", amount);

        on_money_changed?.Invoke(PlayerPrefs.GetInt("Money"));
    }
    public void ChangeState(GameState new_state)
    {
        if (new_state == game_state) return;
        game_state = new_state;
        switch (new_state)
        {
            case GameState.Awake:
                HandleAwake();
                break;
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Bonus:
                HandleBonus();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }
        on_state_changed?.Invoke(new_state);
    }
    void HandleAwake()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
            SetLevel(1, true);
    }
    void HandleStarting()
    {
        TempCount = 0;
        SetLevel(PlayerPrefs.GetInt("Level"), false);
        SetMoney(PlayerPrefs.GetInt("Money"), false);
        UIManager.Instance.ShowPlayScene();
        Question.Instance.AmountAndQuestion();
        Question.Instance.GenerateRandom();
        LevelManager.Instance.SetLoadLevel();
        Timer.Instance.StartTimer();
        AssetManager.Instance.Pan.gameObject.SetActive(true);
        NewQuestion();
    }
    void HandleBonus()
    {
        HideOrShowObjects(true);
        Timer.Instance.StartTimer(30f);
        UIManager.Instance.ShowPlayScene();
        SetMoney(PlayerPrefs.GetInt("Money") - 5, false);
    }
    void HandleWin()
    {
        FindObjects();
        HideOrShowObjects(false);
        SetMoney(10, true);
        UIManager.Instance.ShowFinishPage(true);
        Timer.Instance.StopTimer();
    }
    void HandleLose()
    {
        FindObjects();
        HideOrShowObjects(false);
        Timer.Instance.StopTimer();
        UIManager.Instance.ShowFinishPage(false);
    }
    void FindObjects()
    {
        ObjectTypes = GameObject.FindObjectsOfType<ObjectType>();
        TextMeshProS = GameObject.FindObjectsOfType<TextMeshPro>();
    }
    void HideOrShowObjects(bool isShow)
    {
        foreach (var _objectType in ObjectTypes)
            _objectType.gameObject.SetActive(isShow);

        foreach (var _textMeshPro in TextMeshProS)
            _textMeshPro.gameObject.SetActive(isShow);

        AssetManager.Instance.Pan.gameObject.SetActive(isShow);
    }
}
