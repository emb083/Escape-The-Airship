using Unity.VisualScripting;
using UnityEngine;

public class EscapeShipAnimation : MonoBehaviour
{
    private Animator animator;
    public bool won;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (won)
        {
            animator.Play("GoodEnding");
        }
        else {
            animator.Play("EscapeShipIdle");
        }
    }
}
