using UnityEngine;
using UnityEngine.UI;

public class FinishPage : MonoBehaviour
{
    [SerializeField] Text winLoseText;
    public void WinOrLose(bool succes)
    {
        if (succes)
            winLoseText.text = "WIN";
        else winLoseText.text = "TIME OUT";
    }
}
