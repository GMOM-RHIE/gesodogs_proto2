using UnityEngine;
using System.Collections;

public class PetMove : MonoBehaviour
{
	public enum condition{
		normal=1,badmood=2,sick=3,goaway=4
	}
	public enum motion_state{
		idle,walk,eat,sleep,brush,stretch,sit
	}
	static int[,] MOTION_CHANGE = new int[,]{
		{10,49,0,10,10,10,10},
		{20,39,0,10,10,10,10},
		{20,50,0,30,0,0,0},
		{0,0,0,80,0,20,0},
		{10,20,0,0,50,10,10},
		{10,23,0,0,33,0,33},
		{9,10,0,0,20,10,50}
	};
	float r = 105.0f;
	const int DEFAULT_MOTION_INTERVAL = 10;
	float motionInterval= 0;
	public float walkInterval=0;
	public motion_state currentMotionState = motion_state.sit;
	// Use this for initialization
	
	public Vector3 MoveTargetPosition;
	
	enum ItemState
	{
		none,
	    touch,
	    mytouch
	};
	ItemState touchState = ItemState.none;
	
	/**
	 * pet data
	 **/	
	public PetJson petDetail = null;
//	Main3D Main3D_Component;
//	MainUI MainUI_Component;
	PetNetwork petNetworkComponent;
	Transform toucher;
	
	
	void Start ()
	{
		//Main3D_Component = Camera.main.GetComponent("Main3D") as Main3D;
		//MainUI_Component = Camera.main.GetComponent("MainUI") as MainUI;
		petNetworkComponent = Camera.main.GetComponent("PetNetwork") as PetNetwork;
		motionInterval = DEFAULT_MOTION_INTERVAL;
	}
	
	// Update is called once per frame
	void Update ()
	{
		motionInterval-=Time.deltaTime;
		if (motionInterval <= 0) {
			changeMotionState();
		}
		walkInterval-=Time.deltaTime;
		if (walkInterval <= 0) {
			MoveTargetPosition = new Vector3((float)Random.Range(-3.5f,3.5f),0f,(float)Random.Range(-1.0f,6.0f));
			walkInterval = Random.Range(4,10);
			/*if ( Application.loadedLevelName == "hiroba") {
				gameObject.GetComponent<NavMeshAgent>().SetDestination(MoveTargetPosition);
			}*/
		}
		
		if (currentMotionState == motion_state.walk && MoveTargetPosition != null) {
			//if ( Application.loadedLevelName == "default") {
				transform.rotation = Quaternion.LookRotation(MoveTargetPosition-transform.position);
				gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,MoveTargetPosition,Time.deltaTime/2);
				if (Vector3.Distance(gameObject.transform.position,MoveTargetPosition) < 0.1) {
					changeMotionState(motion_state.sit);	
				}
			//}
		}
	}
	
	void OnGUI ()
	{
	  	/*if(MainUI_Component != null)
	  	{
	
	      if(MainUI_Component.showGUI)
	      {*/
	        GUI.skin = petNetworkComponent.AvatarGameSkin;
			switch(touchState)
	        {
	        case ItemState.mytouch:
	          showPetMenu(true);
	          break;
	        case ItemState.touch:
	          showPetMenu(false);
	          break;
	        }
	      }
	  /*}
  	}*/
	
	void changeMotionState() {
		int r = Random.Range(0,100);
		int c = 0;
		for (int i = 0; i < MOTION_CHANGE.GetLength(0); i++) {
			c+=MOTION_CHANGE[(int)currentMotionState,i];
			if (c >= r) {
				if (System.Enum.IsDefined(typeof(motion_state), i)) {
					currentMotionState = (motion_state) i;
					if (Debug.isDebugBuild)Debug.Log ("MOTIONSTATE:"+currentMotionState);
					break;
				}	
			}
		}
//gameObject.animation.CrossFade(System.Enum.GetName(typeof(motion_state), currentMotionState));
	//	motionInterval = (float) Random.Range(DEFAULT_MOTION_INTERVAL-2.0f,DEFAULT_MOTION_INTERVAL+2.0f);
		startAnimation();
	}
	public void changeMotionState(motion_state state) {
		currentMotionState = state;
		startAnimation();
	}
	
	public void startAnimation() {
		motionInterval = (float) Random.Range(DEFAULT_MOTION_INTERVAL-2.0f,DEFAULT_MOTION_INTERVAL+2.0f);	
		if (currentMotionState == motion_state.walk && animation.IsPlaying("walk_loop") || currentMotionState == motion_state.sit && animation.IsPlaying("sit_loop")) {
			return;	
		}
		Debug.Log(motionInterval + "," + currentMotionState);
		
		if (currentMotionState == motion_state.walk && animation.IsPlaying("sit_loop")) {
			animation["standup2"].speed = -1;
			animation["standup2"].time = animation["standup2"].length;
			gameObject.animation.CrossFade("standup2");
			gameObject.animation.PlayQueued(System.Enum.GetName(typeof(motion_state), currentMotionState));
		} else {
			gameObject.animation.CrossFade(System.Enum.GetName(typeof(motion_state), currentMotionState));
		}
		switch(currentMotionState) {
		case motion_state.walk:
			gameObject.animation.PlayQueued("walk_loop");
			break;
		case motion_state.sit:
			gameObject.animation.PlayQueued("sit_loop");
			break;
		default:
			break;
		}
	}
	
	public void OnTouch(Transform _toucher) {
		Debug.LogWarning(_toucher.name);
		if(!PetNetwork.PetItemConfirmDialogVisibility && !PetNetwork.PetItemDialogVisibility) {
			//&& Application.loadedLevelName == "default" && Main3D_Component.SetContoler(gameObject)){
			toucher = _toucher;
	      	switch(touchState){
	        case ItemState.none:
	          //if((_toucher.gameObject.GetComponent("MoveTo") as MoveTo).user.user_lite.user_id == petDetail.user_id.ToString()){
	            touchState = ItemState.mytouch;
	          /*}else{
	            touchState = ItemState.touch;
	          }*/
	          break;
	        case ItemState.touch:
	          break;
			}	
		}
	}
	
	public void showPetMenu(bool isOwner) {
		Vector3 menu_pos = Camera.main.WorldToScreenPoint(transform.position+new Vector3(0,1,0));
	    if(menu_pos.x < r*0.25f || menu_pos.x > Screen.width - r*0.25f || menu_pos.y < 0 || menu_pos.y > Screen.height)
	    {
	      toucher = null;
	      touchState = ItemState.none;
//	      Main3D_Component.ResetContoler(gameObject);
	      return;
	    }
	    if(menu_pos.x < r){menu_pos.x = r;}
	    if(menu_pos.x > Screen.width - r){menu_pos.x = Screen.width - r;}
	    if(menu_pos.y < r*0.25f){menu_pos.y = r*0.25f;}
	    if(menu_pos.y > Screen.height - r){menu_pos.y = Screen.height - r;}
	
	    GUILayout.BeginArea(new Rect(menu_pos.x - 200, Screen.height - menu_pos.y -150, 400, 400));
	    if((isOwner || Debug.isDebugBuild) && GUI.Button(new Rect(200-32+r*Mathf.Cos(4.5f*Mathf.PI/4),200-32+r*Mathf.Sin(4.5f*Mathf.PI/4),56,65),"ごはん","Ring_PetFeedBtn"))
//	      && MainUI_Component.customDialogVisibility == false)
	    {
	      toucher = null;
	      touchState = ItemState.none;
//	      Main3D_Component.ResetContoler(gameObject);
			petNetworkComponent.setPetItemDialogVisible(this.gameObject);
	    }
	    if(GUI.Button(new Rect(200-32+r*Mathf.Cos(5.5f*Mathf.PI/4),200-32+r*Mathf.Sin(5.5f*Mathf.PI/4),56,65),"なでる","Ring_PetBrushBtn"))
//	      && MainUI_Component.customDialogVisibility == false)
	    {
	      Application.ExternalCall("DoBrushPet", petDetail.user_pet_id.ToString());
	      toucher = null;
	      touchState = ItemState.none;
//	      Main3D_Component.ResetContoler(gameObject);
	    }
	
	    if(GUI.Button(new Rect(200-39+r*Mathf.Cos(6.5f*Mathf.PI/4),200-32+r*Mathf.Sin(6.5f*Mathf.PI/4),56,65),"体調","Ring_PetConditionBtn"))
//	      && MainUI_Component.customDialogVisibility == false)
	    {
	      toucher = null;
	      touchState = ItemState.none;
//	      Main3D_Component.ResetContoler(gameObject);
		  petNetworkComponent.setPetDetailDialogVisible(this.gameObject);
	    }
	
	    if(GUI.Button(new Rect(200-32+r*Mathf.Cos(7.5f*Mathf.PI/4),200-32+r*Mathf.Sin(7.5f*Mathf.PI/4),70,64),"とじる","Ring_CloseBtn"))
//	      && MainUI_Component.customDialogVisibility == false)
	    {
	      toucher = null;
	      touchState = ItemState.none;
//	      Main3D_Component.ResetContoler(gameObject);
	    }
		GUILayout.EndArea();
	}
	void OnCollisionEnter(Collision collision) {
		if(LayerMask.LayerToName(collision.gameObject.layer) != "ground"){
			currentMotionState = motion_state.sit;
			startAnimation();
			transform.position -= transform.forward * Time.deltaTime*6;
			if (Debug.isDebugBuild)Debug.Log (collision.gameObject.name);
		}
	}
	
}