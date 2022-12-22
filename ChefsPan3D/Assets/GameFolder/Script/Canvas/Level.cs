using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    TextMeshProUGUI _levelTMP;

    private void Start() => _levelTMP = GetComponentInChildren<TextMeshProUGUI>();
    private void OnEnable() => GameManager.on_level_changed += SetLevelTMP;
    private void OnDisable() => GameManager.on_level_changed -= SetLevelTMP;
    public void SetLevelTMP(int current_level) => _levelTMP.text = "Level " + current_level.ToString();
}
