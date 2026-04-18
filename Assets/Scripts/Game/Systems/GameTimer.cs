using R3;
using Cysharp.Threading.Tasks;

public class GameTimer
{
    public ReactiveProperty<int> Time = new(0);

    public async UniTask Start(int seconds)
    {
        Time.Value = seconds;

        while (Time.Value > 0)
        {
            await UniTask.Delay(1000);
            Time.Value--;
        }
    }
}