//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIControl : MonoBehaviour
//{
//    public static UIControl Instance;
//    public Button _killBtn;
//    public bool HasTarget;
//    public Killable CurrentPlayer;

//   private void Awake()
//    {
//        Instance = this;
//    }

//    private void Update()
//    {
//        _killBtn.interactable = HasTarget;
//    }

//    public void OnKIllButtonPressed()
//    {
//        if (CurrentPlayer == null) { return; }
//        CurrentPlayer.Kill();
//    }
//}
