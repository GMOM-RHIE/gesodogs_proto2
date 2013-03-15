#define GESOTEN_BUILD_MODE_TEST
//#define GESOTEN_BUILD_MODE_RELEASE
using UnityEngine;
using System.Collections;

public class GlobalDefines : MonoBehaviour {
	
	private static GlobalDefines _instance = null;

    public static GlobalDefines instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(GlobalDefines)) as GlobalDefines;
            }
            if(_instance == null)
            {
                GameObject obj = new GameObject("GlobalDefines");
                _instance = obj.AddComponent(typeof(GlobalDefines)) as GlobalDefines;
            }
            return _instance;
        }
    }
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 public string AvatarLifeURL
    {
        get
        {
			#if GESOTEN_BUILD_MODE_TEST  
			            return "http://life.t.gesoten.com";
			#elif GESOTEN_BUILD_MODE_RELEASE
			            return "http://life.gesoten.com";
			#endif
        }
    }
}
