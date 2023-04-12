using UnityEngine;
using UnityEngine.UI;

public class SelectorInRoom : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;

    private bool _isActive = true;

    private void OnEnable()
    {
        _buttonBack.onClick.AddListener(ResetSelector);
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(ResetSelector);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isActive == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<PictureOpener>(out PictureOpener pictureOpener))
                {
                    if(pictureOpener.IsOpen == true)
                    {
                        if (hit.collider.gameObject.TryGetComponent<FrameStore>(out FrameStore picturePointFrameStore))
                        {
                            ShowFrames(picturePointFrameStore);
                        }
                    }
                    else
                    {
                        pictureOpener.TryOpenPicture();
                    }
                }
                else if (hit.collider.gameObject.TryGetComponent<FrameStore>(out FrameStore picturePointFrameStore))
                {
                    ShowFrames(picturePointFrameStore);
                }

            }
        }
    }

    private void ShowFrames(FrameStore picturePointFrameStore)
    {
        _isActive = false;
        picturePointFrameStore.ShowFrames();
    }

    private void ResetSelector()
    {
        _isActive = true;
    }
}
