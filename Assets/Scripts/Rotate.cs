using UnityEngine;

public enum RotateDir
{
    CLOCK,
    CTR_CLK
}

public class Rotate : MonoBehaviour
{
    public PipePuzzlePipe pipe;
    public RotateDir rotateDir;

    public void Grab()
    {
        if (rotateDir == RotateDir.CLOCK)
        {
            pipe.ChangeState(PipeStates.ROTATE_CLW);
        }
        else
        {
            pipe.ChangeState(PipeStates.ROTATE_CTRCLW);
        }
    }
}
