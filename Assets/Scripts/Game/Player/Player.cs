namespace Temp.Game.Player
{
    public class Player
    {
        public int Id;
        public IPlayerInput Input;

        public int SkillPoint;

        public void AddSkillPoint(int value)
        {
            SkillPoint += value;
        }
    }
}