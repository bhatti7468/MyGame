using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    [Header("Camera & Color Settings")]
    public Camera mainCamera;
    public Color[] backgroundColors; // અહીં તમે મનપસંદ કલર્સ ઉમેરી શકશો
    public float changeSpeed = 0.3f;  // કલર બદલાવાની સ્પીડ

    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float transitionProgress = 0f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // જો Inspector માં કલર અસાઇન ન કર્યા હોય તો ડિફોલ્ટ ડાર્ક કલર્સ સેટ થશે
        if (backgroundColors == null || backgroundColors.Length < 2)
        {
            backgroundColors = new Color[]
            {
                new Color(0.1f, 0.1f, 0.2f), // Blueish Dark
                new Color(0.2f, 0.1f, 0.3f), // Purple Dark
                new Color(0.1f, 0.2f, 0.2f), // Teal Dark
                new Color(0.2f, 0.2f, 0.1f)  // Olive Dark
            };
        }
    }

    void Update()
    {
        // Game Pause હોય ત્યારે કલર ચેન્જ અટકી જશે
        if (Time.timeScale == 0f) return;

        // સ્મૂથ કલર ટ્રાન્ઝિશન (Color Lerp)
        transitionProgress += Time.deltaTime * changeSpeed;
        mainCamera.backgroundColor = Color.Lerp(backgroundColors[currentColorIndex], backgroundColors[targetColorIndex], transitionProgress);

        // એક કલર પૂરો થાય એટલે પછીના કલર તરફ જવું
        if (transitionProgress >= 1f)
        {
            transitionProgress = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex = (targetColorIndex + 1) % backgroundColors.Length;
        }
    }
}