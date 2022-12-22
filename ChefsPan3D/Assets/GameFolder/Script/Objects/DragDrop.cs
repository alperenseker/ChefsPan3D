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
    bool ObjBlock = false;

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
    bool CheckArea()
    {
        if (transform.position.x > 1.5f)
        {
            objectType._objectData.isCollectible = false;
            transform.DOMoveX(1.5f, 0.1f).OnComplete(() => objectType._objectData.isCollectible = true);
        }
        else if (transform.position.x < -1.5f)
        {
            objectType._objectData.isCollectible = false;
            transform.DOMoveX(-1.5f, 0.1f).OnComplete(() => objectType._objectData.isCollectible = true);
        }
        else if (transform.position.y > 2.5f)
        {
            objectType._objectData.isCollectible = false;
            transform.DOMoveY(2.5f, 0.1f).OnComplete(() => objectType._objectData.isCollectible = true);
        }
        else if (transform.position.y < -1.0f)
        {
            objectType._objectData.isCollectible = false;
            transform.DOMoveY(-1.0f, 0.1f).OnComplete(() => objectType._objectData.isCollectible = true);
        }
        else objectType._objectData.isCollectible = true;

        return objectType._objectData.isCollectible;


    }
    void TrueFalse()
    {
        bool Succes = false;
        ObjBlock = true;

        foreach (var _QuestionObj in Question.Instance.QuestionObjTypes)
        {
            if(_QuestionObj._objectData.WhichObj == objectType._objectData.WhichObj)
            {
                Succes = true; break;
            }
        }

        if (Succes)
            GameManager.Instance.TempTrueCount(true);
        else
        {
            objectType.SetCollectible(false);


            transform.DOMove(AssetManager.Instance.Pan.transform.position, 0.2f).SetEase(Ease.Linear)
                .OnComplete(() => transform.DOMove(AssetManager.Instance.TrashPoint.transform.position, 1f).SetEase(Ease.Linear));
        }

    }
    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
            isDoubleClick = true;
    }
    private void OnMouseDown()
    {
        if (CheckArea() && !ObjBlock) 
        {
            if (isDoubleClick == true && timerForDoubleClick < delay)
            {
                objectType.SetCollectible(false);

                transform.DOMove(AssetManager.Instance.Pan.transform.position, 1f).SetEase(Ease.Linear)
                    .OnComplete(() => transform.DOMoveY((transform.position.y - 1f), 0.2f).OnComplete(() => TrueFalse()));

                return;
            }

            if (GameManager.Instance.game_state == GameManager.GameState.Starting)
            {
                screenPoint = Camera.main.WorldToScreenPoint(transform.position);

                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }
    }
    private void OnMouseDrag()
    {
        if (CheckArea() && !ObjBlock)
        {
            if (GameManager.Instance.game_state == GameManager.GameState.Starting)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
            }
        }
    }
}
