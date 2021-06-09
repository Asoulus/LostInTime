using UnityEngine;
using UnityEngine.UI;

public class ContinueGameCheck : MonoBehaviour
{
    public static ContinueGameCheck instance;

    private static string _continueKey = "ContinueCheck";

    [SerializeField]
    private Button _continueButton;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Debug.Log("lol");
        if (PlayerPrefs.GetInt(_continueKey) == 0)
        {
            _continueButton.interactable = false;
        }
        else
        {
            Debug.Log("lol");
            _continueButton.interactable = true;
        }
    }

    public void NewGame()
    {
        QuestHandler.instance.ResetQuestsEvent();
        LockWeapons.instance.NewGameLock();
        PlayerPrefs.SetInt(_continueKey, 1);
    }

}
