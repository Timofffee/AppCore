namespace App.ArmyClash.StateMachines
{
    public enum AppStates
    {
        SceneLoaded,
        Initialization,
        ModelsLoaded,
        Lobby,
        PrepareToBattle,
        Battle,
        BattleEnded,
        Win,
        Lose,
        SceneQuit
    }
}
