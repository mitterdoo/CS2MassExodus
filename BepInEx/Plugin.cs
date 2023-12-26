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

using BepInEx;
using HarmonyLib;
using System.Reflection;
using Game.Common;
using Game;

namespace MassExodus
{
	[BepInPlugin(GUID, "Mass Exodus", "1.0.2")]
	[HarmonyPatch]
	public class Plugin : BaseUnityPlugin
	{
		public const string GUID = "com.ranthos.CS2.MassExodus";
		private Mod _mod;
		private void Awake()
		{
			// Plugin startup logic

			_mod = new Mod(Logger);
			_mod.OnLoad();

			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
			Logger.LogInfo($"Plugin {GUID} is loaded!");
		}


		/// <summary>
		/// Harmony postfix to <see cref="SystemOrder.Initialize"/> to substitute for IMod.OnCreateWorld.
		/// </summary>
		/// <param name="updateSystem"><see cref="GameManager"/> <see cref="UpdateSystem"/> instance.</param>
		[HarmonyPatch(typeof(SystemOrder), nameof(SystemOrder.Initialize))]
		[HarmonyPostfix]
		private static void InjectSystems(UpdateSystem updateSystem) => Mod.Instance.OnCreateWorld(updateSystem);
	}
}
