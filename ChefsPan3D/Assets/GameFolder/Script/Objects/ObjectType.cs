using System;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public ObjectData _objectData;

    [Serializable]
    public struct ObjectData
    {
        public int WhichObj;
        public Vector3 StartPosition;
        public Vector3 StartRotate;
        public bool isCollectible;
    }

    private void Awake() => _objectData.isCollectible = true;
    public void SetActive(bool _active) => gameObject.SetActive(_active);
    public void SetCollectible(bool _active) => _objectData.isCollectible = _active;
    public void SetTransform() => transform.position = _objectData.StartPosition;
    public void SetRotation() => transform.eulerAngles = _objectData.StartRotate;

}
