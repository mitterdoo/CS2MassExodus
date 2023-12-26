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

using BepInEx.Logging;
using Game;
using Game.Modding;
using MassExodus.Systems;

namespace MassExodus
{
	public sealed class Mod(ManualLogSource Logger) : IMod
	{
		public const string ModName = "Mass Exodus";
		public static Mod Instance { get; private set; }

		public ManualLogSource Logger { get; private set; } = Logger;

		public void OnCreateWorld(UpdateSystem updateSystem)
		{
			updateSystem.World.GetOrCreateSystemManaged<MassExodusSystem>();
			updateSystem.UpdateAt<MassExodusSystem>(SystemUpdatePhase.GameSimulation);
		}

		public void OnDispose()
		{
		}

		public void OnLoad()
		{
			Instance = this;
		}
	}
}
