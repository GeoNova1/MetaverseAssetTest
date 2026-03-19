# MetaverseAssetTest
An environment to train boat maneuvering.

### Objective: 
Dock your boat at the port facing portside, whilst avoiding other vessels.

### Controls:
- Move: Arrow Keys
- Quit: Escape

### Known Issues:
The visual of the waves doesn't always align with how they push you. I tried implementing the same noise function in C# as in the shadergraph to see if I could save sending data between the CPU and GPU. They likely diverge due to precision point errors or the difference in the operators. In the future I would try either using a compute shader or calculating everything on the CPU.

### Possible Future Work:
- Fixing the desyncing waves
- More stats (number of collisions, collision force)
- Exporting stats to a file
- Failing from hitting other vessels
- Sound effects
- Particle effects
- Networking to allow other users to train simultaneously or spectate
- More obstacles and weather conditions
