using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : MonoBehaviour {
    public Button btnEnterGame;

    private void Awake() {
        btnEnterGame.onClick.AddListener(delegate {
            LevelCtrl.enterLevel01();
        });
    }

}
