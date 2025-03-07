﻿using Engine.Items;
using Engine.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Engine.Tests.Unit
{
	public class BooleanItemTest
	{
		private readonly static Person creator = new Person();
		[Fact]
		public void SingleItem()
		{
			var item = new BooleanItem("Is US citizen?");
			var checklist = new Checklist(creator, item);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item).To(true);
			Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());
			creator.Sets(item).To(false);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());
			creator.Reset(item);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item).To(true);
			Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());
		}

		[Fact]
		public void MultipleItems()
		{
			var item1 = "Is US citizen?".TrueFalse();
			var item2 = "Is Indian citizen?".TrueFalse();
			var item3 = "Is Nordic citizen?".TrueFalse();
			var checklist = new Checklist(creator, item1, item2, item3);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item1).To(true);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());

			creator.Sets(item2).To(true);
			creator.Sets(item3).To(true);
			Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());
			creator.Sets(item2).To(false);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());

			creator.Reset(item1);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());
			creator.Reset(item2);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			var visitor = new CurrentAnswers(checklist);
			Assert.Null(visitor.value("Is US citizen?"));
			Assert.Null(visitor.value("Is Indian citizen?"));
			Assert.Equal(true, visitor.value("Is Nordic citizen?"));
			var output = new PrettyPrint(checklist).Result();
		//	Assert.Equal("", output);
		}
		[Fact]
		public void CancelItem()
		{
			var item1 = new BooleanItem("Is US citizen?");
			var item2 = new BooleanItem("Is US citizen?");
			var checklist = new Checklist(creator, item1, item2);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item1).To(true);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item2).To(false);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());
			creator.Cancel(item2).In(checklist);
			Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());
		}
		[Fact]
		public void ReplaceItems()
		{
			var item1 = new BooleanItem("Is US citizen?");
			var item2 = new BooleanItem("Is US citizen?");
			var item3 = new BooleanItem("Is US citizen?");
			var item4 = new BooleanItem("Is US citizen?");
			var checklist = new Checklist(creator, item1, item2);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item1).To(true);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item2).To(false);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());
			creator.Cancel(item2).In(checklist);
			creator.Add(item3, item4).In(checklist);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item4).To(false);
			Assert.Equal(ChecklistStatus.Failed, checklist.Status());
		}
		[Fact]
		public void InvalidValue()
		{
			var item = new BooleanItem("Is US citizen?");
			var checklist = new Checklist(creator, item);
			Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
			creator.Sets(item).To(true);
			Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());
			Assert.Throws<InvalidCastException>(() => creator.Sets(item).To("green"));
		}
	}
}
