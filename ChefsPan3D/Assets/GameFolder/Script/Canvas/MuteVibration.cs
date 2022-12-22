using UnityEngine;
using System.Linq;

public class MuteVibration : MonoBehaviour
{
    public Transform[] VibrationOnOffObj;
    void Start()
    {
        VibrationOnOffObj = GetComponentsInChildren<Transform>().Where(t => t.tag == "Vibration").ToArray();
        CheckMute();
    }
    public void MuteVibrations()
    {
        if (PlayerPrefs.GetInt("VibrationMute") == 0)
        {
            PlayerPrefs.SetInt("VibrationMute", 1);
            CheckMute();
            return;
        }
        if (PlayerPrefs.GetInt("VibrationMute") == 1)
        {
            PlayerPrefs.SetInt("VibrationMute", 0);
            CheckMute();
        }
    }
    private void CheckMute()
    {
        if (PlayerPrefs.GetInt("VibrationMute") == 0)
        {
            VibrationOnOffObj[0].gameObject.SetActive(true);
            VibrationOnOffObj[1].gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("VibrationMute") == 1)
        {
            VibrationOnOffObj[0].gameObject.SetActive(false);
            VibrationOnOffObj[1].gameObject.SetActive(true);
        }
    }
}
