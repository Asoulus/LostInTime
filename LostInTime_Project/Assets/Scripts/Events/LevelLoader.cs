using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static string level = "LevelOption";

    public Animator transition;

    public float transitionTime = 1f;

    public List<CanvasGroup> canvases = new List<CanvasGroup>();

    public void LoadLevel()
    {
        if (canvases.Count > 0)
        {
            foreach (var c in canvases)
            {
                c.alpha = 0;
                c.blocksRaycasts = false;
                c.interactable = false;
            }
        }

        StartCoroutine(DelayLoading(PlayerPrefs.GetString(level)));
    }

    public void LoadLevel(string _name)
    {
        if (_name != "Menu")
        {
            PlayerPrefs.SetString(level, _name);
            PlayerPrefs.Save();
        }


        if (canvases.Count > 0)
        {
            foreach (var c in canvases)
            {
                c.alpha = 0;
                c.blocksRaycasts = false;
                c.interactable = false;
            }
        }
        StartCoroutine(DelayLoading(_name));
    }

    private IEnumerator DelayLoading(string _lvlName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(_lvlName, LoadSceneMode.Single);
    }
}
