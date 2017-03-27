using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {

	KeywordRecognizer keywordRecognizer = null;
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	// Use this for initialization
	void Start() {
		keywords.Add("Reset", () => {
			Debug.Log("Speech recognition: Reset");
			// Call the OnReset method on every descendant object.
			this.BroadcastMessage("OnReset");
		});

		keywords.Add("Run", () => {
			Debug.Log("Speech recognition: Run");
			var focusObject = GazeGestureManager.Instance.FocusedObject;
			if (focusObject != null) {
				// Call the OnRun method on just the focused object.
				focusObject.SendMessage("OnRun");
			}
		});

		keywords.Add("Walk", () => {
			Debug.Log("Speech recognition: Walk");
			var focusObject = GazeGestureManager.Instance.FocusedObject;
			if (focusObject != null) {
				// Call the OnWalk method on just the focused object.
				focusObject.SendMessage("OnWalk");
			}
		});

		// Tell the KeywordRecognizer about our keywords.
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

		// Register a callback for the KeywordRecognizer and start recognizing!
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}

	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
		System.Action keywordAction;
		if (keywords.TryGetValue(args.text, out keywordAction)) {
			keywordAction.Invoke();
		}
	}
}