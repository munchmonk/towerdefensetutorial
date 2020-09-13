﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundsSurvived : MonoBehaviour {
    public Text roundsText;

    // Similar to Start(), gets called every time the object is enabled
	void OnEnable() {
		StartCoroutine(AnimateText());
	}

    IEnumerator AnimateText() {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStats.Rounds) {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }

    }
}
