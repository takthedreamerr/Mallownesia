using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public int totalPieces = 5;  
    private bool[] collectedPieces;
    private int collectedCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectedPieces = new bool[totalPieces];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectPiece(int id)
    {
        if (id >= 0 && id < totalPieces && !collectedPieces[id])
        {
            collectedPieces[id] = true;
            collectedCount++;
            Debug.Log($"Collected piece {id}. Total: {collectedCount}/{totalPieces}");
        }
    }

    public bool HasAllPieces()
    {
        return collectedCount == totalPieces;
    }
}
