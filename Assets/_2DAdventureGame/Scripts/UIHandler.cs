using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; } // Синглтон для швидкого доступу

    public float displayTime = 4.0f;
    private VisualElement m_Healthbar;

    private void Awake()
    {
        instance = this; // Ініціалізація до викликів з інших Start()
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        // Важливо: назва елемента "HealthBar" точно збігається з UI Builder!
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
}
