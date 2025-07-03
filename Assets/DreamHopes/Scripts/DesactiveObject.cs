using UnityEngine;

public class DesactiveObject : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactive", 0.5f);
    }
    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
