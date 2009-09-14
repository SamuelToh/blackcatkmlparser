﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;

namespace TestDataModel
{
    [TestFixture] 
    class TestSocialModel
    {
        private SocialModel social;

        [SetUp]
        public void SetUp()
        {
            social = new SocialModel();

        }
        // pre: true
        // post: returns the sociological column names
        //ArrayList getColumnNames();


        // pre: true
        // post: returns the original sociological table
        //DataTable getDataTbl();

        // pre: true
        // post: returns list of first preferences party names
        //ArrayList getFirstPrefParties();

        // pre: true
        // post: returns list of TPP names
        //ArrayList getTPPNames();

        //This method is used to update social data table. The winner party is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        //pre: true
        //post: social data table includes winner party in each row.
        //void calculateWinners();

        // pre: true
        // post: returns a sociological table including winner parties
        //DataTable getUpdatedTable();

        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        //string getSeatWinner(string electorate);
    }
}