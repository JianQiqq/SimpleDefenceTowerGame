using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    [HideInInspector]
    public TurretData selectedTurretData;
    public static BuildManager buildManager;
    public Text moneyText;
    public Animator moneyAnimator;
    
    public int money = 1500;
    public GameObject upgradecanvas;
    public Button buttonUpgrad;
    private mapQb selectedmapQb;
    private Animator upgradeCanvasAnimator;

    void Awake()
    {
        buildManager = this;
    }

    void Start()
    {
        upgradeCanvasAnimator = upgradecanvas.GetComponent<Animator>();
    }

    void UpdateMoney(int change)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("mapQ"));
                if (isCollider)
                {
                    mapQb mapQ = hit.collider.GetComponent<mapQb>();
                    if (selectedTurretData != null && mapQ.turrentGo == null)
                    {
                        if (money >= selectedTurretData.cost)
                        {
                            UpdateMoney(-selectedTurretData.cost);
                            mapQ.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //no enough money
                            moneyAnimator.SetTrigger("flick");
                        }
                    }
                    else if (mapQ.turrentGo != null)
                    {
                        
                        //upgrade
                        
                        /*if (mapQ.isUpgraded)
                        {
                            ShowUpgrade(mapQ.transform.position, true);
                        }
                        else
                        {
                            ShowUpgrade(mapQ.transform.position, false);
                        }*/
                        if (mapQ == selectedmapQb && upgradecanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgrade(mapQ.transform.position, mapQ.isUpgraded);
                        }
                        selectedmapQb = mapQ;
                    }
                }
            }
        }
    }
        
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnTurretSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    void ShowUpgrade(Vector3 pos, bool isDisableUpgrade)
    {
        StopCoroutine("HideUpgradeUI");
        upgradecanvas.SetActive(true);
        upgradecanvas.transform.position = pos;
        buttonUpgrad.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("hide");
        yield return new WaitForSeconds(0.8f);
        upgradecanvas.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        if (money >= selectedmapQb.turretData.costUpgraded)
        {
            UpdateMoney(-selectedmapQb.turretData.costUpgraded);
            selectedmapQb.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("flick");
        }
        StartCoroutine(HideUpgradeUI());
        
    }
    public void OnDestroyButtonDown()
    {
        selectedmapQb.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }

}
