using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    //public string id;
    public Rigidbody rigidBody;
    public Animator animator;
    public NPCAnchor defaultAnchor;
    public string defaultAnimation = "TPose";
    public CapsuleCollider npcCollider;
    public AudioSource npcAudioSource;

    void Start()
    {
        CheckIfPlayerValid();
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

    public void ResizeColliderForAnimation()
    {

    }
    
    public void CheckIfPlayerValid()
    {
        if (!animator || !npcCollider || !rigidBody || !npcAudioSource) Debug.LogError($"{name}: Missing required NPC component(s).");
    }
}
