
using TMPro;
using UnityEngine;

using System.Linq;
using Image = UnityEngine.UI.Image; 

public class FindLargestCube : MonoBehaviour
{
    private FindLargestCube uiManager; 
    
    public TextMeshProUGUI maxCubeNumberText;
    public TMP_Text[] SaveMaxCubeNumberText;
    public int cubeNumber = 0;
    public bool ActivText = true;
    
    public Color CubeColorS; 
    public Color NewCubeColorS;

    [Header("Налаштування кольору картинки UI")]
    public Image myImageComponent; // UI Image компонент для основного кольору
    public Image SaveMyImageComponent; // UI Image компонент для відображення збереженого кольору

    [Header("Налаштування кольору 3D Об'єкта")]
    public MeshRenderer cubeMeshRenderer; // MeshRenderer 3D куба

    [Header("Колір для обнулення")] // Новий заголовок для кольору обнулення
    public Color resetColor = Color.black; // Колір, на який буде обнулятися (за замовчуванням чорний)

    void Awake()
    {
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<FindLargestCube>();
        }
        
        if (maxCubeNumberText == null)
        {
            GameObject foundTextObject = GameObject.Find("MaxCubeNumberText");
            if (foundTextObject != null)
            {
                maxCubeNumberText = foundTextObject.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("Об'єкт 'MaxCubeNumberText' не знайдено на сцені або він не має компонента TextMeshProUGUI.");
            }
        }
        
        // Застосовуємо колір до maxCubeNumberText при старті (якщо потрібно)
        // Ваша логіка ActivText не передбачає зміни кольору, тому я не додаю тут.
        // Якщо потрібно, додайте: if (maxCubeNumberText != null) maxCubeNumberText.color = Color.white;

        // Завантажуємо збережені кольори для UI Image та 3D Cube MeshRenderer при старті
        if (SaveMyImageComponent != null)
        {
            SaveMyImageComponent.color = LoadColorFromPlayerPrefs("SaveMyImageColor", Color.white); // Завантажуємо колір для SaveMyImageComponent
        }
        if (cubeMeshRenderer != null && cubeMeshRenderer.material != null)
        {
            cubeMeshRenderer.material.color = LoadColorFromPlayerPrefs("CubeMeshColor", Color.white); // Завантажуємо колір для 3D об'єкта
        }


        if (SaveMaxCubeNumberText != null && SaveMaxCubeNumberText.Length > 0)
        {
            int savedNumber = PlayerPrefs.GetInt("MaxCubeNumber", 0);
            for (int i = 0; i < SaveMaxCubeNumberText.Length; i++)
            {
                if (SaveMaxCubeNumberText[i] != null)
                {
                    if(ActivText == true)
                        SaveMaxCubeNumberText[i].text = "SaveNumber: " + savedNumber.ToString();
                    else
                    {
                        SaveMaxCubeNumberText[i].text = savedNumber.ToString();
                    }
                }
            }
        }
    }
    

    public void ButtenUbdSeveZiro()
    {
        PlayerPrefs.SetInt("MaxCubeNumber", 0);
        PlayerPrefs.Save();
        Debug.Log("Збережене число MaxCubeNumber обнулено до 0 за допомогою кнопки 'Q'.");

        if (SaveMaxCubeNumberText != null && SaveMaxCubeNumberText.Length > 0)
        {
            for (int i = 0; i < SaveMaxCubeNumberText.Length; i++)
            {
                if (SaveMaxCubeNumberText[i] != null)
                {
                    if(ActivText == true)
                        SaveMaxCubeNumberText[i].text = "SaveNumber: " + PlayerPrefs.GetInt("MaxCubeNumber", 0).ToString();
                    else
                    {
                        SaveMaxCubeNumberText[i].text = PlayerPrefs.GetInt("MaxCubeNumber", 0).ToString();
                    }
                }
            }
        }
        if (myImageComponent != null)
        {
            myImageComponent.color = resetColor;
            SaveColorToPlayerPrefs(resetColor, "MyImageColor"); // Зберігаємо обнулений колір
        }
        if (SaveMyImageComponent != null)
        {
            SaveMyImageComponent.color = resetColor;
            SaveColorToPlayerPrefs(resetColor, "SaveMyImageColor"); // Зберігаємо обнулений колір
        }
        if (cubeMeshRenderer != null && cubeMeshRenderer.material != null)
        {
            cubeMeshRenderer.material.color = resetColor;
            SaveColorToPlayerPrefs(resetColor, "CubeMeshColor"); // Зберігаємо обнулений колір
        }
    }

    /// <summary>
    /// Зберігає компоненти кольору (R, G, B, A) у PlayerPrefs.
    /// </summary>
    /// <param name="colorToSave">Колір, який потрібно зберегти.</param>
    /// <param name="keyPrefix">Префікс для ключів PlayerPrefs (наприклад, "MyImageColor").</param>
    public void SaveColorToPlayerPrefs(Color colorToSave, string keyPrefix)
    {
        PlayerPrefs.SetFloat(keyPrefix + "_R", colorToSave.r);
        PlayerPrefs.SetFloat(keyPrefix + "_G", colorToSave.g);
        PlayerPrefs.SetFloat(keyPrefix + "_B", colorToSave.b);
        PlayerPrefs.SetFloat(keyPrefix + "_A", colorToSave.a);
        PlayerPrefs.Save();
        Debug.Log($"Колір збережено в PlayerPrefs з префіксом '{keyPrefix}': {colorToSave}");
    }

    /// <summary>
    /// Завантажує компоненти кольору (R, G, B, A) з PlayerPrefs.
    /// </summary>
    /// <param name="keyPrefix">Префікс для ключів PlayerPrefs (наприклад, "MyImageColor").</param>
    /// <param name="defaultColor">Колір за замовчуванням, якщо збережені дані не знайдено.</param>
    /// <returns>Завантажений колір або колір за замовчуванням.</returns>
    public Color LoadColorFromPlayerPrefs(string keyPrefix, Color defaultColor)
    {
        if (PlayerPrefs.HasKey(keyPrefix + "_R"))
        {
            float r = PlayerPrefs.GetFloat(keyPrefix + "_R");
            float g = PlayerPrefs.GetFloat(keyPrefix + "_G");
            float b = PlayerPrefs.GetFloat(keyPrefix + "_B");
            float a = PlayerPrefs.GetFloat(keyPrefix + "_A");
            Color loadedColor = new Color(r, g, b, a);
            Debug.Log($"Колір завантажено з PlayerPrefs з префіксом '{keyPrefix}': {loadedColor}");
            return loadedColor;
        }
        else
        {
            Debug.LogWarning($"Збережений колір з префіксом '{keyPrefix}' не знайдено. Повернено колір за замовчуванням: {defaultColor}");
            return defaultColor;
        }
    }

    // Цей метод тепер знаходитиме найбільший куб на сцені
    public void UpdateMaxCubeNumber()
    {
        Cube[] cubes = FindObjectsOfType<Cube>();

        if (cubes.Length > 0)
        {
            
            Cube maxCube = cubes.Aggregate((max, next) => next.CubeNumber > max.CubeNumber ? next : max);

            if (uiManager != null)
            {
                if (maxCubeNumberText != null)
                {
                    maxCubeNumberText.text = maxCube.CubeNumber.ToString();
                }
                
                cubeNumber = maxCube.CubeNumber;
                
                // Оновлюємо кольори UI Image та 3D куба на колір знайденого найбільшого куба
                if (myImageComponent != null)
                {
                    myImageComponent.color = maxCube.CubeColor;
                    // Зберігаємо колір myImageComponent після оновлення
                }
                

                // Оновлюємо тимчасові змінні кольору (якщо вони використовуються для інших цілей)
                CubeColorS = maxCube.CubeColor;

                int previouslySavedMaxCubeNumber = PlayerPrefs.GetInt("MaxCubeNumber", 0);
                
                if (SaveMaxCubeNumberText != null && SaveMaxCubeNumberText.Length > 0)
                {
                    NewCubeColorS = maxCube.CubeColor;

                    for (int i = 0; i < SaveMaxCubeNumberText.Length; i++)
                    {
                        if (SaveMaxCubeNumberText[i] != null)
                        {

                            if(ActivText == true)
                                SaveMaxCubeNumberText[i].text =  previouslySavedMaxCubeNumber.ToString();
                            else
                            {
                                SaveMaxCubeNumberText[i].text = previouslySavedMaxCubeNumber.ToString();
                            }
                        }
                    }
                }

                if (cubeNumber > previouslySavedMaxCubeNumber)
                {
                    
                    PlayerPrefs.SetInt("MaxCubeNumber", cubeNumber);
                    PlayerPrefs.Save();
                    Debug.Log($"Нове максимальне число куба збережено: {cubeNumber}");
                    
                    SaveColorToPlayerPrefs(NewCubeColorS, "SaveMyImageColor");
                    SaveColorToPlayerPrefs(NewCubeColorS, "CubeMeshColor");
                    SaveMyImageComponent.color = LoadColorFromPlayerPrefs("SaveMyImageColor", Color.white);
                    cubeMeshRenderer.material.color = LoadColorFromPlayerPrefs("CubeMeshColor", Color.white);
                }
           
            }
            else if (maxCubeNumberText != null)
            {
                maxCubeNumberText.text = maxCube.CubeNumber.ToString();
            }
            else
            {
                Debug.LogError(
                    "UiManager не знайдено. Переконайтеся, що він присутній на сцені та призначений FindLargestCube.");
            }
        }
        else // Якщо на сцені не знайдено жодного об'єкта Cube
        {
            Debug.LogWarning("На сцені не знайдено жодного об'єкта Cube.");
            if (uiManager != null && maxCubeNumberText != null)
            {
                maxCubeNumberText.text = "0"; // Або інше значення за замовчуванням
            }
            else if (maxCubeNumberText != null)
            {
                maxCubeNumberText.text = "0";
            }
            // Оновлюємо збережений текст, навіть якщо кубів немає (якщо масив і його елементи існують)
            if (SaveMaxCubeNumberText != null && SaveMaxCubeNumberText.Length > 0)
            {
                for (int i = 0; i < SaveMaxCubeNumberText.Length; i++)
                {
                    if (SaveMaxCubeNumberText[i] != null)
                    {
                        if(ActivText == true)
                            SaveMaxCubeNumberText[i].text = PlayerPrefs.GetInt("MaxCubeNumber", 0).ToString();
                        else
                        {
                            SaveMaxCubeNumberText[i].text = PlayerPrefs.GetInt("MaxCubeNumber", 2).ToString();
                        }
                    }
                }
            }
        }
    }
}
