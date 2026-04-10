using Unity.VisualScripting;
using UnityEngine;


public class MainShipAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("BadEnding");
    }

}
