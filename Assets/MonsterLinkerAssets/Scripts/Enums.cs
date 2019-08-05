// HACK: Delete this line

public enum eGameState
{
    Setup,
    Loadout,
    Intro,
    PlayerInput,
    InitiativeCheck,
    QTEAttack,
    QTEBlock,
    NextRound,
    Result,
    Blacklist
};

public enum eEnemyState
{
    Default,
    Normal_FA3Use,
    Normal_NoRP,
    LowHP_FA3Use,
    LowHP_NoRP,
};

public enum eTurn
{
    PlayerFirst,
    PlayerSecond,
    EnemyFirst,
    EnemySecond,
    BothDone,
}

public enum eQTEType
{
    Attack,
    Block,
    FAEndurance,
    FA
}

public enum eQTEState
{
    Waiting,
    Running,
    Endurance,
    Done
}

public enum eQTEZone
{
    None,
    Fail,
    Good,
    Perfect
}

public enum eHomeUI
{
    Title,
    SaveProfile,
    Home,
    Settings,
    Loadout,
    Blacklist,
};

public enum eArenaUI
{
    ArenaIntro,
    PlayerInput,
    PlayerInputConfirm,
    InitiativeCheck,
    PlayerTurn,
    EnemyTurn,
    Result
};

public enum eAttackType
{
    FA,
    BA
}

public enum eImplant
{
    None,
    RisingRage,
    UnleashedMode,
    SuperFA,
    TempInputSlot
}

public enum eUnleashedMode
{
    sleeping,
    available,
    active,
    done
}

public enum eSuperFeralArt
{
    sleeping,
    available,
    used,
    done
}

public enum eFightResult
{
    None,
    Victory,
    Defeat
}

public enum eQTEInput
{
    None,
    QTE,
    Endurance
}

public enum eLoadout
{
    LoadoutOnly,
    FeralArtChoice,
    ImplantChoice
};

public enum eTutorial
{
    notstarted,
    menu,
    loadout,
    input1,
    inicheck,
    infight,
    input2,
    done
}

public enum eQTEFacialExpression
{
    annoyed,
    concerned,
    neutral,
    excited
}

public enum eQTEBodyLanguage
{
    armscrossed,
    armsdown,
    pointLeft,
    pointRight
}

public enum eToriiColor
{
    violett,
    pink,
    yellow,
    green,
    orange
}

public enum eEnemySkin
{
    Slicer,
    Crystalfang,
    Pyro,
    Xensor,
    Eldritch
}

public enum eCamPosition
{
    loadout,
    intro,
    input,
    attack,
    block,
    result
}