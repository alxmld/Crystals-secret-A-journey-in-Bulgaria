using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueScript; // Референция към скрипта, който управлява диалога
    public Button[] triggerButtons;  // Масив от бутони, които ще задействат диалога
    public float delayBeforeDialogue = 5f;  // Време за изчакване преди стартиране на диалога (в секунди)
    private bool isDialogueTriggered = false;  // Флаг, който предотвратява повторно стартиране на диалога

    void Start()
    {
        // Проверка дали е зададен скриптът за диалог
        if (dialogueScript == null)
        {
            Debug.LogError("Dialogue script is not assigned!");
            return;
        }

        // Проверка дали са зададени бутони
        if (triggerButtons.Length == 0)
        {
            Debug.LogError("No trigger buttons assigned!");
            return;
        }

        // Добавя OnButtonPress към onClick събитието на всеки бутон
        foreach (Button button in triggerButtons)
        {
            button.gameObject.SetActive(true); // Проверява видимостта на бутоните
            button.onClick.AddListener(() => OnButtonPress()); // Добавя слушател за натискане на бутона
        }
    }

    // Функция, която се изпълнява при натискане на бутон
    public void OnButtonPress()
    {
        // Ако диалогът още не е стартиран, стартираме го с отлагане
        if (!isDialogueTriggered)
        {
            isDialogueTriggered = true;
            StartCoroutine(DelayedDialogueStart());
        }
    }

    // Корутина, която изчаква преди да стартира диалога
    private IEnumerator DelayedDialogueStart()
    {
        yield return new WaitForSeconds(delayBeforeDialogue); // Изчаква зададеното време
        dialogueScript.gameObject.SetActive(true); // Активира обекта за диалога
        dialogueScript.StartDialogue(); // Стартира диалога
    }
}
