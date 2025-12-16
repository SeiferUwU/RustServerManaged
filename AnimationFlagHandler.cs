using UnityEngine;

public class AnimationFlagHandler : MonoBehaviour
{
	public Animator animator;

	public bool forceUpdateIfDisabled;

	public void SetBoolTrue(string name)
	{
		animator.SetBool(name, value: true);
		TryForceAnimation();
	}

	public void SetBoolFalse(string name)
	{
		animator.SetBool(name, value: false);
		TryForceAnimation();
	}

	private void TryForceAnimation()
	{
		if (forceUpdateIfDisabled && !animator.isActiveAndEnabled)
		{
			animator.enabled = true;
			animator.Update(10f);
			SingletonComponent<InvokeHandler>.Instance.Invoke(DisableAnimator, 2f);
		}
	}

	private void DisableAnimator()
	{
		if (!(animator == null))
		{
			animator.enabled = false;
		}
	}
}
