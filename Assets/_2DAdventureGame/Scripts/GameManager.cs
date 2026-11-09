using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Налаштування квесту")]
    public int totalRobots = 1;

    [Header("Посилання на об'єкти")]
    public GameObject player;
    public UIHandler uiHandler;

    private int repairedCount = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void FixRobot()
    {
        repairedCount++;
        Debug.Log($"Відремонтовано роботів: {repairedCount}/{totalRobots}");

        if (repairedCount >= totalRobots)
        {
            OnAllRobotsRepaired();
        }
    }

    private void OnAllRobotsRepaired()
    {
        Debug.Log("Усі роботи відремонтовані!");
    }

    public bool AreAllRobotsFixed()
    {
        return repairedCount >= totalRobots;
    }

    public int GetFixedCount()
    {
        return repairedCount;
    }
}
