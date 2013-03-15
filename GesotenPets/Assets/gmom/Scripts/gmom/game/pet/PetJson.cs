using System;
using UnityEngine;
public class PetJson
	{
	public int user_id { get; set; }
    public int user_pet_id { get; set; }
    public int hunger_point { get; set; }
    public int feeling_point { get; set; }
    public int growth_point { get; set; }
    public int pet_condition { get; set; }
    public String effect { get; set; }
	public int pet_id { get; set; }
    public String name { get; set; }
    public String description { get; set; }
    public int love_point { get; set; }
    public String model { get; set; }
    public String texture { get; set; }
    public int price { get; set; }
    public int flag { get; set; }
    public int rarity { get; set; }
    public String image_url { get; set; }
	public GUIContent iconTexture{ get; set; }
	public PetJson ()
	{
		iconTexture = new GUIContent();	
	}
}

