﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

namespace Strata
{
    //This is the main Monobehaviour class that will be placed on an object in your scene and used to generate your levels
    public class BoardGenerator : MonoBehaviour
    {
        [Tooltip("If true, will automatically build the level when the scene is initialized, otherwise you can call BuildLevel from your own game manager script.")]
        public bool buildOnStart;

        [Tooltip("This is useful if you want true random generation each time, uses a random number instead of the seeded value")]
        public bool randomSeed;

        [Tooltip("Set this to true if you want to create daily challenges for all of your players that are the same.")]
        public bool useDailySeed;

        [Tooltip("Reference to the Tilemap component that will be used to display generated levels")]
        public Tilemap tilemap;

        [Tooltip("BoardGenerationProfiles hold all information needed to generate a type of level, tiles, generators etc.")]
         public BoardGenerationProfile boardGenerationProfile;

        //This is where all the generated data is recorded as ASCII characters from the various generator passes
        //before being instantiated as Tilemap or whatever you choose

        public char[,] boardGridAsCharacters;

        //This is a dictionary of ASCII characters and BoardLibraryEntries which is used to look up data associated with a 
        //character by the system like TileBase, prefab to spawn, etc.
        private Dictionary<char, BoardLibraryEntry> libraryDictionary;

        //Used for generating connected empty space

        //Stores a List of GridPositionLists which contain recordings of empty spaces generated by each generator, used for
        //interlinking spaces created by each Generator pass
        [HideInInspector]
        public List<GridPositionList> emptySpaceLists = new List<GridPositionList>();

        //Records the index in the Generator array that we are currently recording empty spaces for, so that we can make sure
        //that we connect all generators that generate empty space and need to be connected.
        [HideInInspector]
        public int currentGeneratorIndexIdForEmptySpaceTracking = 0;

        [HideInInspector]
        public List<GameObject> generatedObjectsToClear;

        // Use this for initialization
        void Start()
        {
            //If buildOnStart is true the level will be generated when the scene loads.
            if (buildOnStart)
            {
                //BuildLevel is a Coroutine to allow you to wait for it to finish, then for example build a navmesh 
                //(I recommend Aron Granberg's A* for 2D pathfinding)
                StartCoroutine(BuildLevel());
            }
        }

        //Clear out all local variables and regenerate the level, useful for testing your algorithms quickly, enter play mode and press 0 repeatedly
        //Worth noting that this does allocate significant memory so you probably don't want to be repeatedly generating levels during performance critical gameplay.


        public void ClearLevel()
        {
            //Debug.Log("CLEARING ----------------------------------------------------------");

            for (int i = 0; i < generatedObjectsToClear.Count; i++)
            {
                GameObject toDestroy = generatedObjectsToClear[i];
                if (EditorApplication.isPlaying)
                {
                    Destroy(toDestroy);
                }
                else
                {
                    DestroyImmediate(toDestroy);
                }
                
            }

            generatedObjectsToClear.Clear();

            tilemap.ClearAllTiles();
            emptySpaceLists.Clear();
            currentGeneratorIndexIdForEmptySpaceTracking = 0;
            SetupEmptyGrid();

            //It's probably a good idea to manually trigger garbage collection here before your game starts (optional)
            System.GC.Collect();
        }

        //This sets the state of the random number generator to produce predictable, repeatable random generation
        void SetRandomStateFromStringSeed()
        {
            int seedInt = 0;

            //If you're using random seeding, just pick a random number for the seed, this *will not* produce repeating results
            if (randomSeed)
            {
                seedInt = UnityEngine.Random.Range(0, 100000);
            }
            //If you're using daily seeding (for a daily challenge approach) this will create a hash based on today's date
            else if (useDailySeed)
            {
                seedInt = System.DateTime.Today.GetHashCode();
            }

            //Otherwise it will use the seed set in the BoardGenerationProfile, you could have players enter this at start
            //of a run or auto-suggest random seeds for them to give them some control over seeding if desired.
            else
            {
                seedInt = boardGenerationProfile.seedValue.GetHashCode();
            }
            
            //Set the Random to the selected seed type
            UnityEngine.Random.InitState(seedInt);

        }


        //These operations are separate from BoardGeneration since they don't need to be repeated for every level and
        //they generate a lot of garbage allocations.
        public void InitializeGeneration()
        {
            //Setup the BoardLibrary, in this case build the dictionary of ChanceCharacters
            boardGenerationProfile.boardLibrary.Initialize();

            //Build the Dictionary of BoardLibraryEntries to get it ready for use before generation
            InitializeLibraryDictionary();
        }

        public IEnumerator BuildLevel()
        {

            InitializeGeneration();
            //Choose seeding RNG approach (see above)
            SetRandomStateFromStringSeed();

            //Clear the Tilemap before refilling it
            if (tilemap != null)
            {
                tilemap.ClearAllTiles();
            }

            //Build an empty grid in our character array to get ready for filling
            SetupEmptyGrid();

            //Run the generation process
            RunGenerators();
            //Once we've generated our level, turn the grid of ASCII characters into actual viewable data, like a Tilemap
            InstantiateGeneratedLevelData();


            yield return null;
        }


        private void RunGenerators()
        {

            //Go through all the generators in the currently loaded profile
            for (int i = 0; i < boardGenerationProfile.generators.Count; i++)
            {
                //For each Generator in the list, set up a new list of GridPositions, so that we can record it's empty spaces separately and match to it
                emptySpaceLists.Add(new GridPositionList());
                //If the Generator has it's generatesEmptySpace flag set to true in the Inspector
                if (boardGenerationProfile.generators[i].generatesEmptySpace)
                {
                    //Match the current index of the Generator in the array to a list of empty spaces
                    currentGeneratorIndexIdForEmptySpaceTracking = i;
                }

                //Run the generator
                boardGenerationProfile.generators[i].Generate(this);


            }
        }


        //This is used primarily during the design process to allow rapid regeneration of the level at runtime, see immediate below
        public void ClearAndRebuild()
        {
            ClearLevel();
            StartCoroutine(BuildLevel());
        }

#if UNITY_EDITOR
        //Checking to see if the 0 (zero) key is pressed during play mode, only in the Unity Editor. Remove the if/endif if you want this in your build for testing.
        private void Update()
        {
            //Check for the 0 key on the numpad or alphanumeric 0
            if (Input.GetKeyUp(KeyCode.Keypad0) || Input.GetKeyUp(KeyCode.Alpha0))
            {
                //And empty all collections and data, then rebuild the level.
                ClearAndRebuild();
            }
        }


#endif
       

        //Create an empty two dimensional grid of ASCII characters to prepare for generation
        void SetupEmptyGrid()
        {
            boardGridAsCharacters = new char[boardGenerationProfile.boardHorizontalSize, boardGenerationProfile.boardVerticalSize];
            for (int i = 0; i < boardGenerationProfile.boardHorizontalSize; i++)
            {
                for (int j = 0; j < boardGenerationProfile.boardVerticalSize; j++)
                {
                    boardGridAsCharacters[i, j] = boardGenerationProfile.boardLibrary.GetDefaultEmptyChar();
                }
            }
        }

        //Set up the BoardLibraryDictionary to prepare for generation
        public void InitializeLibraryDictionary()
        {
            //Create a new dictionary of characters and BoardLibraryEntry objects
            libraryDictionary = new Dictionary<char, BoardLibraryEntry>();

            //Loop through the list of all BoardLibrary Entry objects (set up by you in the inspector) and add  them to the dictionary matching their
            //characterId to the Entry object so that we can look things up based on the characterId
            for (int i = 0; i < boardGenerationProfile.boardLibrary.boardLibraryEntryList.Count; i++)
            {
                libraryDictionary.Add(boardGenerationProfile.boardLibrary.boardLibraryEntryList[i].characterId, boardGenerationProfile.boardLibrary.boardLibraryEntryList[i]);
            }
        }

        //Now that we've built a dictionary of characters and BoardLibraryEntry objects, you can query that Dictionary with this function
        public BoardLibraryEntry GetLibraryEntryViaCharacterId(char charId)
        {
            BoardLibraryEntry entry = null;
            //Check to see if that character is in the dictionary
            if (libraryDictionary.ContainsKey(charId))
            {
                //If it is, set entry to equal the returned entry
                entry = libraryDictionary[charId];
            }
            else
            {  
                //If the character is null (never assigned) just return the default empty tile from the boardLibrary, this is predefined to a black tile during
                //initial creation of the BoardLibrary
                if (charId == '\0')
                {
                    return boardGenerationProfile.boardLibrary.GetDefaultEntry();
                }
                
            }

            return entry;
        }

        //This method turns our array of ASCII data into actual displayable data in Unity
        public void InstantiateGeneratedLevelData()
        {
            //Loop over the two dimensional array of characters along the x and y axes
            for (int x = 0; x < boardGenerationProfile.boardHorizontalSize; x++)
            {
                for (int y = 0; y < boardGenerationProfile.boardVerticalSize; y++)
                {
                    Vector2 spawnPos = new Vector2(x, y);
                    //Spawn something at the coordinates based on the character stored in the array
                    CreateMapEntryFromGrid(boardGridAsCharacters[x, y], spawnPos);
                }
            }
        }

        public void CreateMapEntryFromGrid(char charId, Vector2 position)
        {
            //Get the relevant library entry based on the character found
            BoardLibraryEntry entryToSpawn = GetLibraryEntryViaCharacterId(charId);

            
            if (entryToSpawn != null)
            {
                //Use the currently assigned InstantationTechnique, the default is TilemapInstantiationTechnique which reads ASCII characters into 
                //TileBase objects and draws them on a Tilemap. Other options would be to spawn 3D objects, draw ASCII directly or whatever you can imagine.
                boardGenerationProfile.boardLibrary.instantiationTechnique.SpawnBoardSquare(this, position, entryToSpawn);
            }
        }
        
        //Simple helper function to get a random GridPosition inside the board
        public GridPosition GetRandomGridPosition()
        {
            GridPosition randomPosition = new GridPosition(UnityEngine.Random.Range(0, boardGenerationProfile.boardHorizontalSize), UnityEngine.Random.Range(0, boardGenerationProfile.boardVerticalSize));
            return randomPosition;
        }

        //This takes a RoomTemplate object and draws it to the grid at a given position
        public void DrawTemplate(int x, int y, RoomTemplate templateToSpawn, bool overwriteFilledCharacters, bool inConnectedPlayableArea)
        {
            
            int charIndex = 0;

            //Loop over the array of characters stored in the RoomTemplate and read them, then write them into the boardGridAsCharacters at the specified coordinates
            for (int i = 0; i < templateToSpawn.roomSizeX; i++)
            {
                for (int j = 0; j < templateToSpawn.roomSizeY; j++)
                {
                    WriteToBoardGrid(x + i, y + j, templateToSpawn.roomChars[charIndex], overwriteFilledCharacters, inConnectedPlayableArea);
                    charIndex++;
                }
            }
        }

        //Use this to check if a space is within the grid before attempting writing to it
        bool TestIfInGrid(int x, int y)
        {
            if (x < boardGenerationProfile.boardHorizontalSize && y < boardGenerationProfile.boardVerticalSize && x >= 0 && y >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Use this to test a position on the grid if it has a certain character
        public bool TestIfSpaceIsInGridAndMatchesChar(GridPosition spaceToTest, char charToTest)
        {
            if (TestIfInGrid(spaceToTest.x, spaceToTest.y))
            {
                if (boardGridAsCharacters[spaceToTest.x, spaceToTest.y] == charToTest)
                {
                    return true;
                }
            }

            return false;
        }

        //This is the function we use to write characters to our boardGrid, the parameters include x and y coordinates, the character to write, whether this should
        //overwrite spaces that are not empty (overwrite previous data) and if it is generating space that we will want to connect together (empty, traversable player space)
        public void WriteToBoardGrid(int x, int y, char charIdToWrite, bool overwriteFilledSpaces, bool inConnectedPlayableArea)
        {
            //Check if the space is valid in the grid
            if (TestIfInGrid(x, y))
            {
                //Check if we want to overwrite whatever is there
                if (overwriteFilledSpaces)
                {

                    //Check if this is a Chance character, chance characters are used for authoring potential spaces and will be transformed to something else
                    //when being written to the boardGrid. We do this here before writing to the grid, then write it to the grid.
                    char nextChar = boardGenerationProfile.boardLibrary.TestCharForChanceBeforeWritingToGrid(charIdToWrite);
                    boardGridAsCharacters[x, y] = nextChar;
                }
                else
                {
                    //If we're not overwriting, we want to check if the space is empty (it matches our specified empty character, usually 0.
                    if (boardGridAsCharacters[x, y] == boardGenerationProfile.boardLibrary.GetDefaultEmptyChar())
                    {
                        //If it matches our empty character, test it for Chance, then write it
                        char nextChar = boardGenerationProfile.boardLibrary.TestCharForChanceBeforeWritingToGrid(charIdToWrite);
                        boardGridAsCharacters[x, y] = nextChar;
                    }
                }


                //Now that we've written our character, if it was an empty, we want to record that so we can connect it later potentially
                if (boardGridAsCharacters[x, y] == boardGenerationProfile.boardLibrary.GetDefaultEmptyChar() && inConnectedPlayableArea)
                {
                    //Wrote an empty space to grid, let's add it to our list of lists
                    GridPosition emptyPosition = new GridPosition(x, y);
                    RecordEmptySpacesLeftByEachGenerator(emptyPosition);
                }
                else
                {
                    //If it's not empty, we'll make sure to remove it from our list of empty spaces previously recorded, since we may have overwritten empty space
                    GridPosition filledPosition = new GridPosition(x, y);
                    RemoveFilledSpaceFromEmptyLists(filledPosition);
                }
            }

        }

        //This method adds empty spaces to our lists of empty spaces for each generator
        public void RecordEmptySpacesLeftByEachGenerator(GridPosition emptyPosition)
        {
            //The empty space lists correspond to the array index of the generator so that we can make sure to connect all relevant generators
            emptySpaceLists[currentGeneratorIndexIdForEmptySpaceTracking].gridPositionList.Add(emptyPosition);
        }

        //We may have overwritten once empty spaces, so let's remove the empty space we just wrote
        public void RemoveFilledSpaceFromEmptyLists(GridPosition filledPosition)
        {
            //Loop through all the empty space lists and check them, then remove any empty spaces that have been filled.
            for (int i = 0; i < emptySpaceLists.Count; i++)
            {
                for (int j = emptySpaceLists[i].gridPositionList.Count - 1; j > -1; j--)
                {
                    if (emptySpaceLists[i].gridPositionList[j].x == filledPosition.x && emptySpaceLists[i].gridPositionList[j].y == filledPosition.y)
                    {
                        emptySpaceLists[i].gridPositionList.RemoveAt(j);
                    }

                }
            }

        }


        //Use this method to get a random empty space from our list of empty space lists, based on the index of the Generator in the Generator array
        public GridPosition GetRandomEmptyGridPositionFromLastEmptySpaceGeneratorInStack(BoardGenerator boardGenerator)
        {
            int genIndex = 0;
            //Loop through and stores the index of the most recently added Generator that creates empty space
            for (int i = 0; i < boardGenerationProfile.generators.Count; i++)
            {
                if (boardGenerationProfile.generators[i].generatesEmptySpace)
                {
                    genIndex = i;
                }
            }
            //Once we've got the index, let's get a random empty space generated by it.
            GridPosition randPosition = emptySpaceLists[genIndex].gridPositionList[UnityEngine.Random.Range(0, emptySpaceLists[genIndex].gridPositionList.Count)];

            return randPosition;
        }



        //Use this to input a Vector2 and return a GridPosition
        public GridPosition Vector2ToGridPosition(Vector2 vectorPos)
        {
            GridPosition convertedPosition = new GridPosition((int)vectorPos.x, (int)vectorPos.y);

            return convertedPosition;
        }

        //Convert a GridPosition to a Vector2
        public Vector2 GridPositionToVector2(GridPosition gridPosition)
        {
            Vector2 convertedPosition = new Vector2(gridPosition.x, gridPosition.y);
            return convertedPosition;
        }

        //Roll dice between 0-100, used for percentage based randomness
        public bool RollPercentage(int chanceToHit)
        {
            int randomResult = UnityEngine.Random.Range(0, 100);
            if (randomResult < chanceToHit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

