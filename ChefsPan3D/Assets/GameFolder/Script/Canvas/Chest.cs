using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform[] ChestImages;

    void Awake() => ChestImages = GetComponentsInChildren<Transform>().Where(t => t.tag == "Chest").ToArray();
    private void OnEnable() => Check();
    void Check()
    {
        if ((PlayerPrefs.GetInt("Level") % 3 == 0) && PlayerPrefs.GetInt("isCollected") == 0)
        {
            ChestImages[0].gameObject.SetActive(true);
            ChestImages[1].gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("isCollected", 1);
            ChestImages[0].gameObject.SetActive(false);
            ChestImages[1].gameObject.SetActive(true);
        }
    }
    public void CollectChest()
    {
        if (PlayerPrefs.GetInt("isCollected") == 0)
        {
            GameManager.Instance.SetMoney(5, true);
            PlayerPrefs.SetInt("isCollected", 1);
            Check();
        }
    }
}
