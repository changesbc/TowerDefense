using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData laserTurret;
    public TurretData missileTurret;
    public TurretData standardTurret;
    public Text moneyText;
    public Animator moneyAnim;
    public GameObject upgradeCanvas;
    public Button upgradeButton;

    public TurretData selectedTurret; //表示当前选择的炮台（要建造的炮台）
    //private GameObject selectedTurretGo;//表示当前选择的炮台（场景中的游戏物体）
    private MapCube selectedMapcube;

    private int money = 1000;// 总资金

    public void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "¥" + money;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开始炮塔的创造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //得到mapcube
                    //GameObject mapcube = hit.collider.gameObject;
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.turretGo == null)   //mapcube上没有炮塔
                    {
                        if (money > selectedTurret.cost)
                        {
                            ChangeMoney(-selectedTurret.cost);

                            if (selectedTurret.turretPrefab == null) return;
                            mapCube.BuildTurret(selectedTurret);
                        }
                        else
                        {
                            //提示资金不够
                            moneyAnim.SetTrigger("flicker");
                        }
                    }
                    else
                    {
                        //TODO 升级炮塔
                        ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        if (mapCube.turretGo == selectedMapcube && upgradeCanvas.activeInHierarchy)
                        {
                            HideUpgradeUI();
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapcube = mapCube;
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = laserTurret;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = missileTurret;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = standardTurret;
        }
    }

    public void ShowUpgradeUI(Vector3 pos,bool isDisableUpgrade=false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    public void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }

    //升级炮塔
    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapcube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapcube.turretData.costUpgraded);
            selectedMapcube.UpgradeTurret();
        }
        else
        {
            moneyAnim.SetTrigger("flicker");  //所剩金额不能满足升级所需
        }
        HideUpgradeUI();
    }

    //拆除炮塔
    public void OnDestroyButtonDown()
    {
        selectedMapcube.DestroyTurret();
        HideUpgradeUI();
    }

}

