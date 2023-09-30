using System.Collections.Generic;

public class Combat
{
    public static Combat ActiveCombat;
    public Player Player { get; private set; }
    public List<AbstractEnemy> Enemies { get; private set; }
    public int TurnCount { get; private set; }

    public Queue<object> visualEvents;

    public void PlayCard(AbstractCard card)
    {
        //do target selection stuff

        //actually play 
        card.OnPlayed(new());
        card.Cooldown.Maximize();
    }

    public void DoEnemyTurn()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.Cooldown == 0)
            {
                enemy.Act();
                break;
            }
            enemy.Cooldown--;
        }
    }
}
