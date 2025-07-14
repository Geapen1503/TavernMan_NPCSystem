using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    //public string id;
    public Animator animator;
    public NPCAnchor defaultAnchor;
    public string defaultAnimation = "TPose";

    void Start()
    {
        InitializeNPC(defaultAnchor);
    }

    public void InitializeNPC(NPCAnchor anchor)
    {
        if (anchor != null)
        {
            MoveToAnchor(anchor);

            string anim = !string.IsNullOrEmpty(anchor.defaultAnchorAnimation)
                ? anchor.defaultAnchorAnimation
                : defaultAnimation;

            PlayAnimation(anim);
        }
    }

    public void MoveToAnchor(NPCAnchor anchor)
    {
        if (anchor != null)
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null && !string.IsNullOrEmpty(animationName)) animator.SetTrigger(animationName);
    }
}
