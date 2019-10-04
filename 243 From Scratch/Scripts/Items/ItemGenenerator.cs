using UnityEngine;

public static class ItemGenerator {
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;
	
	public static Item CreateItem() {
		//decide what type of item to make
		int rand = Random.Range( 0, 2 );
		
		Item item = new Item();

		//call the method to create that base item type
		switch( rand ) {
		case 0:
			item = CreateWeapon();
			break;
		case 1:
			item = CreateArmor();
			break;
		}
		

//		private string _name;

		item.Value = Random.Range(1, 101);
		
		item.Rarity = RarityTypes.Common;
		
		item.MaxDurability = Random.Range(50, 61);
		item.CurDurability = item.MaxDurability;

		//return the new Item
		return item;
	}
	
	private static Weapon CreateWeapon() {
		//decide if we make a melee or ranged weapon
		Weapon weapon = CreateMeleeWeapon();

		//return the weapon created
		return weapon;
	}
	
	private static Weapon CreateMeleeWeapon() {
		Weapon meleeWeapon = new Weapon();
		
		string[] weaponNames = new string[] {
											"Sword",
											"Morningstar",
											"Silifi",
											"Scimitar",
											"Hachet",
											"Axe",
											"Hammer",
											"Fork",
											"Pick",
											"Weak Sword",
											"Bastard Sword",
											"Torch"
											};

		
		//fill in all of the values for that item type
		meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];

		//assign the max damage of the weapon
		meleeWeapon.MaxDamage = Random.Range(5, 11);
		meleeWeapon.DamageVariance = Random.Range(.2f, .76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;
		
		//assign the max range of this weapon
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;
		
		//assign the icon for the weapon
		meleeWeapon.Icon = Resources.Load(GameSetting2.MELEE_WEAPON_ICON_PATH + meleeWeapon.Name) as Texture2D;
		
		//return the melee weapon
		return meleeWeapon;
	}
	
	private static Armor CreateArmor() {
		//decide what type of armor to make
		int temp = Random.Range( 0, 2 );
		
		Armor armor = new Armor();
		
		switch( temp ) {
		case 0:
			armor = CreateShield();
			break;
		case 1:
			armor = CreateHat();
			break;
		}

		//return the armor created
		return armor;
	}
	
	private static Armor CreateShield() {
		Armor armor = new Armor();
		
		string[] shieldNames = new string[] {
											"Small Shield",
											"Large Shield"
											};
		
		//fill in all of the values for that item type
		armor.Name = shieldNames[Random.Range(0, shieldNames.Length)];

		//assign properties for the shield
		armor.ArmorLevel = Random.Range(10, 50);
		
		//assign the icon for the weapon
		armor.Icon = Resources.Load(GameSetting2.SHIELD_ICON_PATH + armor.Name) as Texture2D;
		
		//assign the eqipment slot where this can be assigned
		armor.Slot = EquipmentSlot.OffHand;
		
		//return the melee weapon
		return armor;
	}
	
	private static Armor CreateHat() {
		Armor armor = new Armor();
		
		string[] hatNames = new string[] {
			"Bandana",
			"Squire Cap",
			"Heaume",
			"Pirate Hat",
			"Robin Hood",
			"Sailor"
											};
		
		//fill in all of the values for that item type
		armor.Name = hatNames[Random.Range(0, hatNames.Length)];

		//assign properties for the item
		armor.ArmorLevel = Random.Range(10, 50);
		
		//assign the icon for the weapon
		armor.Icon = Resources.Load(GameSetting2.HAT_ICON_PATH + armor.Name) as Texture2D;
		
		//assign the eqipment slot where this can be assigned
		armor.Slot = EquipmentSlot.Head;

		//return the melee weapon
		return armor;
	}

	
}

public enum ItemType {
	Armor,
	Weapon,
	Potion,
	Scroll
}