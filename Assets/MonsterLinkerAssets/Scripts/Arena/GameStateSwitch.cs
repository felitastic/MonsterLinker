using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSwitch : MonoBehaviour
{
    public static GameStateSwitch Instance;

    public eGameState GameState;
    public eFightResult FightResult;
    public eImplant Implant;

    public float IntroTime = 1f;
    //TODO: List of all scripts in the arena scene
    public FeralArtCheck feralartcheck;
    public ArenaPlayerInput playerinput;
    public InputBarHandler inputbarhandler;
    public ArenaUIHandler arenaui;
    public AttackSlotSpawn attackslotspawn;
    public InitiativeCheck initiativecheck;
    public EnemyStateMachine enemystatemachine;
    public TurnChanger turnchanger;
    public QTEHandler qtehandler;
    public BAEffectsHandler baeffectshandler;
    public AttackRoundHandler attackroundhandler;
    public AnimationHandler animationhandler;
    public QTEAnimEvents qteanimevents;
    public LoadoutButtons loadoutbuttons;
    public FAInfoWindow fainfowindow;
    public StatusBarHandler enemystatusbar;
    public StatusBarHandler playerstatusbar;
    public CreatureAnimEvents playerCreatureanimevents;
    public CreatureAnimEvents enemyCreatureanimevents;
    public CamShake camshake;
    public ImplantHandler implanthandler;

    public Save curProfile; 
    public Enemy curEnemy;
    public bool firstSetupDone;

    void Start()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        GetAllScripts();
        ConnectScripts();
        SwitchState(eGameState.Loadout);
    }

    void GetAllScripts()
    {
        //TODO getcomponentinchildren, get each script at the start of scene
        loadoutbuttons = GetComponentInChildren<LoadoutButtons>();
        feralartcheck = GetComponentInChildren<FeralArtCheck>();
        playerinput = GetComponentInChildren<ArenaPlayerInput>();
        inputbarhandler = GetComponentInChildren<InputBarHandler>();
        arenaui = GetComponentInChildren<ArenaUIHandler>();
        attackslotspawn = GetComponentInChildren<AttackSlotSpawn>();
        initiativecheck = GetComponentInChildren<InitiativeCheck>();
        enemystatemachine = GetComponentInChildren<EnemyStateMachine>();
        turnchanger = GetComponentInChildren<TurnChanger>();
        qtehandler = GetComponentInChildren<QTEHandler>();
        baeffectshandler = GetComponentInChildren<BAEffectsHandler>();
        attackroundhandler = GetComponentInChildren<AttackRoundHandler>();
        animationhandler = GetComponentInChildren<AnimationHandler>();    
        fainfowindow = GetComponentInChildren<FAInfoWindow>();
        implanthandler = GetComponentInChildren<ImplantHandler>();

        qteanimevents = FindObjectOfType<QTEAnimEvents>();        
        enemystatusbar = FindObjectOfType<EnemyStatusBar>();
        playerstatusbar = FindObjectOfType<PlayerStatusBar>();
        camshake = FindObjectOfType<CamShake>();
    }

    void ConnectScripts()
    {
        //Connect scripts that need access to each other
        inputbarhandler.feralartcheck = feralartcheck;
        inputbarhandler.initiativecheck = initiativecheck;
        inputbarhandler.arenaui = arenaui;
        playerinput.inputbarhandler = inputbarhandler;
        playerinput.arenaui = arenaui;
        playerinput.feralartcheck = feralartcheck;
        enemystatemachine.arenaui = arenaui;
        enemystatemachine.initiativecheck = initiativecheck;
        initiativecheck.arenaui = arenaui;
        initiativecheck.turnchanger = turnchanger;
        qtehandler.baeffectshandler = baeffectshandler;
        qtehandler.turnchanger = turnchanger;
        qtehandler.attackroundhandler = attackroundhandler;
        arenaui.inputbarhandler = inputbarhandler;
        feralartcheck.inputbarhandler = inputbarhandler;
        feralartcheck.arenaui = arenaui;
        feralartcheck.baeffectshandler = baeffectshandler;
        attackroundhandler.baeffectshandler = baeffectshandler;
        attackroundhandler.qtehandler = qtehandler;
        attackroundhandler.animationhandler = animationhandler;
        attackroundhandler.turnchanger = turnchanger;
        enemystatemachine.baeffectshandler = baeffectshandler;
        playerCreatureanimevents.animationhandler = animationhandler;
        playerCreatureanimevents.attackroundhandler = attackroundhandler;
        playerCreatureanimevents.qtehandler = qtehandler;
        enemyCreatureanimevents.animationhandler = animationhandler;
        enemyCreatureanimevents.attackroundhandler = attackroundhandler;
        enemyCreatureanimevents.qtehandler = qtehandler;
        qteanimevents.qtehandler = qtehandler;
        baeffectshandler.arenaui = arenaui;
        baeffectshandler.enemystatusbar = enemystatusbar;
        baeffectshandler.playerstatusbar = playerstatusbar;
        baeffectshandler.implanthandler = implanthandler;
    }
    
    //will be called by other scripts, update the arenastate and then run functions from the scripts
    public void SwitchState(eGameState gamestate)
    {
        GameState = gamestate;
        print("arena state: " + GameState);

        switch (gamestate)
        {
            ///Blacklist und FA Loadout für Spieler
            ///Enemy Values laden und Attack Slot Setup für Enemy und Spieler
            case eGameState.Loadout:
                if (!firstSetupDone)
                    loadoutbuttons.WriteFAList();
                else
                {
                    animationhandler.ResetToIdle();
                }

                loadoutbuttons.WindowSwitch(eLoadout.LoadoutOnly);
                loadoutbuttons.SetInitialTexts();
                arenaui.StatusBars.SetActive(false);
                arenaui.ResultPanel.SetActive(false);
                arenaui.FALoadout.SetActive(true);
                arenaui.QTEPanel.SetActive(false);
                enemystatemachine.GetEnemyValues();
                //enemystatemachine.SetEnemyType(curEnemy);
                break;
            ///Arena in cinematischer Cutscene vorstellen
            ///FA Loadout und alle scripts laden
            case eGameState.Intro:
                arenaui.StatusBars.SetActive(false);
                arenaui.FALoadout.SetActive(false);
                arenaui.ResultPanel.SetActive(false);
                arenaui.FALoadout.SetActive(false);

                if (!firstSetupDone)
                    FirstSetup();

                StartCoroutine(WaitForIntro(IntroTime));
                break;
            ///Player Input enablen
            ///Enemy Input laden
            ///FA Check
            case eGameState.PlayerInput:
                attackroundhandler.NoExtraSlot = false;
                //implanthandler.PlayerRPatAttackStart = baeffectshandler.curPlayerRP;

                arenaui.StatusBars.SetActive(true);
                arenaui.ResultPanel.SetActive(false);
                arenaui.EnemyInputBar.SetActive(false);
                arenaui.InputPanel.SetActive(true);
                arenaui.SetConfirmButtonStatus(false);
                arenaui.SetInputButtonsStatus(true);
                arenaui.PlayerInputBar.SetActive(true);

                enemystatemachine.CheckEnemyState();
                enemystatemachine.SetInput();

                //-> Input Slot voll: nach FAs checken
                //-> Input Slot voll: Confirm enablen
                break;
            ///Speedwerte vergleichen um Ini festzulegen
            case eGameState.InitiativeCheck:
                //baeffectshandler.GetAttackLists(feralartcheck.AttackList, enemystatemachine.curAttackInput);
                //baeffectshandler.GetAttackLists(inputbarhandler.PlayerAttackInput, enemystatemachine.curAttackInput);
                arenaui.InputPanel.SetActive(false);
                arenaui.PlayerInputBar.SetActive(true);
                arenaui.EnemyInputBar.SetActive(true);
                arenaui.InitiativeCheck.SetActive(true);
                initiativecheck.GetSpeedValues();
                StartCoroutine(initiativecheck.CompareSpeed());
                arenaui.QTEPanel.SetActive(true);

                //Enemy Input einblenden
                //Vergleichen der Speedwerte, Turn anzeigen
                //Int Turn += 1; Bei Turn 2 zu NextRound wechseln
                break;
            case eGameState.QTEAttack:
                animationhandler.PlayerAnim.SetBool("block", false);
                animationhandler.EnemyAnim.SetBool("block", true);
                arenaui.InitiativeCheck.SetActive(false);
                arenaui.EnemyInputBar.SetActive(false);
                arenaui.PlayerInputBar.SetActive(true);                                
                //baeffectshandler.SetEnduranceModifier(0);
                attackroundhandler.GetAttackList(feralartcheck.AttackList);



                //qtehandler.SetType(eQTEType.Attack, attackslotspawn.NumberOfAttackSlotsPlayer);
                //qtehandler.QTEStateSwitch(eQTEState.Waiting);
                //Animation der Attacke des Spielers sowie Reaktion des Gegners triggern
                //QTE zu den Attacken
                //DMG bei Hit austeilen
                //HP Gain während der Attacke
                //=> Loop bis alle Attacken durch sind
                //Am Ende des Turns: RP Gain Summe, Total DMG Dealt Count -> Check for Death
                break;
            case eGameState.QTEBlock:
                animationhandler.EnemyAnim.SetBool("block", false);
                animationhandler.PlayerAnim.SetBool("block", true);
                arenaui.InitiativeCheck.SetActive(false);
                arenaui.PlayerInputBar.SetActive(false);
                arenaui.EnemyInputBar.SetActive(true);
                //baeffectshandler.SetEnduranceModifier(0);
                attackroundhandler.GetAttackList(enemystatemachine.curAttackInput);


                //attackroundhandler.SetEffectValues();
                //attackroundhandler.StartAttack();
                //Animation der Attacke des Gegners sowie Reaktion des Spielers triggern
                //QTE zum Blocken & für RP Gain
                //DMG bei Hit austeilen
                //=> Loop bis alle Attacken durch sind
                //Am Ende des Turns: RP Gain Summe, Total DMG Taken Count -> Check for Death
                break;
            case eGameState.NextRound:
                animationhandler.ResetToIdle();
                animationhandler.JumpBack();

                arenaui.QTEPanel.SetActive(false);
                //Disable both Initiative Arrows
                arenaui.StatusBars.SetActive(false);
                //arenaui.PlayerInitiativeArrow.enabled = false;
                //arenaui.EnemyInitiativeArrow.enabled = false;
                initiativecheck.ResetSpeedValues();
                //Reset Player Input und Enemy Input
                inputbarhandler.Reset();
                feralartcheck.ResetLists();
                enemystatemachine.ClearInput();
                enemystatemachine.curAttackInput.Clear();
                //Reset DMG counters for the end of each turn
                baeffectshandler.ResetDmgCount();

        //HACK: zum Test von Temp Input Slot
        //attackroundhandler.NoExtraSlot = false;                       

                //Go to Player Input State
                SwitchState(eGameState.PlayerInput);
                break;
            case eGameState.Result:
                arenaui.StatusBars.SetActive(false);
                arenaui.EnemyInputBar.SetActive(false);
                arenaui.PlayerInputBar.SetActive(false);

                SoundController.Instance.AudioSourceBGM_Fight.mute = true;
                SoundController.Instance.AudioSourceBGM_Menu.mute = false;

                ResetFight();

                //TODO result buttons updaten nach gate 2
                switch (FightResult)
                {
                    case eFightResult.None:
                        Debug.LogError("No Result set!");
                        break;
                    case eFightResult.Victory:
                        arenaui.RetryButton.SetActive(true);
                        arenaui.LoadoutButton.SetActive(true);
                        //arenaui.NextButton.SetActive(true);
                        //arenaui.LoadoutButton.SetActive(false);
                        arenaui.ResultText.text = "WINNER";

                        break;
                    case eFightResult.Defeat:
                        arenaui.RetryButton.SetActive(true);
                        arenaui.LoadoutButton.SetActive(true);
                        //arenaui.NextButton.SetActive(false);
                        //arenaui.LoadoutButton.SetActive(true);

                        arenaui.ResultText.text = "LOSER";
                        break;
                }
                arenaui.ResultPanel.SetActive(true);
                break;
        }
    }

    public void FirstSetup()
    {
        print("first setup of values n shit");
        baeffectshandler.StartHpandRPValues(curProfile.MaxHitPoints, curProfile.curRP, curEnemy.MaxHitPoints, 0, curEnemy.RPgainperHit, curEnemy.DmgModifier);
        inputbarhandler.maxBaseAttackInputSlots = curProfile.maxBaseAttackInputSlots;

        attackslotspawn.Setup(GameStateSwitch.Instance.curProfile.maxBaseAttackInputSlots, GameStateSwitch.Instance.enemystatemachine.maxInputSlots);
        attackslotspawn.SpawnPlayerSlots();
        attackslotspawn.SpawnEnemySlots();
        arenaui.GetAttackSlots();

        baeffectshandler.UpdateHPandRPCounter();
        //arenaui.SetEnemyHPandRP(Mathf.RoundToInt(baeffectshandler.curEnemyHP), Mathf.RoundToInt(baeffectshandler.curEnemyRP));
        //arenaui.SetPlayerHPandRP(Mathf.RoundToInt(baeffectshandler.curPlayerHP), Mathf.RoundToInt(baeffectshandler.curPlayerRP));
        feralartcheck.LoadedFeralArts = curProfile.FALoadout;
        curProfile.SetCheapestFAcost();
        fainfowindow.WriteFAData();

        playerstatusbar.GetValues(curProfile.MaxHitPoints, 100.0f, -547.0f, -234.0f, 0.0f, 0.0f);
        enemystatusbar.GetValues(curEnemy.MaxHitPoints, 100.0f, 547.0f, 234.0f, 0.0f, 0.0f);

        playerstatusbar.HPTick(curProfile.MaxHitPoints);
        playerstatusbar.RPTick(0);
        enemystatusbar.HPTick(curEnemy.MaxHitPoints);
        enemystatusbar.RPTick(0);

        arenaui.PlayerName.text = curProfile.MonsterName;
        arenaui.EnemyName.text = curEnemy.MonsterName;

        if (Implant == eImplant.SuperFA)
            feralartcheck.superFAused = false;

        //TODO uncheck unleashed mode bool just in case
        //if (Implant == eImplant.UnleashedMode)
        //    feralartcheck.UnleashedModeused = false;
               
        firstSetupDone = true;
    }

    public void ResetFight()
    {
        inputbarhandler.maxBaseAttackInputSlots = curProfile.maxBaseAttackInputSlots;

        print("resetting fight values n shit");
        baeffectshandler.curEnemyHP = curEnemy.MaxHitPoints;
        baeffectshandler.curPlayerHP = curProfile.MaxHitPoints;
        baeffectshandler.curEnemyRP = 0;
        baeffectshandler.curPlayerRP = 0;

        baeffectshandler.StartHpandRPValues(curProfile.MaxHitPoints, curProfile.curRP, curEnemy.MaxHitPoints, 0, curEnemy.RPgainperHit, curEnemy.DmgModifier);

        playerstatusbar.GetValues(curProfile.MaxHitPoints, 100.0f, -685.0f, -290.0f, 0.0f, 0.0f);
        enemystatusbar.GetValues(curEnemy.MaxHitPoints, 100.0f, 685.0f, 235.0f, 0.0f, 0.0f);
        playerstatusbar.HPTick(curProfile.MaxHitPoints);
        playerstatusbar.RPTick(0);
        enemystatusbar.HPTick(curEnemy.MaxHitPoints);
        enemystatusbar.RPTick(0);

        baeffectshandler.UpdateHPandRPCounter();

        //arenaui.SetPlayerHPandRP(Mathf.RoundToInt(baeffectshandler.curPlayerHP), Mathf.RoundToInt(baeffectshandler.curPlayerRP));
        //arenaui.SetEnemyHPandRP(Mathf.RoundToInt(baeffectshandler.curEnemyHP), Mathf.RoundToInt(baeffectshandler.curEnemyRP));

        //reset player and enemy inputs
        inputbarhandler.Reset();
        enemystatemachine.ClearInput();

        if (Implant == eImplant.SuperFA)
            feralartcheck.superFAused = false;

        //TODO uncheck unleashed mode bool just in case
        //if (Implant == eImplant.UnleashedMode)
        //    feralartcheck.UnleashedModeused = false;
    }

    IEnumerator WaitForIntro(float waitingTime)
    {
        print("showing arena intro");
        yield return new WaitForSeconds(waitingTime);
        SwitchState(eGameState.PlayerInput);
    }
}