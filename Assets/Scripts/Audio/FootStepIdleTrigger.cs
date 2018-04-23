using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepIdleTrigger : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		FindObjectOfType<AudioManager> ().Stop ("step");
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		FindObjectOfType<AudioManager> ().Play ("step");
	}
}
