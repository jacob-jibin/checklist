﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Engine.Items;
using Engine.Persons;
using Xunit;

namespace Engine.Tests.Unit
{   
   
    public class GroupItemTest
    {
        private static readonly Person Creator = new Person();
        [Fact]
        public void Group()
        {
            var item1 = "first question".TrueFalse();
            var item2 = "second question".TrueFalse();
            var item3 = "third question".TrueFalse();
            var group = new GroupItem(item1,item2,item3);
            var checklist = new Checklist(Creator, group);

            Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
            Creator.Sets(item1).To(true);
            Creator.Sets(item2).To(true);
            Assert.Equal(ChecklistStatus.InProgress, checklist.Status());
            Creator.Sets(item3).To(true);
            Assert.Equal(ChecklistStatus.Succeeded, checklist.Status());

            Creator.Sets(item2).To(false);
            Assert.Equal(ChecklistStatus.Failed, checklist.Status()); 
            Creator.Reset(item2);
            Assert.Equal(ChecklistStatus.InProgress, checklist.Status());

        }
    }
}
