using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PetNetwork : MonoBehaviour
{
	// Use this for initialization
//	Main3D Main3D_Component;
//	MainUI MainUI_Component;
//	ItemAppearance ItemAppearance_Component;
	
	public static bool PetDetailDialogVisibility = false;
	public static bool PetItemDialogVisibility = false;
	public static bool PetItemConfirmDialogVisibility = false;
	public static GameObject currentSelectPet = null;
	public static PetJson currentSelectPetData = null;
	public static PetItemJson currentSelectPetItem = null;
	public static PetItemJson feedingPetItem = null;
	public static GUIContent currentSelectPetItemContent = null;
	public int MaxGrowth = 20;
	

	private PetItemJson[] userPetItems = null;
	private PetJson[] userPets = null;
	private PetMove[] userPetObjects = null;
	private GUIContent[] itemLabels = null;
	public GUIStyle GridGuiStyle;
	public GUIStyle GridItemGuiStyle;
	public GUIStyle ScrollGuiStyle;
	public GUISkin AvatarGameSkin;
	public Hashtable PetGameObjects;
	
	public Texture lovePointIcon;
	public Texture hungryPointIcon;
	public Texture feelingPointIcon;
	public Texture nolovePointIcon;
	public Texture nohungryPointIcon;
	public Texture nofeelingPointIcon;
	public Texture petIcon;
	
	public Texture BadMoodTexture;
	public Texture SickTexture;
	
	public string[] conditionNames;
	void Start ()
	{
		//Main3D_Component = gameObject.GetComponent("Main3D") as Main3D;
		//MainUI_Component = gameObject.GetComponent("MainUI") as MainUI;
	    //ItemAppearance_Component = GameObject.Find("/3di/room_item").GetComponent("ItemAppearance") as ItemAppearance;
		if (Application.loadedLevelName == "default") {
			Application.ExternalCall("GetRoomPet", "");
		}
		SetRoomPet("[{\"description\":\"ねこ\",\"effect\":\"\",\"feeling_point\":60,\"flag\":0,\"growth_point\":20,\"love_point\":30,\"hunger_point\":20,\"image_url\":\"/img/item/pet/dog_01.png\",\"model\":\"prefab/pet/pet_001\",\"name\":\"ハチ公ああああああ\",\"pet_condition\":2,\"pet_id\":1,\"price\":100,\"rarity\":0,\"texture\":\"textures/pet/pet_01_001\",\"user_id\":106,\"user_pet_id\":1,\"brushable\":true},{\"description\":\"ねこ\",\"effect\":\"\",\"feeling_point\":10,\"flag\":0,\"growth_point\":2,\"love_point\":10,\"hunger_point\":10,\"image_url\":\"/img/item/pet/dog_01.png\",\"model\":\"prefab/pet/pet_001\",\"name\":\"HACHI\",\"pet_condition\":3,\"pet_id\":1,\"price\":100,\"rarity\":0,\"texture\":\"textures/pet/pet_01_001\",\"user_id\":106,\"user_pet_id\":20,\"brushable\":true}]");		
		SetRoomPet("[{\"description\":\"ねこ\",\"effect\":\"\",\"feeling_point\":60,\"flag\":0,\"growth_point\":20,\"love_point\":30,\"hunger_point\":20,\"image_url\":\"/img/item/pet/dog_01.png\",\"model\":\"prefab/pet/pet_001\",\"name\":\"ハチ公ああああああ\",\"pet_condition\":2,\"pet_id\":1,\"price\":100,\"rarity\":0,\"texture\":\"textures/pet/pet_01_001\",\"user_id\":106,\"user_pet_id\":1,\"brushable\":true},{\"description\":\"ねこ\",\"effect\":\"\",\"feeling_point\":10,\"flag\":0,\"growth_point\":2,\"love_point\":10,\"hunger_point\":10,\"image_url\":\"/img/item/pet/dog_01.png\",\"model\":\"prefab/pet/pet_001\",\"name\":\"HACHI\",\"pet_condition\":3,\"pet_id\":1,\"price\":100,\"rarity\":0,\"texture\":\"textures/pet/pet_01_001\",\"user_id\":106,\"user_pet_id\":20,\"brushable\":true}]");		
		SetPetItem("" +
			"[{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":1,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"無添加ビスケット\",\"particle_amount\":20}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":2,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM2\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":3,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM3\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":4,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM4\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":5,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM5\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":6,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM6\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":7,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEM7\"}" +
			",{\"consumable\":1,\"description\":\"ペットの食べるものじゃない\",\"feeling_point\":0,\"growth_point\":0,\"hunger_point\":10,\"image_url\":\"/img/item/pet/biscuit.png\",\"item_type\":1,\"number\":8,\"pet_item_id\":1,\"price\":0,\"rarity\":0,\"user_id\":106,\"user_pet_item_id\":1,\"name\":\"TEST_ITEMJ\"}]");
		PetItemDialogVisibility = false;
		//CompleteFeedPet("{\"description\":\"ねこ\",\"effect\":\"\",\"feeling_point\":60,\"flag\":0,\"growth_point\":3,\"love_point\":30,\"hunger_point\":20,\"image_url\":\"/img/item/pet/biscuit.png\",\"model\":\"prefab/room/doll/doll_02\",\"name\":\"ハチ公\",\"pet_condition\":0,\"pet_id\":1,\"price\":100,\"rarity\":0,\"texture\":\"textures/room/kotatsu/kotatsu_02/kotatsu_02_002\",\"user_id\":106,\"user_pet_id\":1,\"brushable\":true}");
		CompleteBrushPet();
	}
	
	public void SetRoomPet(string json) {
		//if (Debug.isDebugBuild) {
			Debug.Log(json);	
		//}
		if (PetGameObjects != null) {
			foreach(DictionaryEntry ent in PetGameObjects) {
				Destroy((GameObject)ent.Value);
			}
		}
		PetGameObjects = new Hashtable();
		PetJson[] pets =  JsonConvert.DeserializeObject<PetJson[]>(json);
		userPets = pets;
		userPetObjects = new PetMove[pets.Length];
		string goaways = "";
		for(var i=0;i<pets.Length;i++) {
			GameObject g = null;
			if (pets[i].pet_condition == (int)PetMove.condition.goaway) {
				if (goaways != "") goaways +="、";
				goaways += pets[i].name;
				continue;
			}
		    try{
		    	g = (GameObject)Instantiate(Resources.Load(pets[i].model));
			    g.transform.parent = GameObject.Find("room_item").transform;
				g.transform.position=new Vector3(Random.Range(-3.5f,3.5f),0,Random.Range(-1f,6f));
				PetMove m = g.GetComponent("PetMove") as PetMove;
				m.petDetail = pets[i];
				userPetObjects[i] = m;
				/*StartCoroutine
                (ItemAppearance_Component.loadTextureFromURL
                 (g.transform.Find("Null/root").gameObject.renderer.material,
                  0,
                  pets[i].texture));
                  //Get texture from url
                  */
				float s=1.0f+((float)pets[i].growth_point)/MaxGrowth/2.0f;
				g.transform.localScale = new Vector3(s,s,s);
				PetGameObjects.Add(pets[i].user_pet_id, g);
				if (pets[i].pet_condition != (int)PetMove.condition.normal) {
					/*GameObject condition = (GameObject)Instantiate(Resources.Load("prefab/effect/billboard"));
					condition.name="billboard";
					if (pets[i].pet_condition == (int)PetMove.condition.badmood) {
						condition.renderer.material.mainTexture = BadMoodTexture;						
					} else if (pets[i].pet_condition == (int)PetMove.condition.sick) {
						condition.renderer.material.mainTexture = SickTexture;
					}
					condition.transform.parent = g.transform;
					BillboardBehavior b = condition.GetComponent("BillboardBehavior") as  BillboardBehavior;
					b.fixedPosition = new Vector3(0.05f,0.5f,-0.3f);
					b.positionFixed = true;*/
					GameObject condition = null;
					switch ((PetMove.condition)pets[i].pet_condition) {
					case PetMove.condition.badmood:
						condition = (GameObject)Instantiate(Resources.Load("prefab/effect/BadMoodParticle"),g.transform.position+new Vector3(0,1.2f,0),g.transform.rotation);
						break;
					case PetMove.condition.sick:
						condition = (GameObject)Instantiate(Resources.Load("prefab/effect/SickParticle"));
						break;
							
					}
					if (condition != null) {
						condition.name = "billboard";
						condition.transform.parent = g.transform;
					}
				}			
				StartCoroutine(getIcon(GlobalDefines.instance.AvatarLifeURL+pets[i].image_url, pets[i].iconTexture));
		    }catch{
		      Debug.LogError("Not found Resouce: "+pets[i].model);
				userPetObjects[i] = null;
		    }
		}
		if (goaways != "") {
			Debug.LogWarning(goaways + "が家出してしまいました…");
			//	MainUI_Component.OnCustomDialog(goaways + "が家出してしまいました…");	
		}
		userPets = pets;
		
		//currentSelectPetData = pets[0];
		//PetDetailDialogVisibility = true;
	}
	public void SetPetItem(string json) {
		//if (Debug.isDebugBuild) {
			Debug.Log(json);	
		//}
		PetItemJson[] petItems =  JsonConvert.DeserializeObject<PetItemJson[]>(json);
		if (Debug.isDebugBuild) {
			Debug.Log(petItems.Length);
			for(var i=0;i<petItems.Length;i++) {
				Debug.Log(petItems[i].name);
			}
		}
		itemLabels = new GUIContent[petItems.Length];
		for(var i=0;i<petItems.Length;i++) {
			GUIContent item = new GUIContent();
			item.text=petItems[i].name;
			itemLabels[i]=item;
			StartCoroutine(getIcon(GlobalDefines.instance.AvatarLifeURL+petItems[i].image_url, item));
		}
		userPetItems = petItems;
		PetItemDialogVisibility = true;
	}
	public void CompleteFeedPet(string json="") {
		Debug.Log("Feed : " + json);
		/*PetJson pet = JsonConvert.DeserializeObject<PetJson>(json);
		if (pet.pet_condition == (int)PetMove.condition.normal) {
			removeBadCondition(pet.user_pet_id);
		}*/
		
		foreach(DictionaryEntry ent in PetGameObjects) {
			GameObject obj = (GameObject)ent.Value;
			PetMove mover = (obj.GetComponent("PetMove") as PetMove);
			PetDetailDialogVisibility = false;
			GameObject touch_effect = (GameObject)Resources.Load ("prefab/effect/FeedParticle");
			touch_effect = (GameObject)Instantiate(touch_effect);
		    Destroy( touch_effect , 5.0f);
			touch_effect.transform.parent = obj.transform;
			touch_effect.transform.localPosition=new Vector3(0,0.1f,0.4f);
			
			GameObject touch_effect2 = (GameObject)Resources.Load ("particle_prefab/hart_kirakira_pirticle");
			touch_effect2 = (GameObject) Instantiate (touch_effect2,obj.transform.position + touch_effect2.transform.position + new Vector3(0,-1,0),touch_effect2.transform.rotation);
		    
			ParticleSystem particle = (ParticleSystem) touch_effect2.GetComponent("ParticleSystem");
			particle.GetComponentsInChildren<ParticleSystem>();
			particle.Stop();
			particle.startDelay = 5.0f;
			particle.emissionRate = 10f;
			ParticleSystem[] children = particle.GetComponentsInChildren<ParticleSystem>();
			particle.Play();
			foreach(ParticleSystem p in children){
				p.Stop();
				p.startDelay = 5f;
				p.Play();
			}
			Destroy(touch_effect2,8.5f);
				
			mover.changeMotionState(PetMove.motion_state.eat);
		}
		/*GameObject obj = PetGameObjects[pet.user_pet_id] as GameObject;
		PetMove mover = (obj.GetComponent("PetMove") as PetMove);
		mover.petDetail = pet;
		PetDetailDialogVisibility = false;
		GameObject touch_effect = (GameObject)Resources.Load ("prefab/effect/FeedParticle");
		touch_effect = (GameObject)Instantiate(touch_effect);
	    Destroy( touch_effect , 5.0f);
		touch_effect.transform.parent = obj.transform;
		touch_effect.transform.localPosition=new Vector3(0,0.1f,0.4f);
		
		GameObject touch_effect2 = (GameObject)Resources.Load ("particle_prefab/hart_kirakira_pirticle");
		touch_effect2 = (GameObject) Instantiate (touch_effect2,obj.transform.position + touch_effect2.transform.position + new Vector3(0,-1,0),touch_effect2.transform.rotation);
	    
		ParticleSystem particle = (ParticleSystem) touch_effect2.GetComponent("ParticleSystem");
		particle.GetComponentsInChildren<ParticleSystem>();
		particle.Stop();
		particle.startDelay = 5.0f;
		particle.emissionRate = 10f;
		ParticleSystem[] children = particle.GetComponentsInChildren<ParticleSystem>();
		particle.Play();
		foreach(ParticleSystem p in children){
			p.Stop();
			p.startDelay = 5f;
			p.Play();
		}
		Destroy(touch_effect2,8.5f);
			
		mover.changeMotionState(PetMove.motion_state.eat);*/
	}
	public void CompleteBrushPet(string json="") {
		Debug.Log("Brush : " + json);
		/*PetJson pet = JsonConvert.DeserializeObject<PetJson>(json);
		if (pet.pet_condition == (int)PetMove.condition.normal) {
			removeBadCondition(pet.user_pet_id);
		}
		GameObject obj = PetGameObjects[pet.user_pet_id] as GameObject;
		PetMove mover = (obj.GetComponent("PetMove") as PetMove);
		mover.petDetail = pet;
		PetDetailDialogVisibility = false;
		GameObject touch_effect = (GameObject)Resources.Load ("particle_prefab/hart_kirakira_pirticle");
	    Destroy(
	      Instantiate (touch_effect,
	        obj.transform.position,
	        touch_effect.transform.rotation),
	      3.0f);*/
		foreach(DictionaryEntry ent in PetGameObjects) {
			GameObject obj = (GameObject)ent.Value;
			PetMove mover = (obj.GetComponent("PetMove") as PetMove);
			PetDetailDialogVisibility = false;
			GameObject touch_effect = (GameObject)Resources.Load ("particle_prefab/hart_kirakira_pirticle");
		    Destroy(
		      Instantiate (touch_effect,
		        obj.transform.position,
		        touch_effect.transform.rotation),
		      3.0f);
			mover.changeMotionState(PetMove.motion_state.brush);
		}
		//effect
	}
	
	public void removeBadCondition(int userPetId) {
		GameObject obj = PetGameObjects[userPetId] as GameObject;
		Transform billboard = obj.transform.FindChild("billboard");
		if (billboard != null) {
			Destroy(billboard.gameObject);
		}
	}
	
	IEnumerator getIcon(string url, GUIContent content) {
		WWW www = new WWW(url);
		Debug.Log (url);
        yield return www;
		content.image = www.texture;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	IEnumerator CallPet(PetMove pm) {
		yield return new WaitForSeconds(Random.Range(0,1.5f));
		/*Vector3 p = Main3D_Component.myAvatar.position;
		p.x+=Random.Range(-0.5f,0.5f);
		p.z+=Random.Range(-0.5f,0.5f);
		
		pm.MoveTargetPosition = p;
		pm.changeMotionState(PetMove.motion_state.walk);
		pm.walkInterval=10f;*/
	}
	void OnGUI(){
		GUI.skin = AvatarGameSkin;
		if (PetItemDialogVisibility) {
			showPetItemDialog();	
		} else if (PetItemConfirmDialogVisibility && currentSelectPetItem != null && currentSelectPetData != null) {
			showPetItemConfirmDialog();	
		} else if (PetDetailDialogVisibility && currentSelectPetData != null) {
			showPetDetailDialog();
		}
		if (userPetObjects != null && userPetObjects.Length > 0) {
			if (GUI.Button(new Rect(Screen.width - 110,5,50,50),"","PetCallBtn")) {
				for(int i=0;i<userPetObjects.Length;i++) {
					if (userPetObjects[i] != null) {
						StartCoroutine(CallPet(userPetObjects[i]));
					}
				}
			}
		}
		
		if (imageDialogVisibility) {
			if (imageDialogContent.image != null) {
				int w = imageDialogContent.image.width;
				int h = imageDialogContent.image.height;
				GUI.Label(new Rect((Screen.width-w)/2,(Screen.height-h)/2,w,h),imageDialogContent);
				if (GUI.Button(new Rect((Screen.width+w)/2-20,(Screen.height-h)/2+5,15,15),"×","CloseButton")) {
					imageDialogVisibility = false;
				}
			}
		} else {
			/*if (GUI.Button(new Rect(50,50,50,50),"load")) {
				showImageDialog("/img/ui/icon_info.png");	
			}*/
		}
	}
	int selectedIndex = -1;
	Vector2 scrollPos;
	void showPetItemDialog() {
		Rect scrollRect =new Rect(0,45,245,245);
		Rect scrollRectLabel =new Rect(5,5,250,275);
		GUI.BeginGroup(scrollRectLabel,"",AvatarGameSkin.GetStyle("PetItemScrollArea"));
		GUIStyle style = AvatarGameSkin.GetStyle("PetItemScroll");
		scrollPos = GUI.BeginScrollView(scrollRect,  scrollPos,new Rect(0,0,230,(userPetItems.Length/3+(userPetItems.Length%3>0?1:0))*80),new GUIStyle(),style);
		//float pos = 0;
		GUILayout.BeginVertical();
		for(int i=0;i<userPetItems.Length/3+(userPetItems.Length%3>0?1:0);i++){
			GUILayout.BeginHorizontal("PetItemRow");
			for (int j=0;j<3;j++){
				int c=i*3+j;
				//Debug.Log(userPetItems.Length+":"+c);
				if (c >= userPetItems.Length) {
					GUILayout.Label("","PetBlankItem");
					continue;
					//break;	
				}
				//GUI.BeginGroup(new Rect(0,pos,200,52),"",AvatarGameSkin.GetStyle("PetItemColumn"));
				GUILayout.BeginVertical();
				//itemLabels[c].text = userPetItems[c].number.ToString();
				if(GUILayout.Button(itemLabels[c].image,AvatarGameSkin.GetStyle("PetItemButton")) && !PetItemConfirmDialogVisibility){///////////
					if (userPetItems[c].number > 0) {
						PetItemConfirmDialogVisibility = true;
						currentSelectPetItem=userPetItems[c];
						currentSelectPetItemContent=itemLabels[c];
						PetItemDialogVisibility = false;
					} else {
						//MainUI_Component.OnCustomDialog("アイテムが不足しています！");
						Debug.LogWarning("アイテムが不足しています！");
					}
				};
				GUILayout.Label(userPetItems[c].number.ToString(),AvatarGameSkin.GetStyle("PetItemCountLabel"));
				GUILayout.EndVertical();
				//GUI.EndGroup();
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
		
		GUI.EndScrollView();
//		GUI.Label(new Rect(5,5,180,20),"もちもの");
		if (GUI.Button(new Rect(225,5,15,15),"×","CloseButton")) {
			PetItemDialogVisibility = false;
			currentSelectPet = null;
			currentSelectPetData = null;
		}
		GUI.EndGroup();
	}
	void showPetItemConfirmDialog() {
		GUI.Label(new Rect(Screen.width/2 - 350/2, Screen.height/2 - 250/2, 350, 250), "", "Confirm_Dialog");
	    GUILayout.BeginArea(new Rect(Screen.width/2-300/2+10, Screen.height/2-230/2+16, 290, 190));
		GUILayout.Label(currentSelectPetData.name+" に",AvatarGameSkin.GetStyle("SentenceLabel"));
		GUILayout.BeginHorizontal();
		GUILayout.Label(currentSelectPetItemContent,AvatarGameSkin.GetStyle("SentenceLabel"));
		//GUILayout.Label("を与えます！",AvatarGameSkin.GetStyle("SentenceLabel"),GUILayout.Height(80));
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if (currentSelectPetItem.hunger_point > 0){
			GUILayout.Label("おなか ： +"+currentSelectPetItem.hunger_point,AvatarGameSkin.GetStyle("StatusLabel"));
		}
		if (currentSelectPetItem.love_point > 0){
			GUILayout.Label(" ",GUILayout.Width(3));
			GUILayout.Label("愛情 ： +"+currentSelectPetItem.love_point,AvatarGameSkin.GetStyle("StatusLabel"));
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
	}
	void showPetDetailDialog() {
		GUILayout.BeginArea(new Rect(5,5,250,170),"","PetDetailArea");
		//GUI.Label(new Rect(5,5,240,160),"","PetDetailSeet");
		if (GUI.Button(new Rect(220,5,20,20),"×","CloseButton")) {
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
		/*int p = currentSelectPetData.growth_point/2;
		for(int i=0;i<p;i++) {
			GUILayout.Label(hungryPointIcon,"PetDetailParameterIcon");
		}*/
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
	}
	
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
	}
	
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
	}
	
	public bool imageDialogVisibility = false;
	public GUIContent imageDialogContent;
	public void showImageDialog(string url) {
		imageDialogVisibility = true;
		imageDialogContent = new GUIContent();
		StartCoroutine(getIcon(url,imageDialogContent));
	}
}

