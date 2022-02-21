﻿using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class HearingAssignJudgeSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;

        public HearingAssignJudgeSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Given(@"I want to Assign a Judge with courtroom details")]
        public void GivenIWantToAssignAJudgeWithCourtroomDetails(Table table)
        {
            _hearing = CreateHearingModel(table);
            EnterJudgeDetails(_hearing.Judge);
        }

        private Hearing CreateHearingModel(Table table)
        {
            var tableRow = table.Rows[0];

            _hearing.Judge.Email = tableRow["Judge or Courtroom Account"];
            //_hearing.Judge.DisplayName = tableRow["Judge Name"];
            //_hearing.Judge.Phone = tableRow["Judge Telephone Number"];
            _scenarioContext["Hearing"] = _hearing;
            return _hearing;
        }

        private void EnterJudgeDetails(Judge judge)
        {
            Driver.FindElement(HearingAssignJudgePage.JudgeEmail).SendKeys(judge.Email);
            ExtensionMethods.FindElementWithWait(Driver, HearingAssignJudgePage.SearchResults).Click();
            Driver.FindElement(HearingAssignJudgePage.NextButton).Click();
            var participant = new Participant();
            participant.Id = judge.Email;
            _hearing.Participant.Add(participant);
            _scenarioContext["Hearing"] = _hearing;
        }
    }
}
