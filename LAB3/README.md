# Lab 3 -- Testing and Mutexes

## Questions

### - How can you test your program without needing to manually go through all the dialogue in Shakespeare's plays?

- By using `HelperFunctions.CountCharacterWords` to count how many words each character has and adding to the `Dictionary`

### - Has writing this code multithreaded helped in any way? Show some numbers in your observations. If your answer is no, under what conditions can multithreading help?

- Multithread approach doesn't show effectiveness in this case, at least not with only 10 files being considered. As seen in previous lab, multithread approach would only be effective if there's more than 10^6 samples being tested at the same time
- Some stats from the program: 
```csharp
Single thread duration: 00hrs:00min:00s:000175ms
Multi thread duration: 00hrs:00min:00s:000478ms 
Number of threads used: 10
Speed up factor: 0.366
```

### - As written, if a character in one play has the same name as a character in another -- e.g. *King* -- it will treat them as the same and artificially increase the word count.  How can you modify your code to treat them as separate, but still store all characters in the single dictionary (you do not need to implement this... just think about how you would do it)?

- I can store output of each file in a seperate list then join them all together after the program is finished.