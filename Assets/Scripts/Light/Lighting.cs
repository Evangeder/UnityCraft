using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCraft.Light
{
    using Blocks;
    using World.Map;
    using Settings;

    public class Lighting
    {
        public Stack<int[]> lightLayers;
        private Queue<BoundingBox> _worldBlockPositionTypes;
        private Queue<BoundingBox> _LightQueue;
        private Queue<BoundingBox> _blockUpdateQueue;
        private BoundingBox lightPropagationBox;
        private GameSettings gameSettings;
        private Map map;

        private int currentLightIndex;
        private int[] lightValueIndexes;
        private Stack<int[]> lightLayerList;
        private int skylightValue;
        private int lastSkylightValue;
        private sbyte[] lightValues;

        public Lighting()
        {
            gameSettings = Object.FindObjectOfType<GameSettings>();
        }

        public void Tick()
        {
            if (lightLayers.Count > 0)
            {
                lightLayers.Pop();
            }

            for (int i = 5; i > 0 && _worldBlockPositionTypes.Count > 0; i--)
            {
                BoundingBox boundingBox = _worldBlockPositionTypes.Dequeue();
                // light code here

                // LEGACY SHIT

                LightInRange(boundingBox);

                //if (++test % 256 == 0) Debug.Log($"Light iteration: {++test2}");
                //Light Queue????
                // call update to every renderer
                //for (int i = 0; i < _rawWorld.worldAccesses.Count; ++i)
                //    ((IWorldAccess)_rawWorld.worldAccesses[i]).markBlockRangeNeedsUpdate(boundingBox.x1, boundingBox.y1, boundingBox.z1, boundingBox.x2, boundingBox.y2, boundingBox.z2);

                // END OF LEGACY SHIT
            }

            if (lightPropagationBox != null)
            {
                PropagateLight(iterations: 8);
                return;
            }

            for (int i = 0; i < 16; i++)
            {
                TickLightQueue();
                TickBlockUpdate();
            }

            int x1 = -999;
            int y1 = -999;
            int z1 = -999;
            int x2 = -999;
            int y2 = -999;
            int z2 = -999;

            for (int i = 1024; currentLightIndex > 0 || lightLayerList.Count > 0; i--)
            {
                if (currentLightIndex == 0)
                {
                    if (lightValueIndexes != null)
                    {
                        lightLayers.Push(lightValueIndexes);
                    }

                    lightValueIndexes = lightLayerList.Pop();
                    currentLightIndex = lightValueIndexes[lightValueIndexes.Length - 1];
                }

                if (currentLightIndex > lightValueIndexes.Length - 32)
                {
                    int lightValue = lightValueIndexes[--currentLightIndex];
                    lightValueIndexes[lightValueIndexes.Length - 1] = currentLightIndex;

                    lightLayerList.Push(lightValueIndexes);

                    lightValueIndexes = GetNextLightLayer();
                    currentLightIndex = 1;
                    lightValueIndexes[0] = lightValue;
                }
                else
                {
                    currentLightIndex--;

                    int currentBlockIndex = lightValueIndexes[currentLightIndex];
                    int x3 = currentBlockIndex % map.Size.x;
                    int y3 = currentBlockIndex / map.Size.x % map.Size.y;
                    int z3 = currentBlockIndex / map.Size.x / map.Size.y % map.Size.z;

                    lightValues[currentBlockIndex >> 3] ^= (sbyte)(1 << (currentBlockIndex & 0x7));
                    currentBlockIndex = map.Heightmap[x3 + z3 * map.Size.x];

                    int calculatedLight = (y3 >= currentBlockIndex) ? map.SkylightSubtracted : 0;
                    sbyte currentBlockId = map.Blocks[GetAddress(x3, y3, z3)].ID;
                    int lightOpacity = gameSettings.Blocks[currentBlockId].LightOpacity;

                    if (lightOpacity > 100)
                    {
                        calculatedLight = 0;
                    }
                    else if (calculatedLight < 15)
                    {
                        if (lightOpacity == 0)
                        {
                            lightOpacity = 1;
                        }

                        if (x3 > 0 && x3 < map.Size.x - 1 && y3 > 0 && y3 < map.Size.y - 1 && z3 > 0 && z3 < map.Size.z - 1)
                        {
                            ClampLightValueToAdjacent(x3 - 1, y3, z3, calculatedLight, lightOpacity);
                            ClampLightValueToAdjacent(x3 + 1, y3, z3, calculatedLight, lightOpacity);
                            ClampLightValueToAdjacent(x3, y3 - 1, z3, calculatedLight, lightOpacity);
                            ClampLightValueToAdjacent(x3, y3 + 1, z3, calculatedLight, lightOpacity);
                            ClampLightValueToAdjacent(x3, y3, z3 - 1, calculatedLight, lightOpacity);
                            ClampLightValueToAdjacent(x3, y3, z3 + 1, calculatedLight, lightOpacity);
                        }
                    }

                    if (calculatedLight < gameSettings.Blocks[currentBlockId].LightValue)
                        calculatedLight = gameSettings.Blocks[currentBlockId].LightValue;

                    lightOpacity = GetBlockData(x3, y3, z3).LightOpacity & 0xF;
                    if (lightOpacity == calculatedLight)
                        continue;

                    SetBlockLight(x3, y3, z3, (sbyte)((map.Blocks[GetAddress(x3, y3, z3)].Light & 0xF0) + calculatedLight));

                    void ChangeBlockLight(int _x, int _y, int _z)
                    {
                        int blockAddress = GetAddress(_x, _y, _z);
                        if ((lightValues[blockAddress >> 3] & 1 << (blockAddress & 0x7)) == 0x0)
                        {
                            lightValues[blockAddress >> 3] |= (sbyte)(1 << (blockAddress & 0x7));
                            lightValueIndexes[currentLightIndex++] = blockAddress;
                        }
                    }

                    if (x3 > 0 && (map.Blocks[GetAddress(x3 - 1, y3, z3)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3 - 1, y3, z3);
                    }
                    if (x3 < map.Size.x - 1 && (map.Blocks[GetAddress(x3 + 1, y3, z3)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3 + 1, y3, z3);
                    }
                    if (y3 > 0 && (map.Blocks[GetAddress(x3, y3 - 1, z3)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3, y3 - 1, z3);
                    }
                    if (y3 < map.Size.y - 1 && (map.Blocks[GetAddress(x3, y3 + 1, z3)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3, y3 + 1, z3);
                    }
                    if (z3 > 0 && (map.Blocks[GetAddress(x3, y3, z3 - 1)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3, y3, z3 - 1);
                    }
                    if (z3 < map.Size.z - 1 && (map.Blocks[GetAddress(x3, y3, z3 + 1)].Light & 0xF) != calculatedLight - 1)
                    {
                        ChangeBlockLight(x3, y3, z3 + 1);
                    }

                    if (x1 == -999)
                    {
                        x1 = x2 = x3;
                        y1 = y2 = y3;
                        z1 = z2 = z3;
                    }

                    x1 = x3 < x1 ? x3 : x1;
                    x2 = x3 > x2 ? x3 : x2;
                    y1 = y3 < y1 ? y3 : y1;
                    y2 = y3 > y2 ? y3 : y2;
                    z1 = z3 < z1 ? z3 : z1;

                    if (z3 < z1)
                    {
                        z1 = z3;
                    }
                    else
                    {
                        if (z3 <= z2)
                        {
                            continue;
                        }

                        z2 = z3;
                    }
                }
            }
        }

        private int[] GetNextLightLayer()
        {
            if (lightLayers.Count > 0)
            {
                return lightLayers.Pop();
            }
            return new int[32768];
        }

        private void ClampLightValueToAdjacent(int x, int y, int z, int calculatedLight, int lightOpacity)
        {
            int adjacentBlockLightValue = (map.Blocks[GetAddress(x, y, z)].Light & 0xF) - lightOpacity;
            if (adjacentBlockLightValue > calculatedLight)
                calculatedLight = adjacentBlockLightValue;
        }

        private void TickLightQueue()
        {
            if (_LightQueue.Count > 0)
            {
                BoundingBox blockRange = _LightQueue.Dequeue();
                LightInRange(blockRange);
            }
        }

        private void TickBlockUpdate()
        {
            if (_blockUpdateQueue.Count > 0)
            {
                BoundingBox blockRange = _LightQueue.Dequeue();
                for (int x = blockRange.x1; x < blockRange.x2; x++)
                {
                    for (int z = blockRange.z1; z < blockRange.z2; z++)
                    {
                        int height = map.Heightmap[x + z * map.Size.x];
                        int y1 = map.Size.y - 1;

                        while (y1 > 0)
                        {
                            if (gameSettings.Blocks[map.Blocks[GetAddress(x - 1, y1, z)].ID].LightOpacity != 0)
                            {
                                break;
                            }
                            y1--;
                        }

                        if (height != y1)
                        {
                            LightInRange(new BoundingBox
                            {
                                x1 = x,
                                x2 = x + 1,
                                y1 = Mathf.Min(height, y1),
                                y2 = Mathf.Max(height, y1),
                                z1 = z,
                                z2 = z + 1
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// worldClientMethod2
        /// </summary>
        public void EnqueueUpdate(int x1, int y1, int x2, int y2)
            => _blockUpdateQueue.Enqueue(new BoundingBox 
            {
                x1 = x1,
                y1 = y1,
                z1 = 0,
                x2 = x2,
                y2 = y2,
                z2 = 0
            });

        public void UpdateSkylight(int skylightSubtracted)
        {
            skylightSubtracted = Mathf.Clamp(skylightSubtracted, 0, 15);

            if ((lastSkylightValue = skylightSubtracted - map.SkylightSubtracted) == 0)
            {
                return;
            }

            skylightValue = map.SkylightSubtracted;
            map.SkylightSubtracted = skylightSubtracted;

            while (lightPropagationBox != null)
            {
                PropagateLight(iterations: 64);
            }

            lightPropagationBox = new BoundingBox
            {
                x1 = 0,
                y1 = 0,
                z1 = 0,
                x2 = map.Size.x,
                y2 = map.Size.y,
                z2 = map.Size.z,
            };
        }

        private void PropagateLight(int iterations)
        {
            for (int x = lightPropagationBox.x1; x < lightPropagationBox.x2; ++x)
            {
                if (iterations-- <= 0 && x != lightPropagationBox.x2 - 1)
                {
                    lightPropagationBox.x1 = x;
                    return;
                }

                for (int z = lightPropagationBox.z1; z < lightPropagationBox.z2; ++z)
                {
                    int y = map.Heightmap[x + z * map.Size.x] - 1;
                    while (y > 0 && gameSettings.Blocks[map.Blocks[GetAddress(x, y, z)].ID].LightOpacity < 100)
                    {
                        --y;
                    }

                    for (++y; y < map.Size.y; ++y)
                    {
                        int blockAddress = GetAddress(x, y, z);
                        int lightValue = map.Blocks[blockAddress].Light & 0xF;

                        if (GetBlockDataLight(blockAddress) == 0 && lightValue <= skylightValue)
                        {
                            if (lastSkylightValue < 0 && lightValue > 0)
                            {
                                ModifyBlockLight(blockAddress, +1);
                            }
                            else if (lastSkylightValue > 0 && lightValue < 15)
                            {
                                ModifyBlockLight(blockAddress, -1);
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < map.Size.x; x += 32)
                for (int z = 0; z < map.Size.z; z += 32)
                {
                    _LightQueue.Enqueue(new BoundingBox {
                        x1 = x,
                        y1 = 0,
                        z1 = z,
                        x2 = x + 32,
                        y2 = map.Size.y,
                        z2 = z + 32 
                    });

                    _worldBlockPositionTypes.Enqueue(new BoundingBox
                    {
                        x1 = x,
                        y1 = 0,
                        z1 = z,
                        x2 = x + 32,
                        y2 = map.Size.y,
                        z2 = z + 32
                    }); 
                }

            // call update to every renderer
            //for (int j = 0; j < rawWorld.worldAccesses.Count; ++j)
            //    ((IWorldAccess)rawWorld.worldAccesses[j]).updateAllRenderers();

            lightPropagationBox = null;
        }

        public void EnqueueUpdate(int x1, int y1, int z1, int x2, int y2, int z2)
            => _LightQueue.Enqueue(new BoundingBox { x1 = x1, x2 = x2, y1 = y1, y2 = y2, z1 = z1, z2 = z2 });

        private void LightInRange(BoundingBox box)
        {
            int lightValue, metadataIndex;

            for (; box.y1 < box.y2; ++box.y1)
            {
                for (int z = box.z1; z < box.y2; ++z)
                {
                    for (int x = box.x1; x < box.x2; ++x)
                    {
                        lightValue = x + box.y1 * map.Size.x + z * map.Size.y * map.Size.z;

                        if ((lightValues[lightValue >> 3] & 1 << (lightValue & 0x7)) == 0x0)
                        {
                            metadataIndex = lightValue >> 3;
                            lightValues[metadataIndex] |= (sbyte)(1 << (sbyte)(lightValue & 0x07));
                            lightValueIndexes[currentLightIndex++] = lightValue;

                            if (currentLightIndex > lightValueIndexes.Length - 32)
                            {
                                currentLightIndex--;
                                lightValue = lightValueIndexes[currentLightIndex];

                                lightValueIndexes[^1] = currentLightIndex;
                                lightLayerList.Push(lightValueIndexes);

                                lightValueIndexes = GetNextLightLayer();
                                currentLightIndex = 1;
                                lightValueIndexes[0] = lightValue;
                            }
                        }
                    }
                }
            }
        }

        private int GetAddress(int x, int y, int z) => (y * map.Size.y + z) * map.Size.x + x;

        private BlockDefinition GetBlockData(int x, int y, int z)
        {
            var block = map.Blocks[GetAddress(x, y, z)];
            return gameSettings.Blocks[block.ID];
        }

        private int GetBlockDataLight(int address)
        {
            return gameSettings.Blocks[map.Blocks[address].ID].LightValue;
        }

        private void ModifyBlockLight(int address, int value)
        {
            var currentBlock = map.Blocks[address];
            map.Blocks[address] = new BlockMetadata(map.Blocks[address]) { Light = (sbyte)(currentBlock.Light + value) };

        }

        private void SetBlockLight(int x, int y, int z, sbyte light)
        {
            map.Blocks[GetAddress(x, y, z)] = new BlockMetadata(map.Blocks[GetAddress(x, y, z)]) { Light = light };
        }
    }
}
