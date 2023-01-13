using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject inGame;
    [SerializeField] GameObject MenuPage;
    [SerializeField] GameObject ShopPage;
    [SerializeField] GameObject SettingsPage;
    [SerializeField] GameObject WinFinishPage;
    [SerializeField] GameObject LoseFinishPage;
    [SerializeField] GameObject MoneyBG;

    List<GameObject> Golds2D = new List<GameObject>();
    Vector3 PanTransform;

    public void Start() => InstantiateGolds();
    public void ShowSettingsPage()
    {
        //inGame.SetActive(false);
        MenuPage.SetActive(false);
        ShopPage.SetActive(false);
        SettingsPage.SetActive(true);
        WinFinishPage.SetActive(false);
        LoseFinishPage.SetActive(false);
    }
    public void ShowShopPage()
    {
        //inGame.SetActive(false);
        MenuPage.SetActive(false);
        ShopPage.SetActive(true);
        SettingsPage.SetActive(false);
        WinFinishPage.SetActive(false);
        LoseFinishPage.SetActive(false);
    }
    public void ShowPlayScene()
    {
        inGame.SetActive(true);
        MenuPage.SetActive(false);
        ShopPage.SetActive(false);
        SettingsPage.SetActive(false);
        WinFinishPage.SetActive(false);
        LoseFinishPage.SetActive(false);
    }
    public void ShowMainMenu()
    {
        //inGame.SetActive(false);
        MenuPage.SetActive(true);
        ShopPage.SetActive(false);
        SettingsPage.SetActive(false);
        WinFinishPage.SetActive(false);
        LoseFinishPage.SetActive(false);
    }
    public void ShowFinishPage(bool succes)
    {
        //inGame.SetActive(false);

        if(succes)
            WinFinishPage.SetActive(true);
        else LoseFinishPage.SetActive(true);
    }
    public void StartWinGold() => StartCoroutine(StartGoldsMove());
    void InstantiateGolds()
    {
        PanTransform = RectTransformUtility.WorldToScreenPoint(Camera.main, AssetManager.Instance.Pan.transform.GetChild(0).transform.position);

        for (int i = 0; i < 15; i++)
        {
            if(Golds2D.Count >= 15) { break; };
            GameObject klon_gold = Instantiate(AssetManager.Instance.Gold2D);
            klon_gold.SetActive(false); klon_gold.transform.SetParent(MoneyBG.transform);
            klon_gold.transform.localScale = new Vector3(1, 1, 1); klon_gold.SetActive(false);
            Golds2D.Add(klon_gold);
        }
    }
    IEnumerator StartGoldsMove()
    {
        yield return null;
        for (int i = 0; i < Golds2D.Count; i++)
        {
            Golds2D[i].GetComponent<RectTransform>().position = PanTransform;
            Golds2D[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Golds2D[i].transform.DOLocalMove(new Vector3(0,0, Golds2D[i].transform.localPosition.z), 0.5f).SetEase(Ease.Linear);
        }
    }
}
