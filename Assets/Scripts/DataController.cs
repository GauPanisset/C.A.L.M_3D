using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataController
{
	private string file = "Assets/Data/Data_arme.json";
	private string dataWeapon;
	private WeaponList weaponList = new WeaponList();

	public DataController() {
		using (StreamReader r = new StreamReader (file)) {
			dataWeapon = r.ReadToEnd();
		}
		JsonUtility.FromJsonOverwrite(dataWeapon,weaponList);
	}

	public WeaponOnPlayer SearchID(int ID){
		WeaponOnPlayer newWeapon = new WeaponOnPlayer();
		bool found = false;

		foreach (WeaponOnPlayer element in weaponList.weaponOnPlayer) {
			if (element.GetID () == ID) {
				newWeapon = element;
				found = true;
			}
		}

		if (found) {
			return newWeapon;
		} else {
			return null;
		}
	}

}