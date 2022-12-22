using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    AssetManager assetManager;

    GameObject[,] AllObjects = new GameObject[50, 50];

    void Start() => assetManager = AssetManager.Instance;

    public void SetLoadLevel()
    {
        for (int i = 0; i < assetManager.CollectibleObj.Length; i++)
        {
            for (int y = 0; y < Question.Instance.AmountValue; y++)
            {
                if(y >= AllObjects.GetLength(1)) { break; }
                if(AllObjects[i,y] == null)
                {
                    AllObjects[i, y] = Instantiate(assetManager.CollectibleObj[i]);
                    AllObjects[i, y].transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                }

                AllObjects[i, y].GetComponent<ObjectType>().SetCollectible(true);

                AllObjects[i, y].transform.position = new Vector3(
                        Random.Range(-1.5f, 1.5f),
                        Random.Range(-1.0f, 2.5f), i);

                AllObjects[i, y].SetActive(true);
            }
        }
    }
}
