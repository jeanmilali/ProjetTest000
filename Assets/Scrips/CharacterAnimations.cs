using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public AudioClip sfxStomp;

    AudioSource source;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void SetValue(AnimParams _name)
    {
        animator.SetTrigger(_name.ToString());
    }

    public void SetValue(AnimParams _name, bool _value)
    {
        animator.SetBool(_name.ToString(), _value);
    }

    public void SetValue(AnimParams _name, float _value)
    {
        animator.SetFloat(_name.ToString(), _value);
    }

    // Moment où le pied touche par terre
    void Stomp(int value)
    {
        switch (value)
        {
            case 1:
                Debug.Log("Pied gauche");
                source.PlayOneShot(sfxStomp);
                break;
            case 2:
                Debug.Log("Pied droit");
                source.PlayOneShot(sfxStomp);
                break;
            default:
                break;
        }        
    }
}

public enum AnimParams
{
    Null,
    Forward,
    Strafe,
    Jump,
    YMCA,
    isMoving
}
