using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    public float displayTime = 4.0f;

    private VisualElement m_Healthbar;
    private VisualElement m_NonPlayerDialogue;
    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;
    private float m_TimerDisplay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;
            m_Healthbar = root.Q<VisualElement>("HealthBar");
            m_NonPlayerDialogue = root.Q<VisualElement>("NPCDialogue");
            m_LoseScreen = root.Q<VisualElement>("LoseScreenContainer");
            m_WinScreen = root.Q<VisualElement>("WinScreenContainer");

            if (m_NonPlayerDialogue != null)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }

        m_TimerDisplay = -1.0f;
        SetHealthValue(1.0f);
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                if (m_NonPlayerDialogue != null)
                {
                    m_NonPlayerDialogue.style.display = DisplayStyle.None;
                }
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        if (m_Healthbar != null)
        {
            m_Healthbar.style.width = Length.Percent(100 * percentage);
        }
    }

    public void DisplayDialogue()
    {
        if (m_NonPlayerDialogue != null)
        {
            m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
            m_TimerDisplay = displayTime;
        }
    }

    public void DisplayWinScreen()
    {
        if (m_WinScreen != null)
        {
            m_WinScreen.style.opacity = 1.0f;
        }
    }

    public void DisplayLoseScreen()
    {
        if (m_LoseScreen != null)
        {
            m_LoseScreen.style.opacity = 1.0f;
        }
    }
}