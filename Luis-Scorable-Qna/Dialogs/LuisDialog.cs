﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Threading;

namespace Scorable.Dialogs
{
    [Serializable]
    [LuisModel("e7a9c2d5-0b92-47d3-9d73-d35fb45c1b8e", "4941fa348c49494db1e8e8d2fd7adb2c")]
    public class LuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = $"Hello there";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        private ResumeAfter<object> after()
        {
            return null;
        }

        [LuisIntent("weather")]
        public async Task Middle(IDialogContext context, LuisResult result)
        {
            // confirm we hit weather intent
            string message = $"Weather forecast is...";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("joke")]
        public async Task Joke(IDialogContext context, LuisResult result)
        {
            // confirm we hit joke intent
            string message = $"Let's see...I know a good joke...";

            await context.PostAsync(message);

            await context.Forward(new JokeDialog(), ResumeAfterJokeDialog, context.Activity, CancellationToken.None);
        }

        [LuisIntent("question")]
        public async Task QnA(IDialogContext context, LuisResult result)
        {
            // confirm we hit QnA
            string message = $"Routing to QnA... ";
            await context.PostAsync(message);

            var userQuestion = (context.Activity as Activity).Text;
            await context.Forward(new QnaDialog(), ResumeAfterQnA, context.Activity, CancellationToken.None);       
        }

        private async Task ResumeAfterQnA(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }

        private async Task ResumeAfterJokeDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }

    }
}