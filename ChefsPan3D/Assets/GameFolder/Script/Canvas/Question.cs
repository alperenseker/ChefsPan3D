using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : Singleton<Question>
{
    [Serializable]
    public struct QuestionSlot
    {
        public Transform ObjectPoint;
        public TextMeshPro AmountText;
    }

    public List<QuestionSlot> questionSlots = new List<QuestionSlot>();

    public List<int> randomList = new List<int>();
    public List<ObjectType> QuestionObjTypes = new List<ObjectType>();

    FoodType foodType;

    public int AmountValue, QuestionValue;

    private void Start()
    {
        foodType = GetComponentInChildren<FoodType>();
        AmountAndQuestion();
    }
    public bool newQuestion()
    {
        if (randomList.Count >= questionSlots.Count)
        {
            ClearQuestions();
            SetQuestions();
            return true;
        }
        else return false;
    }
    public void ClearQuestions()
    {
        for (int i = 0; i < QuestionObjTypes.Count; i++)
            Destroy(QuestionObjTypes[i].gameObject);

        QuestionObjTypes.Clear();
    }
    public void SetQuestions()
    {
        if (foodType) foodType.newFoodName();

        for (int i = 0; i < questionSlots.Count; i++)
        {
            ObjectType klon_obj = Instantiate(AssetManager.Instance.CollectibleObj[randomList[0]].GetComponent<ObjectType>());
            klon_obj._objectData.StartPosition = questionSlots[i].ObjectPoint.position;
            klon_obj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            klon_obj.GetComponent<Rigidbody>().isKinematic = true;
            klon_obj.SetTransform(); klon_obj.SetCollectible(false); klon_obj.gameObject.SetActive(true);

            QuestionObjTypes.Add(klon_obj); randomList.RemoveAt(0);
            questionSlots[i].AmountText.text = AmountValue.ToString();
            questionSlots[i].AmountText.gameObject.SetActive(true);
        }
    }
    public void AmountAndQuestion()
    {
        AmountValue = (PlayerPrefs.GetInt("Level") / 3);
        QuestionValue = (PlayerPrefs.GetInt("Level") / 4);
        if (AmountValue == 0) AmountValue = 1;
        if (QuestionValue == 0) QuestionValue = 2;
    }
    public void GenerateRandom()
    {
        for (int i = 0; i < QuestionValue * questionSlots.Count; i++)
        {
            if(randomList.Count != QuestionValue * questionSlots.Count)
            {
                int maxNum = AssetManager.Instance.CollectibleObj.Count();

                int numToAdd = UnityEngine.Random.Range(0, maxNum);

                while (randomList.Contains(numToAdd))
                    numToAdd = UnityEngine.Random.Range(0, maxNum);

                randomList.Add(numToAdd);
            }
        }
    }
}
