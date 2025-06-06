using UnityEngine;
using System.Collections; // Потрібно для використання Coroutines, якщо знадобиться анімація

public class CameraBackgroundChanger : MonoBehaviour
{
    [Tooltip("Призначте компонент Camera, фон якого потрібно змінити.")]
    public Camera targetCamera;

    [Range(-100f, 100f)] // Обмежуємо діапазон для зручності в інспекторі
    [Tooltip("Наскільки сильно затемнювати колір (0.01 - легке затемнення, 0.5 - значне затемнення).")]
    public float darkeningFactor = 100f; // Фактор затемнення за замовчуванням

    [Header("Колір фону з об'єкта")] // Новий заголовок для цього розділу
    [Tooltip("Призначте GameObject, колір якого буде використаний для фону камери.")]
    public GameObject obj1ToMatchColor; // Посилання на об'єкт, колір якого будемо брати

    void Start()
    {
        // Перевіряємо, чи призначено камеру. Якщо ні, спробуємо знайти основну камеру.
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null)
            {
                Debug.LogError("Камера не призначена для скрипта CameraBackgroundChanger на об'єкті " + gameObject.name + " і основну камеру не знайдено. Будь ласка, призначте її в інспекторі.");
                return; // Виходимо, якщо камеру не знайдено
            }
        }
        // При старті, колір фону камери буде той, який встановлений в інспекторі Unity
        // або колір за замовчуванням, якщо він не був встановлений.
    }

    void Update()   
    {
        // Натисніть 'D' для затемнення поточного кольору фону
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApplyDarkerShade();
            SetBackgroundColorFromObject();
        }
        
    }

    /// <summary>
    /// Застосовує темніший відтінок до поточного кольору фону камери.
    /// </summary>
    public void ApplyDarkerShade()
    {
        if (targetCamera != null)
        {
            // Отримуємо поточний колір
            Color currentColor = targetCamera.backgroundColor;

            // Створюємо новий колір, зменшуючи значення R, G, B на darkeningFactor
            // Clamp01 гарантує, що значення залишаються в діапазоні від 0 до 1
            Color darkerColor = new Color(
                Mathf.Clamp01(currentColor.r - darkeningFactor),
                Mathf.Clamp01(currentColor.g - darkeningFactor),
                Mathf.Clamp01(currentColor.b - darkeningFactor),
                currentColor.a // Альфа-канал залишаємо без змін
            );

            // Застосовуємо темніший колір
            targetCamera.backgroundColor = darkerColor;
            Debug.Log($"Колір фону камери затемнено до: {darkerColor}");
        }
        else
        {
            Debug.LogWarning("Камера не призначена. Неможливо затемнити колір фону.");
        }
    }

    /// <summary>
    /// Встановлює колір фону камери таким же, як колір матеріалу призначеного об'єкта.
    /// </summary>
    public void SetBackgroundColorFromObject()
    {
        if (targetCamera == null)
        {
            Debug.LogWarning("Камера не призначена. Неможливо встановити колір фону.");
            return;
        }

        if (obj1ToMatchColor == null)
        {
            Debug.LogWarning("Об'єкт для співставлення кольору (obj1ToMatchColor) не призначено. Будь ласка, призначте його в інспекторі.");
            return;
        }

        // Отримуємо компонент MeshRenderer з об'єкта
        MeshRenderer objMeshRenderer = obj1ToMatchColor.GetComponent<MeshRenderer>();
        if (objMeshRenderer != null && objMeshRenderer.material != null)
        {
            // Встановлюємо колір фону камери на колір матеріалу об'єкта
            targetCamera.backgroundColor = objMeshRenderer.material.color;
            Debug.Log($"Колір фону камери змінено на колір об'єкта '{obj1ToMatchColor.name}': {objMeshRenderer.material.color}");
        }
        else
        {
            Debug.LogWarning($"Об'єкт '{obj1ToMatchColor.name}' не має компонента MeshRenderer або його матеріалу. Неможливо отримати колір.");
        }
    }

    public void ZirovSave()
    {
        ApplyDarkerShade();
        SetBackgroundColorFromObject();
    }
}
