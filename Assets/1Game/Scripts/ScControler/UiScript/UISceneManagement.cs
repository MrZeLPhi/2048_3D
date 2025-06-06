
using UnityEngine;

using UnityEngine.SceneManagement;


public class UISceneManagement : MonoBehaviour
{
    [SerializeField] public GameObject imageToToggle; // Посилання на зображення, яке потрібно вмикати/вимикати
    [SerializeField] public GameObject imageToToggle2; // Посилання на зображення, яке потрібно вмикати/вимикати

    // Функція для переходу на іншу сцену
    public void GoToScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Назва сцени для завантаження не вказана.");
            return;
        }

        // Виправлена назва сцени: "Scene1"
        SceneManager.LoadScene(sceneName);
    }

    // Функція для ввімкнення/вимкнення зображення
    public void SetImageonOff()
    {
        if (imageToToggle == null)
        {
            Debug.LogError("Не призначено посилання на Image.");
            return;
        }
        imageToToggle.SetActive(false);
    }
    public void SetImageonOn()
    {
        if (imageToToggle == null)
        {
            Debug.LogError("Не призначено посилання на Image.");
            return;
        }
        imageToToggle.SetActive(true);
    }
    public void SetImageon2Off()
    {
        if (imageToToggle == null)
        {
            Debug.LogError("Не призначено посилання на Image.");
            return;
        }
        imageToToggle2.SetActive(false);
    }
    public void SetImageo2nOn()
    {
        if (imageToToggle == null)
        {
            Debug.LogError("Не призначено посилання на Image.");
            return;
        }
        imageToToggle2.SetActive(true);
    }

    // Функція для виходу з гри
    public void ExitGame()
    {
#if UNITY_EDITOR //Якщо це редактор юніті
        UnityEditor.EditorApplication.isPlaying = false; //Зупиняємо відтворення
#else //Якщо це не редактор
        Application.Quit(); //Виходимо з додатку
#endif
    }
}
