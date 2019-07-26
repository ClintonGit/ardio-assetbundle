using UnityEngine;
using System.Collections.Generic;
using SA.Android.Utilities;
using SA.Android.Vending.Licensing;
using SA.Foundation.Tests;
using SA.Foundation.Async;

using SA.Android.Social;
using SA.Foundation.Utility;


namespace SA.Android.Tests.Social
{
    public class AN_ShareComposer_Test : SA_BaseTest
    {
        public override bool RequireUserInteraction { get { return true; } }

        public override void Test() {

            SA_ScreenUtil.TakeScreenshot(256, (screenshot) => {
                var composer = new AN_ShareComposer();
                composer.SetTitle("Android Native Share Example");
                composer.SetText("Hello world");
                composer.AddImage(screenshot);
                composer.AddImage(screenshot);

                composer.Share(() => {
                    Debug.Log("Sharing flow is finished, User has retured to the app");
                    SetResult(SA_TestResult.OK);
                });
            });

        }

    
    }
}