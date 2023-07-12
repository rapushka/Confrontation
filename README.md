# Confrontation
my Diploma Project

Music: Adventure Beyond by Alexander Nakarada (www.serpentsoundstudios.com)
Licensed under Creative Commons BY Attribution 4.0 License
http://creativecommons.org/licenses/by/4.0/
A strategy game about land conquest.

# Setting.

Fantasy Middle Ages. Basic principles: castles, magic, possibly fantasy creatures. 

# Cor gameplay.

## Playing field. 

Consists of hexagonal cells. The size and shape of the field is arbitrary. The field is divided into regions. Each region has a settlement. When a settlement is captured, the entire region is under the control of the one who captured it. 

## Cell. 

The basic element of the playing field. A cell can either belong to a player or be neutral. In empty cells you can build buildings. If a cell changes its affiliation, the buildings are also under the control of the player. If a cell becomes neutral, the buildings are retained but do not give any bonuses to anyone.

## The process of capturing a cell

To capture a settlement, you must place military forces in it. Military forces can only be placed if the defense parameter is 0.

## Combat for the settlement.

The attacker sends a unit to capture a settlement. When the unit reaches the settlement, the opposing sides are mutually destroyed. The unit deals damage equal to its strength to the defenders. The defense distributes the damage evenly between the garrison and the settlement army. If one of the components has a value of 0, all damage goes to the remaining component.

### Combat Results. 
If the defender's army remains in the settlement and the attacker's army is completely destroyed, the settlement remains under the defender's control.
If there is no defender's army left in the settlement and the attacker's army is completely destroyed and the garrison forces remain - the settlement gains neutral status
If the garrison and the defender's army are completely destroyed and the attacker's army remains, the settlement becomes under the control of the attacker.

## Settlement parameters

Territory - set of controlled cells.

Level - the numerical value of the settlement's level.

Defenders consist of two separate units:
Garrison defenders - number of units in the garrison. It has a maximum value. The maximum value depends on the settlement level. It is restored over time. 
Player's squad - number of player's units in the settlement. It has no maximum value. The number can only be controlled by the player sending his forces to the settlement.

# Buildings 
Mine - generates gold income over time.
Farm - Increases the speed of unit creation.
Stables - Increases the speed of units
Forge - Increases the combat strength of units.
Barracks - Produces units over time
Mage Tower - generates mana income over time
Quarry - increases the damage absorption rate of the defenders of all settlements when defending them
Workshop - decreases the damage absorption rate of the settlement's defenders when attacking.
Fort - you can move units to this building. Does not have a garrison, but provides a fortification bonus.
Warlock's Workshop (as an idea, not finalized) - changes the damage and defense elements of units. 
Pegasus Corral (as an idea, not finalized) - Gives armies the ability to fly. Perhaps a stage of development of the stable. Probably additionally will increase speed

Buildings have a level. Depending on the level increases the characteristic of the building
A building can be erected in an empty cell. A building cannot be destroyed (maybe except for a fort).


# Unit

The basic unit of a player's army. The main target for the application of building bonuses. An entity that can be controlled by the player.

### Parameters:
Speed - The speed at which the unit moves across the playing field
Strength - Numerical value of damage/armor (combat strength) of the unit
Training speed - the time for which a unit is created in the game.

### Strength Modifiers:
Defense - Percentage by which the unit's damage is reduced when defending it
Attack - Percentage by which the unit's damage increases when attacking.

Only the base strength is displayed. Modifiers are applied hidden from the player.


# Squad

The basic designation of one or more units united by a single task.

# Magic

Severe Blizzard - reduces the speed of all units moving at the moment (those that have not left yet will put on skis).
Wind from the South - completely disables the bonus of all farms in the game for a while.
Philologic Stone - (turns iron into gelatin) temporarily disables the bonus of all forges in the game
Spur Geologists - temporarily increases the bonus of allied forges for a while
??? - (it's not trash, it's fertilizer) temporarily increases the bonus of their farms for a while.  
Poeehaly - (as a result of a magical error, sheep have grown wings. So why not take advantage of that) Gives all units in a campaign the ability to fly.
Not So Dull - Temporarily increases income from all allied mines
Pack of Wolves - temporarily increases the speed of all allied units
Plague - temporarily decreases the combat strength of all allied units in a campaign.
Three-Eyed Tree - temporarily removes the Fog of War for a while


# Capital

Has all the basic properties of a settlement. The loss of a capital by the owner results in his loss. 

### Differences:
Has the Barracks property - Produces units
Has the property of a mine - Produces gold
Has the Farm property - Increases the speed of hiring units.

If you improve a capital, all properties are improved.


# Unit Management. 

The basis is drag and drop mechanics, when the beginning of touching allied buildings, it is the formation of squads, and the end of touching on a building (allied or enemy) giving orders.


### Squad Formation:
A barracks forms a unit from all available units
A settlement forms a unit from half of the player's available units.

Giving an order. In fact, there is only 1 order - move units to the settlement. If the settlement is allied, units will be placed in it. If the settlement is enemy, a battle for the settlement will take place.

# Currencies

Kalym is the main currency used globally. Can be used for non-gameplay mechanics (only academy for now). Obtained for completing campaign missions.

Gold - gameplay currency. It is used to build and improve buildings.
Mana - gameplay currency. used to use spells


# Races(concept). 

Influence on unit parameters (speed/strength/training). Influence on building parameters. Unique buildings.
