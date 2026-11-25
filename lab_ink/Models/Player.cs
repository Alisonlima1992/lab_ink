public class Player
{
    private int lvl;
    private BigNumber gold;
    private BigNumber damage;
    private double damageModifier;
    private BigNumber upgradeCost;
    private double upgradeModifier;

    public int Lvl => lvl;
    public BigNumber Gold => gold;
    public BigNumber Damage => damage;
    public double DamageModifier => damageModifier;
    public BigNumber UpgradeCost => upgradeCost;
    public double UpgradeModifier => upgradeModifier;

    public Player()
    {
        lvl = 1;
        gold = new BigNumber("0");
        damage = new BigNumber("5");
        damageModifier = 1.15;
        upgradeCost = new BigNumber("50");
        upgradeModifier = 1.2;
    }

    public void AddGold(BigNumber amount)
    {
        gold = gold + amount;
    }

    public bool TryUpgrade()
    {
        if (gold < upgradeCost)
            return false;

        gold = gold - upgradeCost;
        lvl++;
        damage = damage * damageModifier;
        upgradeCost = upgradeCost * (upgradeModifier * lvl);

        return true;
    }

    public BigNumber DealDamage()
    {
        return damage;
    }
}