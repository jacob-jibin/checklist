﻿using Engine.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Items
{
	public class CurrentAnswers : ChecklistVisitor
	{
		private Dictionary<string, object?> _answers = [];
		public CurrentAnswers(Checklist checklist)
		{
			checklist.Accept(this);

		}

		public object? value(string question)
		{
			if (!_answers.ContainsKey(question)) throw new ArgumentException("No Such Question.");
			return _answers[question];
		}

		public void Visit(BooleanItem item,string question, bool? value, Dictionary<Person, List<Operation>> operations)
		{
			_answers[question]= value;
		}

		public void Visit(MultipleChoiceItem item, string question, object? value, Dictionary<Person, List<Operation>> operations)
		{
			_answers[question] = value;
		}

	}

}
