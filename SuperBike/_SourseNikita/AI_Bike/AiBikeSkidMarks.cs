using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBikeSkidMarks : MonoBehaviour
{
    [SerializeField] private TrailRenderer skidMark;
    [SerializeField] private ParticleSystem smoke;
    public AiBikeController AiController;
    private void Awake()
    {
	    skidMark.emitting = false;
        skidMark.startWidth = AiController.SkidWidth;
    }
    
    
	private void OnEnable()
	{
		skidMark.enabled = true;
	}
	private void OnDisable()
	{
		skidMark.enabled = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AiController.grounded())
        {

            if (Mathf.Abs(AiController.CarVelocity.x) > 5)
            {
                skidMark.emitting = true;
            }
            else
            {
                skidMark.emitting = false;
            }
        }
        else
        {
            skidMark.emitting = false;
        }

        // smoke
        if (skidMark.emitting == true)
        {
            smoke.Play();
        }
        else { smoke.Stop(); }

    }
}
