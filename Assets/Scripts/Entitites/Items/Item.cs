// Copyright (C) 2018 Bristol Street Studios. All rights reserved.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;

/// <summary> The parent class of all items. </summary>
/// <remarks>
/// All items are intangible and self-destruct when touched by the player .
/// </remarks>
public class Item : Entity {

	/// <summary>
	/// Causes this item to self-destruct when touched by the player.
	/// </summary>
	protected override void OnPlayerEnter() {
		Kill (this); }
}

