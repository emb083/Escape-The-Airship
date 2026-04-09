using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipePuzzle : MonoBehaviour
{
    // set in inspector
    public List<PipePuzzlePipe> pipes;
    public GameObject gear;
    public Animator Gearfalling;
    // private fields
    //private Animator lidDoorAnimator;
    private int[] code;
    private bool isConnected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lidDoorAnimator = GetComponent<Animator>();
        code = new[] { 0, 1, 1, 0, 0, 2, 0, 2, 2, 0, 1, 0, 0, 0, 2, 2, 1,0 };
        gear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected) // avoids the sound from being spammed since it is in update.
        {
            return;
        }
        bool comboCorrect = true;
        for (int i = 0; i < pipes.Count; i++)
        {
            if (i < 5) { 
                comboCorrect &= ((pipes[i].GetNumber() % 2) == code[i]);
            }
            else
            {
                comboCorrect &= (pipes[i].GetNumber() == code[i]); 
            }

        }
        if (comboCorrect)
        {
            //foreach (var tumbler in tumblers) {
            //  tumbler.ChangeState(TumblerStates.DISABLED);
            //}
            isConnected = true; // if open is true the sound will be played.
            Debug.Log("Finished");
            pipes.ForEach(pipe => pipe.ChangeState(PipeStates.DISABLED));
            gear.SetActive(true);
            Gearfalling.SetTrigger("falling");
            SoundManager.Play(SoundType.FALL);

        }

    }
}

