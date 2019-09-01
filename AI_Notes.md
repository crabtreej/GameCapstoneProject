# Proposed AI States

### Idle / Haunting Mode
The monster is doing its normal thing, whatever that may be. Prototype will just be wandering around the map.
The monster does not know our player is trying to escape at this point, it is not looking for anything in particular.
Unlikely, but the monster can stay in this mode the whole time if the player successfully does not alert the monster. 
- Triggers: This is what the monster spawns with at the beginning and when our player dies. 

### Hunt Mode
The monster has noticed someone is trying to escape, so it is looking for a player. This is when it would be using data from a heat map implementation.
We need to decide on x amount of time for the monster to be in this mode (go back to idle mode if it never finds the player).  
- Triggers: The monster notices an object out of place or hears a sound. 

### Attack Mode
This is when the monster sees the player. The monster chases the player. Heat map implementation could be used in this mode for the monster to guess where the player is going (optional).
If the player evades the monsters sight for x amount of time, EITHER back to idle or hunt mode. 
- Triggers: The player is in the monster's sight. 

### Suspicious Mode (Optional)
Could be implemented in a hard mode. The monster is not assuming the player is trying to escape, but is setting traps just in case. 
- Triggers: N/A
