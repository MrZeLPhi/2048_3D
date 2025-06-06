using System.Collections.Generic;
using UnityEngine;
using TMPro; // Додаємо цей рядок для використання TextMeshPro
using UnityEngine.UI; // Додаємо цей рядок для використання компонента Image

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;
    public FindLargestCube _findLargestCube; 
    
    public Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow= true;
    
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] 
    [Tooltip("Призначте СЮДИ об'єкт, колір якого буде змінюватися (наприклад, порожній 3D-куб). Він повинен мати компонент MeshRenderer.")]
    private GameObject TestCube; // Об'єкт, колір якого буде змінюватися
    [SerializeField] private Color[] cubeColors;

    [SerializeField] public int maxCubeNumber; // in our case it's 4096 (2^12)

    private int maxPower = 24;
    private Vector3 defaultSpawnPosition;

    [Header("Наступний кубик UI")] // Заголовок для інспектора
    [Tooltip("Призначте текстове поле TextMeshProUGUI для відображення номера наступного кубика.")]
    public TMP_Text[] nextCubeNumberText ; // Поле для відображення номера наступного кубика

    [Tooltip("Призначте компонент Image, який є фоном для відображення номера наступного кубика.")]
    public Image nextCubeDisplayBackground; // Поле для фонового зображення UI

    private int _nextNumberToSpawn; // Змінна для зберігання номера наступного кубика, який буде спавнений

    private void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubesQueue();
        _nextNumberToSpawn = GenerateRandomNumber(); // Генеруємо перший номер для відображення
    }

    private void Start()
    {
        // При старті, показуємо номер першого випадкового кубика та його колір
        UpdateNextCubeNumberDisplay();
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
            AddCubeToQueue();
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();
        
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Cobes Queue] : no more cubes available in the pool");
                return null; // Якщо кубик не може бути спавнений, повертаємо null.
                            // Код нижче (оновлення _nextNumberToSpawn) не виконається.
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);
        

        if (_nextNumberToSpawn == number)
        {
            _nextNumberToSpawn = GenerateRandomNumber();
            _findLargestCube.UpdateMaxCubeNumber();
        }
        
        // Оновлюємо відображення наступного кубика (включаючи колір фону UI та 3D-об'єкта)
        UpdateNextCubeNumberDisplay();
        
        return cube;
    }

    public Cube SpawnRandom()
    {
        // Викликаємо Spawn, використовуючи _nextNumberToSpawn, який вже був згенерований
        return Spawn(_nextNumberToSpawn, defaultSpawnPosition);
    }

    public void DestroyCobe(Cube cube)
    {
        cube.CubeRigidbody.linearVelocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    private Color GetColor(int number)
    {
        // Перевірка на вихід за межі масиву
        int index = (int)(Mathf.Log(number) / Mathf.Log(2)) - 1;
        if (index >= 0 && index < cubeColors.Length)
        {
            return cubeColors[index];
        }
        else
        {
            Debug.LogWarning($"Не вдалося знайти колір для числа {number}. Перевірте cubeColors та логіку GetColor. Повернено білий колір.");
            return Color.white; // Повертаємо білий колір за замовчуванням
        }
    }

    /// <summary>
    /// Оновлює текстове поле UI, показуючи номер наступного кубика та його колір.
    /// А також змінює колір фонового зображення UI та 3D-об'єкта TestCube.
    /// </summary>
    private void UpdateNextCubeNumberDisplay()
    {
        if (nextCubeNumberText != null)
        {
            SetNumber0();
        }

        if (TestCube != null)
        {
            MeshRenderer testCubeMeshRenderer = TestCube.GetComponent<MeshRenderer>();
            if (testCubeMeshRenderer != null && testCubeMeshRenderer.material != null)
            {
                // Встановлюємо колір матеріалу TestCube на колір наступного кубика
                testCubeMeshRenderer.material.color = GetColor(_nextNumberToSpawn); 
            }

        }
    }
    public void SetNumber0()
    {
        for (int i = 0; i < nextCubeNumberText.Length; i++)
        {
            nextCubeNumberText[i].text = _nextNumberToSpawn.ToString();
        }
    }
}
