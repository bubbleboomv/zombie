using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // <-- ADD THIS

public class GameManager : MonoBehaviour
{
    public GameObject selectZombie;
    public GameObject[] zombies;
    public Vector3 selectedSize;
    public Vector3 pushForce;

    private InputAction next, prev, jump;
    private int selectIndex;

    public TMP_Text timerText;
    private float timer;

    // --- NEW: brains UI + game over ---
    public TMP_Text brainsText;
    private int brains;

    private bool gameOver;
    public GameObject loseScreen;

    void Start()
    {
        // --- NEW: init values/UI ---
        brains = 0;
        if (brainsText != null) brainsText.text = "BRAINS: 0";

        gameOver = false;
        if (loseScreen != null) loseScreen.SetActive(false);

        timer = 0f;
        if (timerText != null) timerText.text = "TIME: 0.0s";

        Time.timeScale = 1f;

        // --- your original setup ---
        SelectZombie(0);
        next = InputSystem.actions.FindAction("nextZombie");
        prev = InputSystem.actions.FindAction("prevZombie");
        jump = InputSystem.actions.FindAction("Jump");
    }

    void SelectZombie(int index)
    {
        if (selectZombie != null)
            selectZombie.transform.localScale = Vector3.one;

        selectZombie = zombies[index];
        selectZombie.transform.localScale = selectedSize;
        Debug.Log("selected:" + selectZombie);
    }

    private void Update()
    {
        if (gameOver) return; // --- NEW: stop inputs/timer when lose ---

        if (next.WasPressedThisFrame())
        {
            Debug.Log("next");
            selectIndex++;
            if (selectIndex >= zombies.Length)
                selectIndex = 0;
            SelectZombie(selectIndex);
        }

        if (prev.WasPressedThisFrame())
        {
            Debug.Log("Prev");
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = zombies.Length - 1;
            SelectZombie(selectIndex);
        }

        if (jump.WasPressedThisFrame())
        {
            Debug.Log("jump");
            Rigidbody rb = selectZombie.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(pushForce, ForceMode.Impulse); // small improvement
        }

        timer += Time.deltaTime;
        if (timerText != null)
            timerText.text = "TIME: " + timer.ToString("F1") + "s";
    }

    // --- NEW: called by brain pickups ---
    public void AddBrain(int amount = 1)
    {
        if (gameOver) return;

        brains += amount;
        if (brainsText != null)
            brainsText.text = "BRAINS: " + brains;
    }

    // --- NEW: called when zombie falls off ---
    public void Lose()
    {
        if (gameOver) return;

        gameOver = true;

        if (loseScreen != null)
            loseScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    // --- NEW: hook this to your Reset button OnClick ---
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}