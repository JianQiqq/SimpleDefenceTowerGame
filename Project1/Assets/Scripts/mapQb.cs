using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mapQb : MonoBehaviour {
    [HideInInspector]
    public GameObject turrentGo;
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgraded = false;
    public GameObject buildeffect;
    public GameObject Upgradeffect;
    private Renderer myrenderer;


    void Start()
    {
        myrenderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        if (turretData != null)
        {
            this.turretData = turretData;
            isUpgraded = false;
            turrentGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
            GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.4f);
        }
        else
        {
            Debug.Log("no selected turret");
        }
    }

    public void UpgradeTurret()
    {
        if (isUpgraded == true) { return; }
        else
        {
            Destroy(turrentGo);
            isUpgraded = true;
            turrentGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
            GameObject effect = GameObject.Instantiate(Upgradeffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.4f);
        }
    }
    public void DestroyTurret()
    {
        Destroy(turrentGo);
        isUpgraded = false;
        turrentGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(Upgradeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.4f);
        int refund = BuildManager.buildManager.money += BuildManager.buildManager.selectedTurretData.cost;
        BuildManager.buildManager.moneyText.text = "$" + refund;
    }

    internal void BuildTurret(GameObject turretPrefab)
    {
        throw new NotImplementedException();
    }

    void OnMouseEnter()
    {
        if (turrentGo == null && EventSystem.current.IsPointerOverGameObject()==false)
        {
            myrenderer.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        myrenderer.material.color = Color.white;
    }

    
}
