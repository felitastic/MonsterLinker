using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStageChanger : MonoBehaviour
{
    public EnemyStateMachine veryEasyEnemyType;
    public EnemyStateMachine easyEnemyType;
    public EnemyStateMachine normalEnemyType;

    public List<Enemy> Enemy;

    public void CheckArenaStage(int arena)
    {
        switch (arena)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }
    }
}
