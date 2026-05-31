using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;

    private float topScore = 0f;
    private bool isGameOver = false;

    private AudioSource fallSound;

    void Start()
    {
        fallSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        if (player == null) return;

        if (player.position.y > topScore)
        {
            topScore = player.position.y;
        }

        scoreText.text = "Score: " + Mathf.RoundToInt(topScore).ToString();

        if (player.position.y < Camera.main.transform.position.y - 6f)
        {
            if (fallSound != null)
            {
                fallSound.Play();
            }

            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }
}