using UnityEngine;
using System.Collections;

public class TrailColorLoader : MonoBehaviour
{
    public CameraBackgroundChanger сameraBackgroundChanger;
    [Tooltip("Призначте компонент TrailRenderer, колір якого потрібно змінити.")]
    public TrailRenderer targetTrailRenderer;

    [Tooltip("Префікс ключа, який використовувався для збереження кольору в PlayerPrefs (наприклад, 'MyImageColor', 'SaveMyImageColor', 'CubeMeshColor').")]
    public string colorKeyPrefix = "MyImageColor"; // За замовчуванням, можна змінити в інспекторі

    [Tooltip("Колір за замовчуванням, якщо збережений колір не знайдено.")]
    public Color defaultTrailColor = Color.white; // Білий колір за замовчуванням

    public void Start()
    {
        // Перевіряємо, чи призначено TrailRenderer
        if (targetTrailRenderer == null)
        {
            Debug.LogError("TrailRenderer не призначено для скрипта TrailColorLoader на об'єкті " + gameObject.name + ". Будь ласка, призначте його в інспекторі.");
            return; // Виходимо, якщо компонент не призначено
        }

        // Завантажуємо колір з PlayerPrefs
        Color loadedColor = LoadColorFromPlayerPrefs(colorKeyPrefix, defaultTrailColor);

        // Застосовуємо завантажений колір до TrailRenderer
        // Зазвичай, TrailRenderer має startColor та endColor.
        // Ви можете застосувати loadedColor до обох або налаштувати за потреби.
        targetTrailRenderer.startColor = loadedColor;
        targetTrailRenderer.endColor = loadedColor;

        Debug.Log($"Колір для TrailRenderer '{targetTrailRenderer.name}' завантажено та встановлено: {loadedColor}");
        сameraBackgroundChanger.SetBackgroundColorFromObject();
        сameraBackgroundChanger.ApplyDarkerShade();
    }

    /// <summary>
    /// Завантажує компоненти кольору (R, G, B, A) з PlayerPrefs.
    /// Цей метод є копією з FindLargestCube для самостійності скрипта.
    /// </summary>
    /// <param name="keyPrefix">Префікс для ключів PlayerPrefs (наприклад, "MyImageColor").</param>
    /// <param name="defaultColor">Колір за замовчуванням, якщо збережені дані не знайдено.</param>
    /// <returns>Завантажений колір або колір за замовчуванням.</returns>
    private Color LoadColorFromPlayerPrefs(string keyPrefix, Color defaultColor)
    {
        if (PlayerPrefs.HasKey(keyPrefix))
        {
            float r = PlayerPrefs.GetFloat(keyPrefix + "_R");
            float g = PlayerPrefs.GetFloat(keyPrefix + "_G");
            float b = PlayerPrefs.GetFloat(keyPrefix + "_B");
            float a = PlayerPrefs.GetFloat(keyPrefix + "_A");
            Color loadedColor = new Color(r, g, b, a);
            // Debug.Log($"Колір завантажено з PlayerPrefs з префіксом '{keyPrefix}': {loadedColor}"); // Закоментовано, щоб уникнути дублювання логів
            return loadedColor;
        }
        else
        {
            // Debug.LogWarning($"Збережений колір з префіксом '{keyPrefix}' не знайдено. Повернено колір за замовчуванням: {defaultColor}"); // Закоментовано
            return defaultColor;
        }
    }
}
