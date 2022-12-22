using UnityEngine;
using System.Linq;

public class MuteAudio : MonoBehaviour
{
    public Transform[] AudioOnOffObj;
    void Start()
    {
        AudioOnOffObj = GetComponentsInChildren<Transform>().Where(t => t.tag == "Sound").ToArray();
        CheckAudio();
    }
    public void MuteAudios()
    {
        if (PlayerPrefs.GetInt("AudioMute") == 0)
        {
            PlayerPrefs.SetInt("AudioMute", 1);
            CheckAudio();
            return;
        }
        if (PlayerPrefs.GetInt("AudioMute") == 1)
        {
            PlayerPrefs.SetInt("AudioMute", 0);
            CheckAudio();
        }
    }
    private void CheckAudio()
    {
        if (PlayerPrefs.GetInt("AudioMute") == 0)
        {
            AudioOnOffObj[0].gameObject.SetActive(true);
            AudioOnOffObj[1].gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("AudioMute") == 1)
        {
            AudioOnOffObj[0].gameObject.SetActive(false);
            AudioOnOffObj[1].gameObject.SetActive(true);
        }
    }
}
