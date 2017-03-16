using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo; //保存当前cube身上的炮台
    public GameObject buildEffectGo;
    private Renderer renderer;
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgraded = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effectGo = GameObject.Instantiate(buildEffectGo, transform.position, Quaternion.identity);
        Destroy(effectGo, 1);
    }

    void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    //升级炮塔
    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, transform.rotation);
    }

    //拆除炮塔
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretData = null;
        turretGo = null;
    }
}
