using UnityEngine;
using UnityEngine.UI;

public class PanelClicker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Button>(out Button button))
        {
            button.onClick.Invoke();
        }
    }
}
