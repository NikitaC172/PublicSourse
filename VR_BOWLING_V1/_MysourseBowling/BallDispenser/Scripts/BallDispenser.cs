using UnityEngine;

public class BallDispenser : MonoBehaviour
{
    [SerializeField] private Transform _pointRespaun;
    [SerializeField] private ConsoleButton _consoleButton;
    [SerializeField] private Item _ballItem;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _consoleButton.Activated += SpawnBall;
    }

    private void OnDisable()
    {
        _consoleButton.Activated -= SpawnBall;
    }

    public void SpawnBall()
    {
        GameObject ball = _ballItem.gameObject;
        ball.SetActive(false);
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.position = _pointRespaun.position;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        _audioSource.Play();
    }
}
