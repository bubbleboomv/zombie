using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

   
    void Start()
    {
        SelectZombie(0);
        next = InputSystem.actions.FindAction("nextZombie");
        prev = InputSystem.actions.FindAction("prevZombie");
        jump = InputSystem.actions.FindAction("Jump");
    }

    void SelectZombie(int index)
    {
        if(selectZombie != null)
            selectZombie.transform.localScale = Vector3.one;
        selectZombie = zombies[index];
        selectZombie.transform.localScale = selectedSize;
        Debug.Log("selected:" + selectZombie);
    }

    private void Update()
    {
        if(next.WasPressedThisFrame())
        {
            Debug.Log("next");
            selectIndex++;
            if(selectIndex >= zombies.Length)
                selectIndex = 0;
            SelectZombie(selectIndex);
        }
        if(prev.WasPressedThisFrame())
        {
            Debug.Log("Prev");
            selectIndex--;
            if(selectIndex < 0)
                selectIndex = zombies.Length - 1;
            SelectZombie(selectIndex);
        }

        if (jump.WasPressedThisFrame())
        {
            Debug.Log("jump");
            Rigidbody rb = selectZombie.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(pushForce);
        }
        timer += Time.deltaTime;
        timerText.text = "TIME: " + timer.ToString("F1") +"s";

    }
}
