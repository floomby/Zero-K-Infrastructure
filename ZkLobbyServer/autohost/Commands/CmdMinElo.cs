using System.Threading.Tasks;
using LobbyClient;
using ZkData;

namespace ZkLobbyServer
{
    public class CmdMinElo : BattleCommand
    {
        private int elo;
        public override string Help => "elo - changes elo limit for players, e.g. !minelo 1600";
        public override string Shortcut => "minelo";
        public override AccessType Access => AccessType.Admin;

        public override BattleCommand Create() => new CmdMinElo();

        public override string Arm(ServerBattle battle, Say e, string arguments = null)
        {
            if (int.TryParse(arguments, out elo))
            {
                /* Round to make for clear "brackets".
                 *
                 * Ideally this would use the actual WHR brackets,
                 * but since their thresholds are dynamic it would
                 * probably be best to be able to specify a rank
                 * directly (instead of its elo). */
                elo = (elo + 50) / 100 * 100;

                return string.Empty;
            }
            else return null;
        }


        public override async Task ExecuteArmed(ServerBattle battle, Say e)
        {
            await battle.SwitchMinElo(elo);
            await battle.SayBattle("Min Elo changed to " + elo);

        }
    }
}
