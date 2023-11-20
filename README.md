# Gilded Rose Refactoring Kata

For the original readme, see [here](README_GildedRose.md)

---

# Patrick Magee - 20 November 2023

Quick notes on approach and changes made.

Decided upon the the C# .net Core version and removed all other languages from the
repository.

With the Item class being locked (and ignoring adding more if's into the
current UpdateQuality function) there were a few approches.

I could add a few functions to the GildedRose class but as it's functionality 
around the Item class is should be seperated off. Decided on a service class
approach as we can swap out the functionality later on if we have more than
one concurrent rule for items. Had to default the class in GildedRose which
could be removed if the Goblins allow the class to be further changed.

Each function in the class should do just one thing. So if the rules change, 
it's easy to change. Having an interface for the service helped write the
new tests before the code (TDD).

ApprovalTest was fixed to pass in 30 days instead of defaulting to 2. Acceptance 
file was also incorrect as Conjured items weren't degrading as per the
accepted output. They were degrading as per normal items and not at double
the rate.

