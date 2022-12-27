using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class DragDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    float timerForDoubleClick = 0.0f;
    float delay = 0.3f;
    bool isDoubleClick = false;

    ObjectType objectType;

    private void Awake() => objectType = GetComponent<ObjectType>();
    void Update()
    {
        if (isDoubleClick == true)
        {
            timerForDoubleClick += Time.deltaTime;
        }

        if (timerForDoubleClick >= delay)
        {
            timerForDoubleClick = 0.0f;
            isDoubleClick = false;
        }
    }
    void TrueFalse()
    {
        foreach (var _QuestionObj in Question.Instance.QuestionObjTypes)
        {
            if(_QuestionObj._objectData.WhichObj == objectType._objectData.WhichObj)
            {
                GameManager.Instance.TempTrueCount(true); return;
            }
        }

        transform.DOMove(AssetManager.Instance.Pan.transform.position, 0.2f).SetEase(Ease.Linear)
            .OnComplete(() => transform.DOMove(AssetManager.Instance.TrashPoint.transform.position, 1f).SetEase(Ease.Linear));
    }
    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
            isDoubleClick = true;
    }
    private void OnMouseDown()
    {
        if (objectType.GetCollectible()) 
        {
            if (isDoubleClick == true && timerForDoubleClick < delay)
            {
                objectType.SetCollectible(false);

                transform.DOMove(AssetManager.Instance.Pan.transform.position, 1f).SetEase(Ease.Linear)
                    .OnComplete(() => transform.DOMoveY((transform.position.y - 1f), 0.2f).OnComplete(() => TrueFalse()));
            }
        }
    }
}
