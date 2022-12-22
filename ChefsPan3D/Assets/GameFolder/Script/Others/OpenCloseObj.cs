using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseObj : MonoBehaviour
{
    public void OpenObj(GameObject OpenedObj) => OpenedObj.SetActive(true);
    public void CloseObj(GameObject ClosedObj) => ClosedObj.SetActive(false);
}
