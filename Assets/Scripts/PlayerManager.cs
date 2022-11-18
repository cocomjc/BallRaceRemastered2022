using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    private RoadsManager roadsManager;
    private GameManager gameManager;
    [SerializeField] private CinemachineVirtualCamera followingCam;
    [SerializeField] private CinemachineVirtualCamera endCam;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI diamondsText;
    [SerializeField] private int lifes;
    private int runDiamonds;
    private float endBonus = 1;

    private void Start()
    {
        roadsManager = RoadsManager.GetInstance();
        gameManager = GameManager.GetInstance();
        GetComponent<PlayerMovements>().SetPaused(true);
        UpdateDiamondsCount();
        UpdateLifeCount();
        CameraSwitcher.SwitchCamera(followingCam);
    }

    private void OnEnable()
    {
        CameraSwitcher.Register(followingCam);
        CameraSwitcher.Register(followingCam);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(followingCam);
        CameraSwitcher.Unregister(endCam);
    }

    public void StartGame()
    {
        GetComponent<PlayerMovements>().SetPaused(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadSpawn"))
        {
            roadsManager.MoveRoad();
        }
        if (other.CompareTag("Gatherable"))
        {
            Gatherable gatherable = other.GetComponent<Gatherable>();
            switch (gatherable.Type)
            {
                case GatherableType.Diamond:
                    runDiamonds++;
                    UpdateDiamondsCount();
                    break;
                case GatherableType.Life:
                    lifes++;
                    UpdateLifeCount();
                    break;
            }
            other.GetComponent<Gatherable>().TriggerGathered();
        }
        if (other.CompareTag("FinishLine") && gameManager.GetGameState() == GameState.Game)
        {
            gameManager.SetGameState(GameState.End);
            CameraSwitcher.SwitchCamera(endCam);
            GetComponent<PlayerMovements>().TriggerEnd();
        }
        if (other.gameObject.CompareTag("EndBonus"))
        {
            endBonus = other.gameObject.GetComponent<EndBonus>().GetBonus();
            Debug.Log("Bonus set to " + endBonus);
            if (lifes < 1)
            {
                StartCoroutine(GoToEndMenu(1f));
                GetComponent<PlayerMovements>().SetPaused(true);
            }
        }
    }

    private void UpdateDiamondsCount()
    {
        diamondsText.text = (PlayerPrefs.GetInt("Diamonds") + runDiamonds).ToString();
    }

    private void UpdateLifeCount()
    {
        lifesText.text = "Lifes: " + lifes;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            if (lifes >= 1)
            {
                Debug.Log("Hit Obstacle to destroy removing a life");
                // Get the parent of the obstacle
                Destroy(hit.gameObject);
                //Transform obstacleParent = hit.gameObject.transform.parent;
                lifes--;
                UpdateLifeCount();
                GetComponent<PlayerMovements>().SlowPlayer();
            }
            else if (gameManager.GetGameState() == GameState.Game)
            {
                gameManager.EndGame(runDiamonds, endBonus);
            }
        }
        if (hit.gameObject.CompareTag("Ramp"))
        {
            GetComponent<PlayerMovements>().Boost();
        }
    }

    private IEnumerator GoToEndMenu (float _delay)
    {
        yield return new WaitForSeconds(_delay);
        gameManager.EndGame(runDiamonds, endBonus);
    }
}
