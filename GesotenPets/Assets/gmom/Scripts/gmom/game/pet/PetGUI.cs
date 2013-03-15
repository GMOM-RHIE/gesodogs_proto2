using UnityEngine;
using System.Collections;

public class PetGUI : MonoBehaviour {

	int selectedIndex = -1;
	Vector2 scrollPos;
	public bool imageDialogVisibility = false;
	public GUIContent imageDialogContent;
	public GUISkin PetSkin;
	
	public int screen_padding_x = 100;
	public int screen_padding_y = 10;
	public int button_padding_x = 5;
	public int button_padding_y = 5;
	
	public int button_size_x = 50;
	public int button_size_y = 50;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.skin = PetSkin;
		if (GUI.Button(new Rect(Screen.width - screen_padding_x,screen_padding_y,button_size_x,button_size_y),"detail","Ring_PetConditionBtn")) {
			Debug.Log("detail");
			//showPetDetailDialog();
		} else if (GUI.Button(new Rect(screen_padding_x,screen_padding_y,button_size_x,button_size_y),"feed", "Ring_PetFeedBtn")) {
			Debug.Log("CompleteFeedPet");
			Camera.main.SendMessage("CompleteFeedPet");
		} else if (GUI.Button(new Rect(screen_padding_x,button_size_y + button_padding_y,button_size_x,button_size_y),"love", "Ring_PetBrushBtn")) {
			Debug.Log("CompleteBrushPet");
			Camera.main.SendMessage("CompleteBrushPet");
		} else if (GUI.Button(new Rect(screen_padding_x,(button_size_y + button_padding_y) * 2,button_size_x,button_size_y),"call", "PetCallBtn")) {
			Debug.Log("called.");
			//TODO : move to default position.
		}

		/*
		if (imageDialogVisibility) {
			if (imageDialogContent.image != null) {
				int w = imageDialogContent.image.width;
				int h = imageDialogContent.image.height;
				GUI.Label(new Rect((Screen.width-w)/2,(Screen.height-h)/2,w,h),imageDialogContent);
				if (GUI.Button(new Rect((Screen.width+w)/2-20,(Screen.height-h)/2+5,15,15),"×","CloseButton")) {
					imageDialogVisibility = false;
				}
			}
		} else {*/
			/*if (GUI.Button(new Rect(50,50,50,50),"load")) {
				showImageDialog("/img/ui/icon_info.png");	
			}*/
		//}
	}
	/*
	void showPetItemDialog() {
		Rect scrollRect = new Rect(0,45,245,245);
		Rect scrollRectLabel = new Rect(5,5,250,275);
		GUI.BeginGroup(scrollRectLabel,"",PetSkin.GetStyle("PetItemScrollArea"));
		GUIStyle style = PetSkin.GetStyle("PetItemScroll");
		scrollPos = GUI.BeginScrollView(scrollRect, scrollPos,new Rect(0,0,230,(userPetItems.Length/3+(userPetItems.Length%3>0?1:0))*80),new GUIStyle(),style);
		GUILayout.BeginVertical();
		for(int i=0;i<userPetItems.Length/3+(userPetItems.Length%3>0?1:0);i++){
			GUILayout.BeginHorizontal("PetItemRow");
			for (int j=0;j<3;j++){
				int c=i*3+j;
				if (c >= userPetItems.Length) {
					GUILayout.Label("","PetBlankItem");
					continue;

				}

				GUILayout.BeginVertical();

				if(GUILayout.Button(itemLabels[c].image,PetSkin.GetStyle("PetItemButton")) && !PetItemConfirmDialogVisibility){///////////
					if (userPetItems[c].number > 0) {
						PetItemConfirmDialogVisibility = true;
						currentSelectPetItem=userPetItems[c];
						currentSelectPetItemContent=itemLabels[c];
						PetItemDialogVisibility = false;
					} else {
						MainUI_Component.OnCustomDialog("アイテムが不足しています！");	
					}
				};
				GUILayout.Label(userPetItems[c].number.ToString(),PetSkin.GetStyle("PetItemCountLabel"));
				GUILayout.EndVertical();

			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
		
		GUI.EndScrollView();

		if (GUI.Button(new Rect(225,5,15,15),"×","CloseButton")) {
			PetItemDialogVisibility = false;
			currentSelectPet = null;
			currentSelectPetData = null;
		}
		GUI.EndGroup();
	}*/
	/*
	void showPetItemConfirmDialog() {
		GUI.Label(new Rect(Screen.width/2 - 350/2, Screen.height/2 - 250/2, 350, 250), "", "Confirm_Dialog");
	    GUILayout.BeginArea(new Rect(Screen.width/2-300/2+10, Screen.height/2-230/2+16, 290, 190));
		GUILayout.Label(currentSelectPetData.name+" に",PetSkin.GetStyle("SentenceLabel"));
		GUILayout.BeginHorizontal();
		GUILayout.Label(currentSelectPetItemContent,PetSkin.GetStyle("SentenceLabel"));
		//GUILayout.Label("を与えます！",PetSkin.GetStyle("SentenceLabel"),GUILayout.Height(80));
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if (currentSelectPetItem.hunger_point > 0){
			GUILayout.Label("おなか ： +"+currentSelectPetItem.hunger_point,PetSkin.GetStyle("StatusLabel"));
		}
		if (currentSelectPetItem.love_point > 0){
			GUILayout.Label(" ",GUILayout.Width(3));
			GUILayout.Label("愛情 ： +"+currentSelectPetItem.love_point,PetSkin.GetStyle("StatusLabel"));
		}
		GUILayout.EndHorizontal();
	    GUILayout.FlexibleSpace();
	    GUILayout.Label(" ",GUILayout.Height(3));
	    GUILayout.BeginHorizontal();
		
	    GUILayout.FlexibleSpace();
	    if(GUILayout.Button("","Feed_ConfirmBtn", GUILayout.Width(128),GUILayout.Height(32)))
	    {
			currentSelectPetItem.number--;
			Application.ExternalCall("DoFeedPet", currentSelectPetData.user_pet_id,currentSelectPetItem.pet_item_id);
			feedingPetItem = currentSelectPetItem;
			currentSelectPetItem = null;
			PetItemConfirmDialogVisibility = false;
	    }
	    GUILayout.Label(" ",GUILayout.Width(3));
	    if(GUILayout.Button("","Feed_CancelBtn", GUILayout.Width(128),GUILayout.Height(32)))
	    {
		    currentSelectPetItem = null;
			PetItemConfirmDialogVisibility = false;
			PetItemDialogVisibility = true;
	    }
	    GUILayout.FlexibleSpace();
	    GUILayout.EndHorizontal();
	    GUILayout.FlexibleSpace();
	    GUILayout.EndArea();	
	}*/
	/*
	void showPetDetailDialog() {
		GUILayout.BeginArea(new Rect(5,5,250,170),"","PetDetailArea");
		//GUI.Label(new Rect(5,5,240,160),"","PetDetailSeet");
		if (GUI.Button(new Rect(220,5,20,20),"X","CloseButton")) {
			PetDetailDialogVisibility = false;
			currentSelectPet = null;
			currentSelectPetData = null;
		}
		if (currentSelectPetData.iconTexture != null) {
			GUI.Label(new Rect(180,5,65,65), currentSelectPetData.iconTexture);
		}
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal("PetDetailNameRow");
		GUILayout.Label(currentSelectPetData.name,"PetDetailName");
		//GUILayout.Label("ねんれい","PetDetailParameter");
			GUILayout.Label(currentSelectPetData.growth_point.ToString(),"PetAge");
		GUILayout.Label("歳","PetAgeSai");
		GUILayout.EndHorizontal();
		
		
		GUILayout.BeginHorizontal("PetDetailParameterRow");
		//GUILayout.Label("あいじょう","PetDetailParameter");
		int p =currentSelectPetData.love_point/20;
		for(int i=0;i<p;i++) {
			GUILayout.Label(lovePointIcon,"PetDetailParameterIcon");
		}
		for (int j=0;j<5-p;j++) {
			GUILayout.Label(nolovePointIcon,"PetDetailParameterIcon");
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("PetDetailParameterRow");
		//GUILayout.Label("おなか","PetDetailParameter");
	    p=currentSelectPetData.hunger_point/20;
		for(int i=0;i<p;i++) {
			GUILayout.Label(hungryPointIcon,"PetDetailParameterIcon");
		}
		for (int j=0;j<5-p;j++) {
			GUILayout.Label(nohungryPointIcon,"PetDetailParameterIcon");
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("PetDetailParameterRow");
		//GUILayout.Label("ごきげん","PetDetailParameter");
		p=currentSelectPetData.feeling_point/20;
		for(int i=0;i<p;i++) {
			GUILayout.Label(feelingPointIcon,"PetDetailParameterIcon");
		}
		for (int j=0;j<5-p;j++) {
			GUILayout.Label(nofeelingPointIcon,"PetDetailParameterIcon");
		}
		GUILayout.EndHorizontal();
		//Debug.Log(currentSelectPetData.pet_condition);
		//GUILayout.Label(conditionNames[currentSelectPetData.pet_condition],"PetDetailCondition"+currentSelectPetData.pet_condition);

		
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}*/
	
	/*
	public void setPetDetailDialogVisible(GameObject pet) {
		currentSelectPet = pet;
		PetMove move = pet.GetComponent("PetMove") as PetMove;
		if (move != null) {
			currentSelectPetData = move.petDetail;
			PetDetailDialogVisibility = true;
				PetItemDialogVisibility = false;
		} else {
			currentSelectPet = null;
		}
	}*/
	/*
	public void setPetItemDialogVisible(GameObject pet) {
		currentSelectPet = pet;
		PetMove move = pet.GetComponent("PetMove") as PetMove;
		if (move != null) {
			currentSelectPetData = move.petDetail;
			if (userPetItems == null) {
				Application.ExternalCall("GetPetItem", "");
			} else {
				PetItemDialogVisibility = true;
				PetDetailDialogVisibility = false;
			}
		} else {
			currentSelectPet = null;
		}
	}*/
	/*
	public void showImageDialog(string url) {
		imageDialogVisibility = true;
		imageDialogContent = new GUIContent();
		StartCoroutine(getIcon(url,imageDialogContent));
	}*/
	
}
