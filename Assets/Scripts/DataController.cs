using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

static public class DataController
{
	static private WeaponList weaponList = new WeaponList();

	static public void Read() {
		string file = "Assets/Data/Data_arme.json";
		string dataWeapon;
		using (StreamReader r = new StreamReader (file)) {
			dataWeapon = r.ReadToEnd();
		}
		JsonUtility.FromJsonOverwrite(dataWeapon,weaponList);
	}

	static public WeaponOnPlayer SearchID(int ID){
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