/*  
	Copyright (C) 2023 Ranthos (Connor Ashcroft)

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 */

using Game;
using Game.Common;
using Game.Citizens;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;
using Game.Agents;

namespace MassExodus.Systems
{
	internal sealed partial class MassExodusSystem : GameSystemBase
	{

		private EntityQuery _mainQuery;

		protected override void OnCreate()
		{
			base.OnCreate();
			Mod.Instance.Logger.LogInfo("OnCreate!");
			_mainQuery = GetEntityQuery(new EntityQueryDesc()
			{
				All =
				[
					ComponentType.ReadOnly<Household>()
				],
				None =
				[
					ComponentType.ReadOnly<MovingAway>(),
					ComponentType.ReadOnly<Deleted>(),
					ComponentType.ReadOnly<Temp>()
				]
			});
			RequireForUpdate(_mainQuery);
		}

		protected override void OnDestroy()
		{
			Mod.Instance.Logger.LogInfo("OnDestroy!");
			base.OnDestroy();
		}
		protected override void OnUpdate()
		{
			uint affected = 0;
			foreach (Entity ent in _mainQuery.ToEntityArray(Allocator.Temp))
			{
				affected++;
				EntityManager.AddComponent<MovingAway>(ent);
			}
			if (affected > 0)
			{
				Mod.Instance.Logger.LogInfo($"Found {affected} households!");
			}
		}
	}
}
