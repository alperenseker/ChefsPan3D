using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodType : MonoBehaviour
{
    [SerializeField] List<string> foodTypes = new List<string>();

    Text foodText;

    void Start() => foodText = GetComponent<Text>();
    public void newFoodName() => foodText.text = foodTypes[Random.Range(0, foodTypes.Count)];
}
