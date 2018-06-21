using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponOnPlayer{

	[SerializeField] private int m_id = 0;
	[SerializeField] private float m_fireRate = 0;
	[SerializeField] private int m_damage = 0;
	[SerializeField] private string m_name = null;
	[SerializeField] private int m_speedprojectile = 0;
	[SerializeField] private int m_force = 0;
	[SerializeField] private string m_pathSprWeapon = null;
	[SerializeField] private int m_idSprWeapon = 0;
	[SerializeField] private string m_pathSprShot = null;
	[SerializeField] private int m_idSprShot = 0;
	[SerializeField] private string m_pathSndEx = null;
	[SerializeField] private string m_pathSndSht = null;
	[SerializeField] private string m_type = null;

	public int GetID(){
		return m_id;
	}

	public float GetfireRate(){
		return m_fireRate;
	}

	public int Getdamage(){
		return m_damage;
	}

	public string Getname(){
		return m_name;
	}

	public int Getspeedprojectile(){
		return m_speedprojectile;
	}

	public int Getforce(){
		return m_force;
	}
		
	public string GetpathSprWeapon(){
		return m_pathSprWeapon;
	}

	public int GetidSprWeapon(){
		return m_idSprWeapon;
	}

	public string GetpathSprShot(){
		return m_pathSprShot;
	}

	public int GetidSprShot(){
		return m_idSprShot;
	}

	public string GetpathSndEx(){
		return m_pathSndEx;
	}

	public string GetpathSndSht(){
		return m_pathSndSht;
	}

	public string Gettype(){
		return m_type;
	}
}

[Serializable]
public class WeaponList{
	public List<WeaponOnPlayer> weaponOnPlayer;
}
	