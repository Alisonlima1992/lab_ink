public class Player
{
    private int lvl = 1;
    private BigNumber gold;
    private BigNumber damage;
    private BigNumber upgradeCost;

    public int Lvl => lvl;
    public BigNumber Gold => gold;
    public BigNumber Damage => damage;
    public BigNumber UpgradeCost => upgradeCost;

    public Player()
    {
        gold = new BigNumber("0");
        damage = new BigNumber("5");
        upgradeCost = new BigNumber("50");
    }

    public void AddGold(BigNumber amount)
    {

    }

    public bool TryUpgrade()
    {

        return false;
    }

    public BigNumber DealDamage()
    {
        return damage;
    }
}