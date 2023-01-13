using DG.Tweening;
using UnityEngine;

public class RaycastDoubleClick : MonoBehaviour
{
    Camera cam;
    ObjectType SelectedObj;

    float timerForDoubleClick = 0.0f;
    float delay = 0.3f;
    bool isDoubleClick = false;

    void Start() => cam = GetComponent<Camera>();
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedWithRay();
        }

        if (isDoubleClick == true)
        {
            timerForDoubleClick += Time.deltaTime;
        }

        if (timerForDoubleClick >= delay)
        {
            timerForDoubleClick = 0.0f;
            isDoubleClick = false;
            SelectedObj = null;
        }
    }

    void SelectedWithRay()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = cam.transform.position.z;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<ObjectType>())
            {
                isDoubleClick = true;

                if (SelectedObj == null || SelectedObj != hit.collider.GetComponent<ObjectType>())
                    SelectedObj = hit.collider.GetComponent<ObjectType>();
                else if (SelectedObj == hit.collider.GetComponent<ObjectType>() && isDoubleClick)
                {
                    if (SelectedObj.GetCollectible())
                    {
                        SelectedObj.SetCollectible(false);
                        SelectedObj.GetComponent<DoubleClick>().StartMovement();
                    }
                }
                else timerForDoubleClick = delay;
            }
        }
    }
}
