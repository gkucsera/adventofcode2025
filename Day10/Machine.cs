namespace Day10;

public class Machine
{
    public bool[] Lights { get; }
    public string Pattern { get; }
    public string JoltagePattern { get; }
    public List<int>[] Buttons { get; }
    public Dictionary<string, int> ButtonMap { get; }
    public int[] Joltage { get; }

    public Machine(string pattern, string[] buttons, string joltage)
    {
        Pattern = string.Join("", pattern.Select(item => item != '.'));
        Lights = new bool[pattern.Length];
        Buttons = buttons.Select(item => item[1..^1].Split(',').Select(int.Parse).ToList()).ToArray();
        ButtonMap = [];
        ButtonMap[string.Join("", Lights.Select(item => '.'))] = 0;
        Joltage = joltage[1..^1].Split(',').Select(int.Parse).ToArray();
        JoltagePattern = joltage[1..^1];
    }

    public void FindBest()
    {
        var currentMax = int.MaxValue;
        DoNextButton(Lights, 1, ref currentMax);
    }

    private void DoNextButton(bool[] lights, int counter, ref int currentMax)
    {
        if (counter > currentMax)
        {
            return;
        }

        for (var i = 0; i < Buttons.Length; i++)
        {
            var nextLight = new bool[lights.Length];
            Array.Copy(lights, nextLight, lights.Length);

            PressButton(nextLight, i);
            var lightsState = string.Join("", nextLight);
            if (lightsState == Pattern)
            {
                currentMax = counter;
            }

            if (ButtonMap.TryGetValue(lightsState, out var minCount))
            {
                if (counter < minCount)
                {
                    ButtonMap[lightsState] = counter;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                ButtonMap[lightsState] = counter;
            }

            DoNextButton(nextLight, counter + 1, ref currentMax);
        }
    }

    private void PressButton(bool[] lights, int button)
    {
        foreach (var item in Buttons[button])
        {
            lights[item] = !lights[item];
        }
    }

    public void FindBestJoltage()
    {
        ButtonMap.Clear();
        var currentMax = int.MaxValue;
        DoNextJoltage(new int[Joltage.Length], 1, ref currentMax);
    }

    private void DoNextJoltage(int[] joltages, int counter, ref int currentMax)
    {
        if (counter > currentMax)
        {
            return;
        }


        for (var i = 0; i < Buttons.Length; i++)
        {
            var nextJoltages = new int[joltages.Length];
            Array.Copy(joltages, nextJoltages, joltages.Length);

            PressJoltageButton(nextJoltages, i);
            if (!IsJoltageStateValid(nextJoltages))
            {
                continue;
            }
            var joltageState = string.Join(",", nextJoltages);
            if (joltageState == JoltagePattern)
            {
                currentMax = counter;
            }

            if (ButtonMap.TryGetValue(joltageState, out var minCount))
            {
                if (counter < minCount)
                {
                    ButtonMap[joltageState] = counter;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                ButtonMap[joltageState] = counter;
            }

            DoNextJoltage(nextJoltages, counter + 1, ref currentMax);
        }
    }

    private bool IsJoltageStateValid(int[] joltages)
    {
        for (int i = 0; i < joltages.Length; i++)
        {
            if (joltages[i] > Joltage[i])
            {
                return false;
            }
        }

        return true;
    }
    private void PressJoltageButton(int[] joltages, int button)
    {
        foreach (var item in Buttons[button])
        {
            joltages[item]++;
        }
    }
}