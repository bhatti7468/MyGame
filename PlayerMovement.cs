using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 2f; // રસ્તાઓ વચ્ચેનું અંતર
    public float moveSpeed = 15f;   // રસ્તો બદલવાની સ્પીડ

    private int currentLane = 1;    // 0 = Left, 1 = Center, 2 = Right
    private Vector3 targetPosition;

    void Start()
    {
        currentLane = 1; // ગેમ શરૂ થાય ત્યારે વચલા (Center) રસ્તા પર રહેશે
        UpdateTargetPosition();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        // Controls (Left/Right Arrow Keys અથવા A & D)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
            {
                currentLane--;
                UpdateTargetPosition();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2)
            {
                currentLane++;
                UpdateTargetPosition();
            }
        }

        // પ્લેયર સ્મૂથલી ટાર્ગેટ પોઝિશન પર જશે
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void UpdateTargetPosition()
    {
        float targetX = 0f;

        if (currentLane == 0) targetX = -laneDistance;      // ડાબો રસ્તો
        else if (currentLane == 1) targetX = 0f;             // વચલો રસ્તો
        else if (currentLane == 2) targetX = laneDistance;   // જમણો રસ્તો

        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}