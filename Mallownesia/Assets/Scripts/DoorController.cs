using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;      // how much to rotate
    public float openSpeed = 2f;       // how fast it rotates
    public bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    
    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }

        transform.rotation = targetRotation; // snap to exact final rotation
    }

    public void OpenDoor()
    {
        if (!isOpen) // only open once
        {
            isOpen = true;

            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(RotateDoor(openRotation));
        }
    }
}
