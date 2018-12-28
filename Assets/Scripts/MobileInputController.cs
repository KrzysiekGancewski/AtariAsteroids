using UnityEngine;

public class MobileInputController : MonoBehaviour {
    
	void Awake () {
        #if UNITY_ANDROID || UNITY_IOS
            SetChildButtonsActive(true);
        #endif
    }

    public void SetChildButtonsActive(bool state) {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(state);
        }
    }
}
