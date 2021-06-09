using System;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public static QuestHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onEnemyDeath;
    public event Action onResetQuests;

    public void EnemyDeathEvent()
    {
        if (onEnemyDeath != null)
        {
            onEnemyDeath();
        }
    }

    public void ResetQuestsEvent()
    {
        if (onResetQuests != null)
        {
            onResetQuests();
        }
    }

}
