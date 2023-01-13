using Unity.VisualScripting;
using UnityEngine;

public class Raycast : Singleton<Raycast>
{
    Camera cam;
    ObjectType SelectedObj;
    Vector3 StartPos;

    [HideInInspector] public bool DraggingFlag = false;

    void Start() => cam = GetComponent<Camera>();
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DraggingFlag = true;
            SelectedWithRay();
        }
        if (Input.GetMouseButtonUp(0))
        {
            AddForceObj();
            ClearObj();
        }
    }
    private void FixedUpdate()
    {
        if (DraggingFlag)
            SelectedWithRay();
    }
    void SelectedWithRay()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = cam.transform.position.z;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(SelectedObj == null)
                if (SelectedObj = hit.collider.GetComponent<ObjectType>())
                    StartPos = SelectedObj.transform.position;


            Vector3 move = hit.point;
            move.y = 2f;

            MoveWithRay(move);
        }
    }
    void MoveWithRay(Vector3 move)
    {
        if (SelectedObj != null
         && SelectedObj.GetCollectible())
            SelectedObj.Rigidbody().MovePosition(move);
    }
    void AddForceObj()
    {
        if (SelectedObj != null
         && SelectedObj.GetCollectible() && DraggingFlag)
        {
            var mouseSpeed = Vector3.ClampMagnitude((StartPos - SelectedObj.transform.position), 400f);
            mouseSpeed.y = 0;

            SelectedObj.Rigidbody().AddForce(mouseSpeed * 250f * -1, ForceMode.Force);
        }

        DraggingFlag = false;

    }
    void ClearObj() => SelectedObj = null;
}
