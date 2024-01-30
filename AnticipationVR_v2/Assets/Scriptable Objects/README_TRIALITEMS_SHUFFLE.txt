The final order of the trial items list was defined as following:


TL;DR: "TrialItems_shuffled" was obtained by shuffling "TrialItems_orig" and the subsequent results a total of three times with the same seed.


- Starting with "TrialItems_orig"

- Used "private static readonly int ConstantSeed = 20230914;" in TrialMainManager

- Set "UnityEngine.Random.InitState(ConstantSeed);"

- Called ShuffleTrialItemsWithSeed()-Method of TrialMainManager (uses Fisher-Yates shuffle algorithm) to shuffle first time

- The resulting list was shuffled a second time with same seed

- The resulting list was shuffled a third and last time with same seed

- Result list was named "TrialItems_shuffled"