﻿using Engine.Items;
using static Engine.Items.ItemStatus;

namespace Engine.Items
{
	public class ConditionalItem(Item baseItem, Item? successItem = null, Item? failItem = null) : Item
	{

        public override void Be(object value) => baseItem.Be(value);

        public override void Reset()
		{
			baseItem.Reset();
		}

		internal override ItemStatus Status()
		{
			if(baseItem.Status() == Succeeded) return successItem?.Status() ?? Succeeded;
			if(baseItem.Status() == Failed) return failItem?.Status() ?? Failed;
			return Unknown;
		} 
	}
}