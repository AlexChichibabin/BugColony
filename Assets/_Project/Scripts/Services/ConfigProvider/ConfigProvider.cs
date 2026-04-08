using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigProvider : IConfigProvider
{
    private Dictionary<string, LevelConfig> levels;
	private Dictionary<EntityId, EntityConfig> entities;
	private LevelConfig[] levelList;

	public int LevelAmount => levelList.Length;

	public void Load()
	{
		levelList = Resources.LoadAll<LevelConfig>(AssetAddress.LevelsConfigPath);

		entities = Resources.LoadAll<EntityConfig>(AssetAddress.EntitiesConfigPath).ToDictionary(x => x.Id, x => x);

		levels = levelList.ToDictionary(x => x.SceneName, x => x);
	}

	public LevelConfig GetLevel(int index) => levelList[index];
	public LevelConfig GetLevel(string name) => levels[name];
	public EntityConfig GetEntity(EntityId entityId)
	{
		if (entities.ContainsKey(entityId))
			return entities[entityId];
		Debug.Log($"There is no config with id {entityId}");
		return null;
	}
}