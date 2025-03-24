using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    private bool _tagged = false;
    private bool _tagImmune = false;

    public bool Tagged { get { return _tagged; } }

    public bool Tag()
    {

        //if tagged already dont do anything
        if (Tagged)
            return false;

        //if immune to tag dont do anything
        if (_tagImmune) return false;

        if (!_tagImmune)

            if (TryGetComponent(out TrailRenderer renderer))
                renderer.emitting = true;
        _tagged = true;
        return true;
    }

    private void SetTagImmuneFalse()
    {
        _tagImmune = false;
    }


    private void Start()
    {
        
            _tagged = _startTagged;
        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = _startTagged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if not tagged do nothing
        if (!Tagged) return;


        if(collision.gameObject.TryGetComponent(out TagSystem tagSystem))
        {
            //tag other player
            if (tagSystem.Tag())
            {
               
           
            _tagged = false;
            _tagImmune = true;
                if (TryGetComponent(out TrailRenderer renderer))
                    renderer.emitting = false;
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
            }
        }

        
    }

}
