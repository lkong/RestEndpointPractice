﻿using Funq;
using RESTShared;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace RESTService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            (new SpellCheckAppHost()).Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
    /// <summary>
    /// Service to process received REST requests
    /// </summary>
    public class DictionaryWebService : Service
    {
        private DictionaryDBService dictionaryDBService = new DictionaryDBService();
        public DictionaryWebService()
        {
            dictionaryDBService.Init(); 
        }
        /// <summary>
        /// Process POST requests
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public SpellCheckSuggestion Post(SpellCheckRequest word)
        {
            dictionaryDBService.InsertWord(new Word() { Spell = word.Word });
            return new SpellCheckSuggestion() { Word = word.Word, Rank = 0 };
        }
        /// <summary>
        /// Process GET requests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<SpellCheckSuggestion> Get(SpellCheckRequest request)
        {
            return dictionaryDBService.GetSpellSuggestions(new Word() {Spell=request.Word });
        }
    }
    public class SpellCheckAppHost : AppHostBase
        {
            /// <summary>
            /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
            /// </summary>
            public SpellCheckAppHost() : base("Spellcheck Web Services", typeof(DictionaryWebService).Assembly) 
            {
            }
 
            /// <summary>
            /// Configure the container with the necessary routes for your ServiceStack application.
            /// </summary>
            /// <param name="container">The built-in IoC used with ServiceStack.</param>
            public override void Configure(Container container)
            {
            //Register user-defined REST-ful urls. You can access the service at the url similar to the following.
            Routes
              .Add<SpellCheckRequest>("/spellcheckrequest")
              .Add<SpellCheckRequest>("/spellcheckrequest/{Word}","GET")
              .Add<SpellCheckRequest>("/spellcheckrequest/{Word}","POST")
              ;
        }
        }

}