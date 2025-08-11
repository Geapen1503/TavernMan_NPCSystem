using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    [Header("Core Components")]
    //public string id;
    public Rigidbody rigidBody;
    public Animator animator;
    public NPCAnchor defaultAnchor;
    public string defaultAnimation = "TPose";
    public CapsuleCollider npcCollider;
    public AudioSource npcAudioSource;

    [Header("Interaction Settings")]
    public bool isTalkable = true;
    public BoxCollider dialogDetectorCol;

    private bool isPlayerInRange = false;

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

    public void TriggerDialog()
    {
        if (!isTalkable) return;
        Debug.Log($"{name}: NPC's Talking!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isTalkable) return;
        if (!other.CompareTag("Player")) return;

        IsPlayerInRange = true;

        // Notify the player controller via the singleton
        var player = Invector.vCharacterController.vThirdPersonController.Instance;
        if (player != null)
        {
            player.SetCurrentNPC(this);
        }
        else
        {
            var fallback = FindObjectOfType<Invector.vCharacterController.vThirdPersonController>();
            if (fallback != null) fallback.SetCurrentNPC(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isTalkable) return;
        if (!other.CompareTag("Player")) return;

        IsPlayerInRange = false;

        var player = Invector.vCharacterController.vThirdPersonController.Instance;
        if (player != null)
        {
            player.ClearCurrentNPC(this);
        }
        else
        {
            var fallback = FindObjectOfType<Invector.vCharacterController.vThirdPersonController>();
            if (fallback != null) fallback.ClearCurrentNPC(this);
        }
    }

    public void CheckIfPlayerValid()
    {
        if (!animator || !npcCollider || !rigidBody || !npcAudioSource)
        {
            Debug.LogError($"{name}: Missing required NPC component(s).");
        }

        if (isTalkable && !dialogDetectorCol)
        {
            Debug.LogWarning($"{name}: Talkable NPC without a dialogDetectorCol assigned.");
        }
    }

    public bool IsPlayerInRange { get => isPlayerInRange; set => isPlayerInRange = value; }
}
