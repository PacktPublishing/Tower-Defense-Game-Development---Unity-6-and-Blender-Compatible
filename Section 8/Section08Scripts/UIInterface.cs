using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInterface : MonoBehaviour {

    public GameObject rocketTurret;
    public GameObject gatlingTurret;
    public GameObject flamerTurret;

    public GameObject turretMenu;

    GameObject itemPrefab;
    GameObject focusObj;

    void Start() {

    }

    public void CreateRocket() {

        itemPrefab = rocketTurret;
        CreateItemForButton();
    }

    public void CreateGatling() {

        itemPrefab = gatlingTurret;
        CreateItemForButton();
    }

    public void CreateFlamer() {

        itemPrefab = flamerTurret;
        CreateItemForButton();
    }

    public void CloseTurretMenu() {

        turretMenu.SetActive(false);
    }

    void CreateItemForButton() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit)) return;

        focusObj = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
        focusObj.GetComponent<Collider>().enabled = false;
    }

    void Update() {


        if (Input.GetMouseButtonDown(0)) {

            if (EventSystem.current.IsPointerOverGameObject()) return;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)
                && hit.collider.gameObject.CompareTag("turret")) {

                turretMenu.transform.position = Input.mousePosition;
                turretMenu.SetActive(true);
            }
        } else if (focusObj && Input.GetMouseButton(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit)) return;

            focusObj.transform.position = hit.point;
        } else if (focusObj && Input.GetMouseButtonUp(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)
                && hit.collider.gameObject.CompareTag("platform")
                && hit.normal.Equals(new Vector3(0.0f, 1.0f, 0.0f))) {

                hit.collider.gameObject.tag = "occupied";
                focusObj.transform.position = new Vector3(
                    hit.collider.gameObject.transform.position.x,
                    focusObj.transform.position.y,
                    hit.collider.gameObject.transform.position.z);
                focusObj.GetComponent<BoxCollider>().enabled = true;
                focusObj.GetComponent<SphereCollider>().enabled = true;
            } else {

                Destroy(focusObj);
            }

            focusObj = null;
        }
    }
}
