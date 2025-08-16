using UnityEngine;

public class PuzzleAssemblyPoint : MonoBehaviour
{
    public GameObject completedPuzzlePrefab; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PuzzleManager.Instance.HasAllPieces())
            {
                AssemblePuzzle();
            }
            else
            {
                Debug.Log("You don’t have all the pieces yet!");
            }
        }
    }

    private void AssemblePuzzle()
    {
        Instantiate(completedPuzzlePrefab, transform.position, transform.rotation);
        Debug.Log("Puzzle Completed!");
    }
}
