﻿namespace Engine.Items
{
	public class BooleanItem : Item
	{
		private bool? _hasSucceeded;
		private readonly string _question;

		public BooleanItem(string question)
		{
			_question = question;
		}

		internal override void Be(object value) => _hasSucceeded = (bool)value;

        internal override void Reset() => _hasSucceeded = null;
        
        internal override void Accept(ChecklistVisitor visitor) {
	        visitor.Visit(this,_question, _hasSucceeded, Operations);
        }

        internal override ItemStatus Status() => _hasSucceeded switch
        {
            true => ItemStatus.Succeeded,
            false => ItemStatus.Failed,
            _ => ItemStatus.Unknown,
        };
    }
}