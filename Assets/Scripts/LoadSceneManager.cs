//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class LoadSceneManager : MonoBehaviour 
//{

//	private void Awake() 
//	{
//        StartCoroutine(LoadGame());
//    }

//    private void Update () 
//	{
		
//	}

//    private IEnumerator LoadGame()
//    {
//        print("LoadGame");

//        //AsyncOperation ao = SceneManager.LoadScene(1, LoadSceneMode.Single);
//        ao.allowSceneActivation = false;

//        while (ao.progress < 0.9f)
//        {
//            yield return null;
//        }
//        //此处有黑屏过渡
//        ao.allowSceneActivation = true;
//        print("222");

//    }
//}
